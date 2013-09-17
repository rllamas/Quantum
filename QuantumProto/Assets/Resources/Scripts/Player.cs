using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	#region public variables
		public float walkingVelocity = 1000.0f;
		public float jumpHeight = 500.0f;
	#endregion
	
	#region private variables
		private OTSprite playerSprite;
	
		public enum playerStates { // public only for debugging
			STANDING,
			WALKING,
			JUMPING
		}
	
		public enum playerDirections { // public only for debugging
			LEFT,
			RIGHT
		}
	
		public playerStates currentState; // public only for debugging
		public playerDirections currentDirection; // public only for debugging
		public bool currentlyJumping;
	#endregion
	
	

	
	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing player...");
		
		currentState = playerStates.STANDING;
		currentDirection = playerDirections.LEFT;
		currentlyJumping = false;
		
		
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
		// Update direction
		UpdateDirection();
		UpdateState();
		Move();
		// Update state
		// move player based on state
	
	}
	
	
	void UpdateDirection() {
		// Handle changing directions right to left
		if (Input.GetKey("left")) {
			if (currentDirection == playerDirections.RIGHT) {
				currentDirection = playerDirections.LEFT;	
			}
		}
		// Handle changing directions left to right
		else if (Input.GetKey("right")) {
			if (currentDirection == playerDirections.LEFT) {
				currentDirection = playerDirections.RIGHT;	
			}
		}	
	}
	
	
	void UpdateState() {
		// If in midair, ignore. state will be changed from jumping in collision detection. 
		if (currentlyJumping) {
			return;	
		}
		else if (Input.GetKey("left")) {
			currentState = playerStates.WALKING;
		}
		else if (Input.GetKey("right")) {
			currentState = playerStates.WALKING;
		}
		else if (Input.GetButton("Jump")) {
			currentState = playerStates.JUMPING;
		}
		else {
			currentState = playerStates.STANDING;	
		}
	}
	
	
	void Move() {
		if (currentlyJumping) {
			return;
		}
		else if (Input.GetKey("left")) {
			// if player should walk left
			if (currentState == playerStates.WALKING) {
				this.transform.Translate( new Vector2(-1 * walkingVelocity * Time.deltaTime, 0) );
			}
		}
		else if (Input.GetKey("right")) {
			// if player should walk right
			if (currentState == playerStates.WALKING) {
				this.transform.Translate( new Vector2(walkingVelocity * Time.deltaTime, 0) );
			}
		}	
	}
	
	// This method will be called when this block is hit.
	public void OnCollision(OTObject owner) {
		
		Debug.Log (owner.name + " is colliding with something!");
		
		// If landing from a jump
		if (currentState == playerStates.JUMPING) {
			currentState = playerStates.STANDING;	
			currentlyJumping = false;
		}
		
	}
}
