using UnityEngine;
using System;
using System.Collections;
using Quantum.States;

public class Player : MonoBehaviour {
	
	
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
	
	
	
	
	void Start () {
		currentDirection = Direction.LEFT;
		previousDirection = Direction.LEFT;
		
		currentState = new ProfessorStandingState(this);
		previousState = new ProfessorStandingState(this);
		
		sprite = GetComponent<tk2dSprite>();
		if (!sprite) {
			throw new Exception("No tk2dSprite was attached to the Player!!!");	
		}
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
		
		handleOtherActions();
	}
	
	
	
	
	void handleOtherActions() {
	
		/* 
		 *  if professor is next to anything pickupable
		 * 		do professor-side pickup logic (attach object to prof)
		 * 		on object, call onPickup()
		 * 
		 * 
		 * */
		
	}
	
	
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Player: Entering collision with a pickup.");
    }
	
	void OnCollisionExit(Collision collision) {
		Debug.Log ("Player: Exiting collision with a pickup.");
	}
		
	
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Player: Entering triggering range of pickup.");
    }
	
	void OnTriggerStay(Collider other) {
		Debug.Log ("Player: Currently in triggering range of pickup.");
    }
	
	void OnTriggerExit(Collider other) {
		Debug.Log ("Player: Leaving triggering range of pickup.");
	}
	
}
