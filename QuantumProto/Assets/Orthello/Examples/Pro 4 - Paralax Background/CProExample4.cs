// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Pro Example 4
// Parallax scrolling background
// ------------------------------------------------------------------------
// Main example class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class CProExample4 : MonoBehaviour {

    // Create the sun action controller - The sun will pulsate a bit (color and size)
    OTActionController SunController()
    {
        // create a new action controller
        OTActionController ac = new OTActionController("sun");
        // Add the size and color actions
        ac.Add("size-up", new OTActionSize(new Vector2(205, 205), 0.25f, OTEasing.CircInOut));
        ac.Add("size-down", new OTActionSize(new Vector2(200, 200), 0.25f, OTEasing.CircInOut));
        ac.Add("tint-up", new OTActionTween("tintColor", new Color(1f, 1f, 1f), 0.25f, OTEasing.CircInOut));
        ac.Add("tint-down", new OTActionTween("tintColor", new Color(1f, 1f, 0.5f), 0.25f, OTEasing.CircInOut));
        // Add the action tree
        ac.Add("size",
            new OTActionTree().Action("size-up").And("tint-up").
                FollowedBy("size-down").And("tint-down"));
        // run this action looping ( count = -1 )
        ac.Run("size", 1, -1);
        return ac;
    }


    // Create an arrow action controller - size up and fade out.
    OTActionController ArrowController()
    {
        // create a new action controller
        OTActionController ac = new OTActionController("arrow-left");
        // add size and fade actions
        ac.Add("setSize", new OTActionSet("size", new Vector2(150, 118)));
        ac.Add("setAlpha", new OTActionSet("alpha", .8f));
        ac.Add("sizeUp", new OTActionSize(new Vector2(150, 118) * 1.4f, .75f, OTEasing.SineOut));
        ac.Add("FadeOut", new OTActionFadeOut(.75f, OTEasing.Linear));
        // Add the action tree
        ac.Add("effect",
            new OTActionTree().Wait(2).
                FollowedBy("setSize").And("setAlpha").
                FollowedBy("sizeUp").And("FadeOut"));
        // Run the tree looping (count - -1 )
        ac.Run("effect", 1, -1);
        return ac;
    }

	// Use this for initialization
	void Start () {

        OTSprite sun = GameObject.Find("sun").GetComponent<OTSprite>();
        // Set up pulsating sun
        sun.AddController(SunController());

        OTSprite arrowLeft = GameObject.Find("arrow-left-1").GetComponent<OTSprite>();
        // set up arrow left effects
        arrowLeft.AddController(ArrowController());

        OTSprite arrowRight = GameObject.Find("arrow-right-1").GetComponent<OTSprite>();
        // set up arrow right effects
        arrowRight.AddController(ArrowController());
    }
	
	// Update is called once per frame
	void Update () {
        // move the view related to the mouse position in the view
        float mx = OT.view.mouseViewPosition.x - 0.5f;
        OT.view.position += new Vector2(1000, 0) * mx * (Time.deltaTime*5);
	}
}
