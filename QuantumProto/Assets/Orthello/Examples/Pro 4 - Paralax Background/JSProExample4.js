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
// Pro Example 4
// Parallax Scrolling Background
// ------------------------------------------------------------------------
// Main Pro Example 4 Demo class
// ------------------------------------------------------------------------

/*
// Create the sun action controller - The sun will pulsate a bit (color and size)
function SunController()
{
    // create a new action controller
    var ac:OTActionController = new OTActionController("sun");
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
function ArrowController()
{
    // create a new action controller
    var ac:OTActionController = new OTActionController("arrow-left");
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
function Start () {

    var sun:OTSprite = GameObject.Find("sun").GetComponent("OTSprite");
    // Set up pulsating sun
    sun.AddController(SunController());

    var arrowLeft:OTSprite = GameObject.Find("arrow-left-1").GetComponent("OTSprite");
    // set up arrow left effects
    arrowLeft.AddController(ArrowController());

    var arrowRight:OTSprite = GameObject.Find("arrow-right-1").GetComponent("OTSprite");
    // set up arrow right effects
    arrowRight.AddController(ArrowController());
}

// Update is called once per frame
function Update () {
   // move the view related to the mouse position in the view
   var mx:Number = OT.view.mouseViewPosition.x - 0.5f;
    OT.view.position += new Vector2(1000, 0) * mx * (Time.deltaTime*5);
}

*/