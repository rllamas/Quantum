#pragma strict
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
// Main Pro Example 3a Demo class
// ------------------------------------------------------------------------
	
/*	
private var pc: OTPathController;	
private var clones:Array = new  Array();

// Create a new arrow sprite object and put it on the circle path
function NewArrow()
{
	// create a new arrow sprite from the prototype
	var arrow:OTSprite = OT.CreateObject("arrow").GetComponent("OTSprite") as OTSprite;
	// place arrow on path;
	var pc:OTPathController = (GameObject.Find("path-circle").GetComponent("OTShape") as OTShape).
		IsPathFor(arrow,2,false,0);
	// return the path controller
	return pc;
}


function CloneArrow()
{
	// create a new arrow and get its path controller
	var clonepc:OTPathController = NewArrow();		
	// get sprite reference from path controller
	var clone:OTSprite = clonepc.owner as OTSprite;
	// store clone and path controller for cleanup later
	clones.Add(clone);		
	// adjust cloned sprite so that it is tinted red, a bit smaller and behind the main arrow
	clone.tintColor = Color.red;
	clone.size = new Vector2(40,40);
	clone.depth += 10;
	// adjust the controller so that it is looping, speed is a bit slower that the main arrow
	// and heading + position is the same as main arrow
	clonepc.looping = true;
	clonepc.speed = pc.speed/(1.2f+(Random.value * 1.2f));
	clonepc.position = pc.position;
	clonepc.oppositeDirection = pc.oppositeDirection;
	clonepc.upVector = pc.upVector;		
}

// Use this for initialization
function Start()
{
	// Create arrow sprite from prototype
	pc = NewArrow();
	// Set the up vector so that the arrow points clockwise
	pc.upVector = OTPathController.UpVector.OutWard;
	// set finish event handler
	pc.onFinish = finishedPath;
	// start in looping mode
	pc.looping = true;
	// start with a non-moving arrow
	pc.Stop();		
}

// finish event is called when sprite is at starting position
private var finished:int = 0;
function finishedPath(path:OTShape, owner:Object)
{
	// increase the counter that we will display in the OnGUI
 	finished++;		
}	


function OnGUI()
{		
	// GUI has yellow tint
	GUI.backgroundColor = Color.yellow;
	GUI.Box(new Rect(5,5,400,160), "");
	if (GUI.Button( new Rect(10,10,150,24), "Move Clockwise"))
	{
		// set direction to normal
		pc.oppositeDirection = false;	
		// up vector outward will point the arrow clockwise
		pc.upVector = OTPathController.UpVector.OutWard;
		// start moving if arrow is standing still
		if (!pc.isMoving) 
			pc.Start();
	}
	if (GUI.Button( new Rect(170,10,200,24), "Move Counter Clockwise"))
	{
		// set direction to opposite
		pc.oppositeDirection = true;	
		// up vector inward will point the arrow counter-clockwise
		pc.upVector = OTPathController.UpVector.InWard;
		// start moving if arrow is standing still
		if (!pc.isMoving) 
			pc.Start();
	}
	// handle GUI & looping adjustment				
	pc.looping = GUI.Toggle(new Rect(10,40,150,24), pc.looping, "  Looping");
	// handle GUI & speed
	GUI.Label(new Rect(10,70,150,24),"Speed : ");
	pc.speed = GUI.HorizontalSlider(new Rect(70,76,200,24), pc.speed, 0.15f, 3f);		
	// display number of finishes
	GUI.Label(new Rect(10,100,150,24),"Over finish : "+finished);
	
	if (clones.Count>0)
	{
		if (GUI.Button( new Rect((pc.isMoving)?170:10,130,150,24), "Remove Clones"))
		{
			// remove all clones
			while (clones.Count>0)
			{
				OT.DestroyObject(clones[0] as OTObject);
				clones.RemoveAt(0);
			}
		}
	}
	if (pc.isMoving)
	{
		if (GUI.Button( new Rect(10,130,150,24), "New Clone"))
			CloneArrow();
	}
}
*/