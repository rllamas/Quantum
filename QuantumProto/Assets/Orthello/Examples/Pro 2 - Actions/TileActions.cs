using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TileActions : OTActionController  {

    Component scriptComponent;

    
    public TileActions(Component scriptComponent)
    {
        this.scriptComponent = scriptComponent;
    }

    // Create Actions for this custom action controller
    
    protected override void Actions()
    {
        base.Actions();
        // commonly used actions
        Add("FadeIn", new OTActionFadeIn(1, OTEasing.Linear));
        Add("FadeOut", new OTActionFadeOut(1, OTEasing.Linear));
        Add("TintNormal", new OTActionTween("tintColor", new Color(0.5f, 0.5f, 0.5f), 1, OTEasing.StrongOut));
        Add("FadeHalf", new OTActionFade(0.5f, 1, OTEasing.Linear));

        // custom tile actions
        Add("Grow", new OTActionSizeBy(new Vector2(50, 50), 1f, OTEasing.ElasticOut));
        Add("Rotate", new OTActionRotateBy(45, 1f, OTEasing.ElasticOut));
        Add("RotateBack", new OTActionRotateBy(-45, 1f, OTEasing.ElasticOut));
        Add("EndSize", new OTActionSize(new Vector2(200, 200), 1f, OTEasing.Linear));
        Add("EndGrow", new OTActionSize(new Vector2(500, 500), 2f, OTEasing.Linear));
        Add("EndDepth", new OTActionSet("depth", -200));
        Add("EndCenter", new OTActionMove(Vector2.zero,1, OTEasing.ElasticOut));
        Add("Shrink", new OTActionSizeBy(new Vector2(-50, -50), 1f, OTEasing.ElasticOut));
        Add("TintBrightYellow", new OTActionTween("tintColor", new Color(1, .95f, .8f), 1, OTEasing.Linear));
        Add("ToFront", new OTActionSet("depth",-100));
        Add("AllTilesDepthPlusOne", new OTActionCall("AllTilesDepthPlusOne", scriptComponent));
        Add("TintDarker", new OTActionTween("tintColor", new Color(0.25f, 0.25f, 0.25f), 1, OTEasing.Linear));

    }

    // Create tile action trees
    
    protected override void ActionTrees()
    {
        base.ActionTrees();
        Add("Hover",
            new OTActionTree().
                Action("ToFront").And("TintBrightYellow",.25f));
        Add("HoverOut",
            new OTActionTree().
                Action("AllTilesDepthPlusOne").
                    FollowedBy("TintNormal", .5f));
        Add("DragStart",
            new OTActionTree().
                Action("Grow").And("Rotate"));
        Add("DragEnd",
            new OTActionTree().
                Action("Shrink").And("RotateBack"));
        Add("TileEnd",
            new OTActionTree().
                Action("EndDepth").And("TintBrightYellow",0).
                    FollowedBy("EndSize", .5f).And("EndCenter", 1.5f).
                            FollowedBy("EndGrow").And("FadeOut", 1.5f).
                                Destroy());
    }

}
