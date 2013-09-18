// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Pro Example 3
// Movement on path
// ------------------------------------------------------------------------
// Main example class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;


public class CProExample3 : MonoBehaviour
{

    bool zooming = false;                   // zooming indicator
    float zoomSpeed = 4f;                   // how fast do we zoom in/out
    float zoomMin = -0.5f;                  // Zoomed out value
    float zoomMax = 1f;                     // Zoomed in value


    // Initialize view
    void InitView()
    {
        // no rotation for this view
        OT.view.rotation = 0;
        // position x/y to 0/0
        OT.view.position = Vector2.zero;
    }

    // Create 25 racer sprites and set them on the path
    void CreateRacers()
    {
        // Lookup racer container
        GameObject Racers = GameObject.Find("-Racers");
        // Create 25 racer sprites
        for (int r = 0; r < 25; r++)
        {
            // Create a new racer from its prototype
            OTSprite racer = OT.CreateObject("racer").GetComponent<OTSprite>();
            // Set the parent of this racer to the container
            racer.transform.parent = Racers.transform;
            // Get the OTPutOnPath Script
            // HINT : The OTPutOnPath script is a helper script that lets you manage the OTPathController using
            // editor settings. The downfall is that it will take an Update() cycle because it is a mono behaviour class
            // you could drop this OTPutOnPath script and add + configute the OTPathController manually.
            OTPutOnPath put = racer.GetComponent<OTPutOnPath>();
            if (put != null)
            {
                // Set racer at random position on the path
                put.customPosition = Random.value;
                // Set how long a racer will take to complete the track
                put.duration = 5 + Random.value * 20;
            }
            // hook up onInput delegate to handle clicking on a mover
            racer.onInput = OnInput;
        }
    }

    // Input handler
    
    public void OnInput(OTObject owner)
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
    void Start()
    {
        // initialize the view
        InitView();
        // create 25 racer sprites
        CreateRacers();
	}
	
	// Update is called once per frame
	void Update () {
        // Check if the right mouse button was pressed
        if (Input.GetMouseButtonDown(2))
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
	
	OTPathController.UpVector vector = OTPathController.UpVector.Follow;
	void OnGUI()
	{		
		OTPathController.UpVector _vector = vector;		
		GUI.Box(new Rect(2,2,110,150),"Orientation");
		_vector = (GUI.Toggle(new Rect(5,25,100,20),(_vector == OTPathController.UpVector.Follow),"Follow"))?OTPathController.UpVector.Follow:_vector;
		_vector = (GUI.Toggle(new Rect(5,50,100,20),(_vector == OTPathController.UpVector.InWard),"Inward"))?OTPathController.UpVector.InWard:_vector;
		_vector = (GUI.Toggle(new Rect(5,75,100,20),(_vector == OTPathController.UpVector.OutWard),"Outward"))?OTPathController.UpVector.OutWard:_vector;
		_vector = (GUI.Toggle(new Rect(5,100,100,20),(_vector == OTPathController.UpVector.Target),"Target"))?OTPathController.UpVector.Target:_vector;
		_vector = (GUI.Toggle(new Rect(5,125,100,20),(_vector == OTPathController.UpVector.None),"None"))?OTPathController.UpVector.None:_vector;
		if (vector!=_vector)
		{
			vector = _vector;
			GameObject racers = GameObject.Find("-Racers");
			OTPutOnPath[] o = racers.transform.GetComponentsInChildren<OTPutOnPath>();
			for (int c=0; c<o.Length; c++)				
				o[c].upVector = vector;			
		}
	}
	
}
