// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example 
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Because Orthello is created as a C# framework the C# classes 
// will only be available as you place them in the /Standard Assets folder.
//
// If you would like to test the JS examples or use the framework in combination
// with Javascript coding, you will have to move the /Orthello/Standard Assets folder
// to the / (root).
//
// This code was commented to prevent compiling errors when project is
// downloaded and imported using a package.
// ------------------------------------------------------------------------
// Pro Example 2
// Actions
// ------------------------------------------------------------------------
// Main Pro Example 2 Demo class
// ------------------------------------------------------------------------

/*
private var dragging:boolean = false;           // dragging indicator
private var current:GameObject = null;          // current active object
private var endingTiles:Array = new Array();     // all tiles that are in ending state


function TileActions(component:Component)
{
   var ac:OTActionController = new OTActionController();
   
    // commonly used actions
    ac.Add("FadeIn", new OTActionFadeIn(1, OTEasing.Linear));
    ac.Add("FadeOut", new OTActionFadeOut(1, OTEasing.Linear));
    ac.Add("TintNormal", new OTActionTween("tintColor", new Color(0.5f, 0.5f, 0.5f), 1, OTEasing.StrongOut));
    ac.Add("FadeHalf", new OTActionFade(0.5f, 1, OTEasing.Linear));

    // custom racer actions
    ac.Add("Grow", new OTActionSizeBy(new Vector2(50, 50), 1f, OTEasing.ElasticOut));
    ac.Add("EndSize", new OTActionSize(new Vector2(200, 200), 1f, OTEasing.Linear));
    ac.Add("EndGrow", new OTActionSize(new Vector2(500, 500), 2f, OTEasing.Linear));
    ac.Add("EndDepth", new OTActionSet("depth", -200));
    ac.Add("EndCenter", new OTActionMove(Vector2.zero,1, OTEasing.ElasticOut));
    ac.Add("Shrink", new OTActionSizeBy(new Vector2(-50, -50), 1f, OTEasing.ElasticOut));
    ac.Add("TintBrightYellow", new OTActionTween("tintColor", new Color(1, .95f, .8f), 1, OTEasing.Linear));
    ac.Add("ToFront", new OTActionSet("depth",-100));
    ac.Add("AllTilesDepthPlusOne", new OTActionCall("AllTilesDepthPlusOne", component));
    ac.Add("TintDarker", new OTActionTween("tintColor", new Color(0.25f, 0.25f, 0.25f), 1, OTEasing.Linear));
 
 	// add action trees
	ac.Add("Hover",
            new OTActionTree().
                Action("ToFront").And("TintBrightYellow",.25f));
    ac.Add("HoverOut",
        new OTActionTree().
            Action("AllTilesDepthPlusOne").FollowedBy("TintNormal", .5f));
    ac.Add("DragStart",
        new OTActionTree().
            Action("Grow"));
    ac.Add("DragEnd",
        new OTActionTree().
            Action("Shrink"));
    ac.Add("TileEnd",
        new OTActionTree().
            Action("EndDepth").And("TintBrightYellow",0).
                FollowedBy("EndSize", .5f).And("EndCenter", 1.5f).
                        FollowedBy("EndGrow").And("FadeOut", 1.5f).
                            Destroy());   
   
   return ac;    
}

// Create 50 random sized tiles
function CreateTiles()
{
    // Lookup the asteroid container
    var tiles:GameObject = GameObject.Find("-Tiles");
    // Iterate and create 50 tile sprites
    for (var t:int = 0; t < 50; t++)
    {
        // Create a new tile from its prototype
        var tile:OTSprite = OT.CreateObject("tile").GetComponent("OTSprite");
        // Set the parent of this tile to the container
        tile.transform.parent = tiles.transform;
        // Determine a random x size
        var sx:Number = 30 + Random.value * 120;
        // Set size of the tile
        tile.size = new Vector2(sx, sx );
        // Put the tile on a random screen position
        tile.position = new Vector2((Screen.width * -1 / 2) + Random.value * Screen.width, (Screen.height / 2) - Random.value * Screen.height);
        // Set the tile image to a random index
        tile.frameIndex = Mathf.Floor(Random.value * 15);
        // Make the tile draggable
        tile.draggable = true;

        // Add the custom tile action controller to this tile
        tile.AddController(TileActions(this));

        // tell the tile yo use CallBack functions
        tile.InitCallBacks(this);
    }
}

// 'short' helper function to get custom tile action controller
function Actions(owner:OTObject)
{
    return owner.Controller(OTActionController);
}

// public function that will be called from the action controller
// to move all tiles one level to the back (increasing depth by one )
function  AllTilesDepthPlusOne()
{
    // Lookup the asteroid container
    var tiles:GameObject = GameObject.Find("-Tiles");
    // loop all tiles
    for (var t:int = 0; t < tiles.transform.childCount; t++)
    {
        // only change depth if it is not the dragging object
        if (tiles.transform.GetChild(t).gameObject!=current)
            (tiles.transform.GetChild(t).GetComponent("OTObject") as OTObject).depth++;
    }
}

// 'public' input handler to capture the right mouse button click
public function OnInput(owner:OTObject)
{
    if (Input.GetMouseButtonDown(1))
    {
        // right mousebutton was clicked
        for (var e:int = 0; e < endingTiles.length; e++)
        {
            // move all 'valid' ending tiles one level to the front
            if (endingTiles[e]!=null)
                (endingTiles[e] as OTObject).depth--;
        }
        // add this tile to the ending tiles
        endingTiles.Add(owner);
        // activate the tile ending action tree
        Actions(owner).Run("TileEnd");
    }
}

// This function will be called when a tile is started beeing dragged.
public function OnDragStart(owner:OTObject)
{
    // if this tile is ending stop here
    if (Actions(owner).IsRunning("TileEnd"))
        return;
    // only one tile can be dragged so we need to wait for an enddrag
    if (dragging) return;
    // start the DragStart effect
    dragging = true;
    Actions(owner).Run("DragStart");
}

public function OnDragEnd(owner:OTObject)
{
    dragging = false;
    // if this tile is ending stop here
    if (Actions(owner).IsRunning("TileEnd"))
        return;
    else
        Actions(owner).Run("DragEnd");
}

public function OnMouseEnterOT(owner:OTObject)
{
    // if this tile is ending or we are dragging stop here
    if (Actions(owner).IsRunning("TileEnd") || dragging)
        return;

    // if the DragEnd actions are playing we need to set the 'Hover' tree in the action queueu
    if (Actions(owner).IsRunning("DragEnd"))
        Actions(owner).Next("Hover");
    else
        Actions(owner).Run("Hover");
    current = owner.gameObject;
}

public function OnMouseExitOT(owner:OTObject)
{
    // if this tile is ending or we are dragging stop here
    if (Actions(owner).IsRunning("TileEnd") || dragging)
        return;

    // if the DragEnd actions are playing we need to set the 'HoverOut' tree in the action queueu
    if (Actions(owner).IsRunning("DragEnd"))
       Actions(owner).Next("HoverOut");
   else
       Actions(owner).Run("HoverOut");
}

// Use this for initialization
function  Start () {
    // Create 50 random tiles
    CreateTiles();
}

// Update is called once per frame
function Update () {

}
*/