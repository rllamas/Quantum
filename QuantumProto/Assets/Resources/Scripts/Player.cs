using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	#region public variables
		public float walkingVelocity = 1000.0f;
	#endregion
	
	#region private variables
		OTSprite playerSprite;
	#endregion
	
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing player...");
		
		
		// ORTHELLO EXAMPLE CODE
		
		// Lookup this block's sprite
	    playerSprite = GetComponent<OTSprite>();
		playerSprite.name = "Player";
	    // Set this sprite's collision delegate 
	    // HINT : We could use sprite.InitCallBacks(this) as well.
	    // but because delegates are the C# way we will use this technique
	    playerSprite.onCollision = OnCollision;  
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey("left")) {
			this.transform.Translate( new Vector2(-1 * walkingVelocity * Time.deltaTime, 0) );
		}
		else if (Input.GetKey("right")) {
			this.transform.Translate( new Vector2(walkingVelocity * Time.deltaTime, 0) );
		}
	
	}
	
	// This method will be called when this block is hit.
	public void OnCollision(OTObject owner) {
		
		Debug.Log (owner.name + " is colliding with something!");
		
		
		
		// ORTHELLO EXAMPLE CODE
		
	    // Set color fading indicator
	    //colorFade = true;
	    // Reset fade time
	    //fadeTime = 0;
	}
}
