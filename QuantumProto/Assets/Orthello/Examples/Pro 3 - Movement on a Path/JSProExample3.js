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
// Pro Example 3
// Movement on a path
// ------------------------------------------------------------------------
// Main Pro Example 1 Demo class
// ------------------------------------------------------------------------

/*
private var zooming:boolean = false;                 // zooming indicator
private var zoomSpeed:Number = 4f;                   // how fast do we zoom in/out
private var zoomMin:Number = -0.5f;                  // Zoomed out value
private var zoomMax:Number = 1f;                     // Zoomed in value


// Initialize view
function InitView()
{
    // no rotation for this view
    OT.view.rotation = 0;
    // position x/y to 0/0
    OT.view.position = Vector2.zero;
}

// Create 25 racer sprites and set them on the path
function CreateRacers()
{
    // Lookup racer container
    var Racers:GameObject = GameObject.Find("-Racers");
    // Create 25 racer sprites
    for (var r:int = 0; r < 25; r++)
    {
        // Create a new racer from its prototype
        var racer:OTSprite = OT.CreateObject("racer").GetComponent(OTSprite);
        // Set the parent of this racer to the container
        racer.transform.parent = Racers.transform;
        // Get the OTPutOnPath Script
        // HINT : The OTPutOnPath script is a helper script that lets you manage the OTPathController using
        // editor settings. The downfall is that it will take an Update() cycle because it is a mono behaviour class
        // you could drop this OTPutOnPath script and add + configute the OTPathController manually.
        var put:OTPutOnPath = racer.GetComponent(OTPutOnPath);
        if (put != null)
        {
            // Set racer at random position on the path
            put.customPosition = Random.value;
            // Set how long a racer will take to complete the track
            put.duration = 5 + Random.value * 20;
        }
        // tell this racer to use callback functions so we can handle OnInput
        racer.InitCallBacks(this);
    }
}

// Input handler

public function OnInput(owner:OTObject)
{
    // check if we clicked the left mouse button
    if (Input.GetMouseButtonDown(0))
    {
        // Link View target object for movement and rotation 
        OT.view.movementTarget = owner.gameObject;
        OT.view.rotationTarget = owner.gameObject;
        // We want to zoom in on that target
        zooming = true;
    }
}


// Use this for initialization
function Start()
{
    // initialize the view
    InitView();
    // create 25 racer sprites
    CreateRacers();
}

// Update is called once per frame
function Update () {
    // Check if the right mouse button was pressed
    if (Input.GetMouseButtonDown(1))
    {
        // Drop our view follow target
        OT.view.movementTarget = null;
        OT.view.rotationTarget = null;
        // Set our view to its initial state
        InitView();
        // Lets start zooming out
        zooming = true;
    }

    if (zooming)
    {
        // we are zooming in or out
        if (OT.view.movementTarget != null)
        {
            // zooming in
            OT.view.zoom += zoomSpeed * Time.deltaTime;
            // cap zooming at max
            if (OT.view.zoom > zoomMax)
            {
                OT.view.zoom = zoomMax;
                zooming = false;
            }
        }
        else
        {
            // zooming out
            OT.view.zoom -= zoomSpeed * Time.deltaTime;
            // cap zooming at min
            if (OT.view.zoom < zoomMin)
            {
                OT.view.zoom = zoomMin;
                zooming = false;
            }
        }
    }

}
*/