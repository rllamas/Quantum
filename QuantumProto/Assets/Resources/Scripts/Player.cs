using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	#region public variables
		public float walkingVelocity = 1000.0f;
		public float jumpingVelocity = 15000.0f;
	#endregion
	
	#region private variables
		private OTSprite playerSprite;
	
		public enum playerStates { // public only for debugging
			STANDING,
			WALKING,
			JUMPING
		}
	
		public enum playerDirections { // public only for debugging
			NONE,
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
		currentDirection = playerDirections.NONE;
		currentlyJumping = false;
		
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
		
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

		UpdateDirection();
		UpdateState();
		Move();

	}
	
	
	void UpdateDirection() {
		// Handle changing directions right to left
		if (Input.GetKey("left")) {
			
			playerSprite.flipHorizontal = false; // Change the way the sprite is facing
			currentDirection = playerDirections.LEFT;
		}
		// Handle changing directions left to right
		else if (Input.GetKey("right")) {
			
			playerSprite.flipHorizontal = true;	// Change the way the sprite is facing
			currentDirection = playerDirections.RIGHT;
		}	
		else {
			currentDirection = playerDirections.NONE;	
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

		// if starting a jump
		if (currentState == playerStates.JUMPING && !currentlyJumping) {
			currentlyJumping = true;
			//this.rigidbody.AddForce( new Vector3(0, jumpingVelocity * Time.deltaTime, 0) );
			this.transform.Translate( new Vector2(0, jumpingVelocity * Time.deltaTime) );
		}
		else if (currentState == playerStates.WALKING || currentState == playerStates.JUMPING) {
			// if walking left
			if (currentDirection == playerDirections.LEFT) {
				this.transform.Translate( new Vector2(-1 * walkingVelocity * Time.deltaTime, 0) );
			}
			// if walking right
			else if (currentDirection == playerDirections.RIGHT) {
				this.transform.Translate( new Vector2(walkingVelocity * Time.deltaTime, 0) );
			}
		}
	}
	
	
	public void OnCollisionEnter(Collision collision) {
		
		Debug.Log (this.name + " is colliding with an Unity object!");
		
		// If landing from a jump
		if (currentlyJumping) {
			currentState = playerStates.STANDING;	
			currentlyJumping = false;
		}
		
	}
	
	
	// This method will be called when this block is hit.
	public void OnCollision(OTObject owner) {
		
		Debug.Log (owner.name + " is colliding with an Orthello object!");
		
	}
}
