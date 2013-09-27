using UnityEngine;
using System;
using System.Collections;
using Quantum.States;

public class Player : MonoBehaviour {
	
	public Pickup heldPickup;
	
	/* All possible directions the player can be in. */
	public enum Direction {
		LEFT,
		RIGHT,
		TURNING_LEFT_TO_RIGHT,
		TURNING_RIGHT_TO_LEFT,
		NONE,
	};
	
	
	
	
	/* Current game state that the player is in. */
	public GameState currentState;

	/* Previous game state that the player was in. */
	public GameState previousState;
	
	
	
	
	/* Current direction that the player is in. */
	public Direction currentDirection;
	
	/* Last direction that the player was in. */
	public Direction previousDirection;
	
	
	
	
	/* The current sprite of the player. */
	public tk2dSprite sprite;
	
	
	
	/* The walking speed of the player. */
	public float walkingVelocity = 20.0f;
	
	/* The jumping speed of the player. */
	public float jumpingVelocity = 20.0f;
	
	
	/* There's a cooldown time for both picking up and setting down pickups. */
	public float pickupCooldown = 0.5f;
	private float pickupCooldownTimeRemaining = 0.0f;
	
	
	
	
	void Start () {
		gameObject.tag = "Player";
		
		currentDirection = Direction.LEFT;
		previousDirection = Direction.LEFT;
		
		currentState = new ProfessorStandingState(this);
		previousState = new ProfessorStandingState(this);
		
		sprite = GetComponent<tk2dSprite>();
		if (!sprite) {
			throw new Exception("No tk2dSprite was attached to the Player!!!");	
		}
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
	}
	
	
	
	
	void Update () {
		/* Update direction. */
		previousDirection = currentDirection;
		currentDirection = NextDirection();
		
		/* Update game state. */
		previousState = currentState;
		currentState = currentState.NextState();
		
		/* Let the current game state do what it needs to do. */
		currentState.Logic();
	
		HandleExtraLogic();
	}
	
	
	
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Player: Colliding with something.");
    }
	
	
	
	
	void OnCollisionExit(Collision collision) {
		Debug.Log ("Player: No longer colliding with something.");
	}
		
	
	
	
	void OnTriggerStay(Collider other) {
		
		/* If hitting action button and other is the collider for something currently possible to pick up,
		 * then pick it up. */
		if (Input.GetButton("Action1") && IsPossibleToPickup(other.gameObject)) {
			
			Pickup triggeredPickup = other.gameObject.GetComponent<Pickup>();	
			GetPickup(triggeredPickup);	
		}

    }
	
	
	
	
	/* Handle any additional logic that the player may need to. */
	private void HandleExtraLogic() {
		
		/* Drop pickup if hitting action button and holding one. */
		if (Input.GetButton("Action1") && heldPickup && IsPossibleToDropHeldPickup()) {
			DropPickup();	
		}
		
		
		/* Decrement cooldown time remaining for interacting with pickups. */
		pickupCooldownTimeRemaining -= Time.deltaTime;
		pickupCooldownTimeRemaining = Math.Max(0.0f, pickupCooldownTimeRemaining);
	}
	
	
	
	
	/* Can the player pick obj up? */
	private bool IsPossibleToPickup(GameObject obj) {
		 return obj.gameObject.CompareTag("Pickup") && !heldPickup && pickupCooldownTimeRemaining == 0;
	}
	
	
	
	
	/* Can the player drop the currently held pickup? */
	private bool IsPossibleToDropHeldPickup() {
		if (!heldPickup) {
			throw new Exception("Calling IsPossibleToDropHeldPickup() when Player has no held pickup!");	
		}
		return pickupCooldownTimeRemaining == 0;	
	}
	
	
	
	
	/* Pick up pickup.*/
	private void GetPickup(Pickup pickup) {
		Debug.Log(this.name + ": Picking " + pickup.gameObject.name + " up.");
		
		heldPickup = pickup;
		pickup.OnObtain(this);	
		
		pickupCooldownTimeRemaining = pickupCooldown;
	}
	
	
	
	
	/* Drop held pickup. */
	private void DropPickup() {
		
		Debug.Log(this.name + ": Setting " + heldPickup.gameObject.name + " down.");
		
		heldPickup.OnRelease();	
		heldPickup = null;
		
		pickupCooldownTimeRemaining = pickupCooldown;
	}
	
	
	
	
	
	/* Get the next direction that the player is going in. */
	private Direction NextDirection() { 
		
		float horizontalAxis = Input.GetAxis("Horizontal");
		Direction nextDirection;
		
		/* If the player isn't pressing left or right. */
		if (horizontalAxis == 0) {
			/* If the character is in the middle of turning left to right. */
			if (currentDirection == Direction.TURNING_LEFT_TO_RIGHT) {
				nextDirection = Direction.RIGHT;	
			}
			/* If the character is in the middle of turning right to left. */
			else if (currentDirection == Direction.TURNING_RIGHT_TO_LEFT) {
				nextDirection = Direction.LEFT;	
			}
			/* Otherwise, remain in the same direction as the last update. */
			else {
				nextDirection = currentDirection;
			}
		}
		/* If the player is pressing left. */
		else if (horizontalAxis < 0) {
			/* If last direction was right and currently going left, mark as turning right to left. */
			if (currentDirection == Direction.RIGHT) {
				nextDirection = Direction.TURNING_RIGHT_TO_LEFT;	
			}
			else {
				nextDirection = Direction.LEFT;	
			}
		}
		/* If the player is pressing right. */
		else if (horizontalAxis > 0) {
			/* If last direction was left and currently going right, mark as turning left to right. */
			if (currentDirection == Direction.LEFT) {
				nextDirection = Direction.TURNING_LEFT_TO_RIGHT;	
			}
			else {
				nextDirection = Direction.RIGHT;	
			}
		}
		/* If you hit here, you probably fucked up somewhere. */
		else {
			throw new Exception("Invalid direction state!");	
		}
		return nextDirection;

	} // end NextDirection()
	
	
	
	
}
