// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Pro Example 3a
// Movement on path
// ------------------------------------------------------------------------
// Main example class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CProExample3a : MonoBehaviour
{
	
	private OTPathController pc;	
	private List<OTSprite> clones = new  List<OTSprite>();
	
	// Create a new arrow sprite object and put it on the circle path
	OTPathController NewArrow()
	{
		// create a new arrow sprite from the prototype
		OTSprite arrow = OT.CreateObject("arrow").GetComponent<OTSprite>();
		// place arrow on path;
		OTPathController pc = GameObject.Find("path-circle").GetComponent<OTShape>().
			IsPathFor(arrow,2,false,0);
		// return the path controller
		return pc;
	}

	
	void CloneArrow()
	{
		// create a new arrow and get its path controller
		OTPathController clonepc = NewArrow();		
		// get sprite reference from path controller
		OTSprite clone = clonepc.owner as OTSprite;
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
    void Start()
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
	int finished = 0;
	void finishedPath(OTShape path, object owner)
	{
		// increase the counter that we will display in the OnGUI
	 	finished++;		
	}	
	
	
	void OnGUI()
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
					OT.DestroyObject(clones[0]);
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
			
}
