using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing player...");
		
		
		// ORTHELLO EXAMPLE CODE
		
		// Lookup this block's sprite
	    OTSprite sprite = GetComponent<OTSprite>();
		sprite.name = "Player";
	    // Set this sprite's collision delegate 
	    // HINT : We could use sprite.InitCallBacks(this) as well.
	    // but because delegates are the C# way we will use this technique
	    sprite.onCollision = OnCollision;  
	}
	
	// Update is called once per frame
	void Update () {
	
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
