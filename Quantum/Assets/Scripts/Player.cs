﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Quantum.States;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FVector2 = Microsoft.Xna.Framework.FVector2;

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
	public string currentStateString; // Used for debugging.

	/* Previous game state that the player was in. */
	public GameState previousState;
	public string previousStateString; // Used for debugging.

	/* Current direction that the player is in. */
	public Direction currentDirection;
	
	/* Last direction that the player was in. */
	public Direction previousDirection;

	/* The animation manager of the player. */
	public tk2dSpriteAnimator animator;

	/* The walking speed of the player. */
	public float walkingVelocity = 20.0f;
	
	public float maxAcceleration = 20.0f;
	
	/* The jumping speed of the player. */
	public float jumpingVelocity = 500.0f;

	/* The current pickup that the player is carrying. */
	public Pickup carriedPickup;
	
	/* There's a cooldown time for both picking up and setting down pickups. */
	public float pickupCooldown = 0.25f;
	private float pickupCooldownTimeRemaining = 0.0f;
	
	/* There's a cooldown time for entering portals. */
	public float vortexCooldown = 0.25f;
	private float vortexCooldownTimeRemaining = 0.0f;
	
	public Body body;
	private Body childBody;
	
	private bool grounded = true;
	
	/* The state the action button is currently in. */
	public enum ActionButtonStates {
		NONE,
		CAN_DROP,
		CAN_PICKUP,
		CAN_ACTIVATE_VORTEX,
	};
	
	public ActionButtonStates currentActionButtonState;
	
	void Start () {
		gameObject.tag = "Player";
		
		currentDirection = Direction.RIGHT;
		previousDirection = Direction.RIGHT;
		
		currentState = new ProfessorStandingState(this);
		currentStateString = currentState.ToString();
		previousState = new ProfessorStandingState(this);
		previousStateString = previousState.ToString();
		
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixedRotation = true;
		//body.OnCollision += OnCollisionEvent;
		//body.OnSeparation += OnCollisionSeparation;
		
		/*childBody = transform.FindChild("FootSensor").GetComponent<FSBodyComponent>().PhysicsBody;
		FVector2[] blah = new FVector2[1];
		blah[0] = new FVector2(0f, 0f);
		Vertices vertices = new Vertices(blah);
		PolygonShape shape = new PolygonShape(vertices, 1f);
		shape.SetAsBox(10, 10);
		childBody.CreateFixture(shape);
		childBody.IsSensor = true;
		childBody.OnCollision += OnCollisionEvent;
		//childBody.OnSeparation += OnCollisionSeparation;*/
	
		//animator = GetComponent<tk2dSpriteAnimator>();
		if (!animator) {
			throw new Exception("No tk2dSpriteAnimator was attached to the Player!!!");	
		}
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
	}
	
	void Update () {
		/* Update direction. */
		previousDirection = currentDirection;
		currentDirection = NextDirection();
		
		/* Update game state. */
		previousState = currentState;
		currentStateString = currentState.ToString();
		currentState = currentState.NextState();
		previousStateString = previousState.ToString();
		
		/* Let the current game state do what it needs to do. */
		currentState.Logic();
	
		//HandleExtraLogic();
	}
	
	private bool OnCollisionEvent (Fixture A, Fixture B, Contact contact) {
		Debug.Log ("A: " + A.UserTag + "\nB: " + B.UserTag);
		if (A.UserTag == "childBody" && B.UserTag == "ground" && contact.IsTouching()) {
			Debug.Log("Is Grounded");
			grounded = true;
		}
		else
			grounded = false;
		Debug.Log(grounded);
		return true;
	}
	
	void OnCollisionSeparation(Fixture A, Fixture B) {
		//Debug.Log ("Player: No longer colliding with something.");
	}
		
	
	
	
	void OnTriggerStay(Collider other) {
		/* If other is the collider of an object you can pick up, then pick it up if possible. *
		if (CanPickup(other.gameObject)) {
			if (Input.GetButtonDown("Action1")) {
				Pickup triggeredPickup = other.gameObject.GetComponent<Pickup>();	
				GetPickup(triggeredPickup);	
				currentActionButtonState = ActionButtonStates.CAN_DROP;
			}
			else {
				currentActionButtonState = ActionButtonStates.CAN_PICKUP;	
			}
		}
		/* If other is the collider of a Vortex, then warp if possible. *
		else if (CanWarp(other.gameObject)) {
			if (Input.GetButtonDown("Action1")) {
				Vortex triggeredVortex = other.gameObject.GetComponent<Vortex>();	
				Warp(triggeredVortex);	
			}
			currentActionButtonState = ActionButtonStates.CAN_ACTIVATE_VORTEX;	
		}*/
    }
	
	void OnTriggerExit(Collider other) {
		currentActionButtonState = ActionButtonStates.NONE;
	}	
	
	/* Handle any additional logic that the player may need to. *
	private void HandleExtraLogic() {
		/* If carrying pickup and can drop it... *
		if (CarryingPickup() && CanDropCarriedPickup()) {
			
			/* Drop pickup if applicable. *
			if (Input.GetButton("Action1")) {
				DropPickup();	
				currentActionButtonState = ActionButtonStates.NONE;
			}
			else {
				currentActionButtonState = ActionButtonStates.CAN_DROP;	
			}
		}
		
		/* Decrement cooldown time remaining for interacting with pickups. *
		pickupCooldownTimeRemaining -= Time.deltaTime;
		pickupCooldownTimeRemaining = Math.Max(0.0f, pickupCooldownTimeRemaining);
		
		
		/* Decrement cooldown time remaining for interacting with vortexes. *
		vortexCooldownTimeRemaining -= Time.deltaTime;
		vortexCooldownTimeRemaining = Math.Max(0.0f, vortexCooldownTimeRemaining);
	}
	
	/* Return true if obj is a Vortex. *
	public bool CanWarp(GameObject obj) {
		return obj.gameObject.CompareTag("Vortex") && vortexCooldownTimeRemaining == 0;
	}
	
	/* Warp the player to another era. *
	private void Warp(Vortex vortex) {
		vortex.OnWarp();
		
		vortexCooldownTimeRemaining = vortexCooldown;
	}
	
	/* Return true if the player is carrying a pickup. *
	public bool CarryingPickup() {
		return carriedPickup != null;	
	}

	/* Can the player pick obj up? *
	private bool CanPickup(GameObject obj) {
		 return obj.gameObject.CompareTag("Pickup") && !CarryingPickup() && Vortex.isPast && pickupCooldownTimeRemaining == 0;
	}

	/* Can the player drop the currently held pickup? *
	private bool CanDropCarriedPickup() {
		if (!CarryingPickup()) {
			throw new Exception("Calling CanDropCarriedPickup() when Player has no held pickup!");	
		}
		return pickupCooldownTimeRemaining == 0;	
	}

	/* Pick up pickup.*
	private void GetPickup(Pickup pickup) {
		//Debug.Log(this.name + ": Picking " + pickup.gameObject.name + " up.");
		animator.Play("Plant");
		carriedPickup = pickup;
		pickup.OnPickup(this);	
		
		pickupCooldownTimeRemaining = pickupCooldown;
	}

	/* Drop held pickup. *
	private void DropPickup() {
		
		//Debug.Log(this.name + ": Setting " + carriedPickup.gameObject.name + " down.");
		animator.Play("Plant");
		carriedPickup.OnDrop();	
		carriedPickup = null;
		
		pickupCooldownTimeRemaining = pickupCooldown;
	}

	/* Returns true if the player is touching the ground. */
	public bool IsGrounded() {
		
		/* Extra distance to look past the bottom of the player's collision box. 
		float extraSearchDistance = 0.2f;
		
		if (extraSearchDistance < 0) {
			throw new Exception("extraSearchDistance cannot be negative!");	
		}
		
		/* Shoot a ray from the center of the player to the bottom of his collision box. 
		 * If anything intersects this ray, then the player is considered touching the ground. 
		 * Collisions with trigger colliders aren't counted. 
		float distanceToGround = this.collider.bounds.extents.y + extraSearchDistance;	
		RaycastHit hitInfo;
		
		bool isCollision = Physics.Raycast(this.transform.position, -Vector2.up, out hitInfo, distanceToGround);
		
		return isCollision && !hitInfo.collider.isTrigger; */
		
		return grounded;
	}

	/* Returns true if the player is falling. *
	public bool IsFalling() {
		
		return body.LinearVelocity.Y <= 0 &&
			(previousState.Equals(new ProfessorFallingState(this)) ||
			 currentState.Equals( new ProfessorFallingState(this)) ||
			 previousState.Equals(new ProfessorJumpingState(this)) ||
			 currentState.Equals( new ProfessorJumpingState(this)) );
	}

	/* Returns true if the player is colliding on the left. */
	/* public bool IsCollidingLeft() {
		
		/* Extra distance to look past the the left of the player's collision box. *
		float extraSearchDistance = 0.2f;
		
		if (extraSearchDistance < 0) {
			throw new Exception("extraSearchDistance cannot be negative!");	
		}
		
		float distanceToCheck = this.collider.bounds.extents.x + extraSearchDistance;
		float yOffset = this.collider.bounds.extents.y/2.0f;
		
		Vector3 topRaySource =    new Vector3(transform.position.x, this.transform.position.y+yOffset, this.transform.position.z);
		Vector3 middleRaySource = this.transform.position;
		Vector3 bottomRaySource = new Vector3(transform.position.x, this.transform.position.y-yOffset, this.transform.position.z);
		
		RaycastHit hitInfoTopRay;
		RaycastHit hitInfoMiddleRay;
		RaycastHit hitInfoBottomRay;

		/* Shoot 3 rays from the player to the left of his collision box. 
		 * If anything intersects these rays, then the player is considered colliding on the left. 
		 * Collisions with trigger colliders aren't counted. *
		bool isCollidingTop =    Physics.Raycast(topRaySource,    -Vector2.right, out hitInfoTopRay,    distanceToCheck);
		bool isCollidingMiddle = Physics.Raycast(middleRaySource, -Vector2.right, out hitInfoMiddleRay, distanceToCheck);
		bool isCollidingBottom = Physics.Raycast(bottomRaySource, -Vector2.right, out hitInfoBottomRay, distanceToCheck);
		
		return (isCollidingTop    && !hitInfoTopRay.collider.isTrigger)    || 
			   (isCollidingMiddle && !hitInfoMiddleRay.collider.isTrigger) || 
			   (isCollidingBottom && !hitInfoBottomRay.collider.isTrigger);
	} */

	/* Returns true if the player is colliding on the right. *
	public bool IsCollidingRight() {
				
		/* Extra distance to look past the the right of the player's collision box. *
		float extraSearchDistance = 0.2f;
		
		if (extraSearchDistance < 0) {
			throw new Exception("extraSearchDistance cannot be negative!");	
		}
		
		float distanceToCheck = this.collider.bounds.extents.x + extraSearchDistance;
		float yOffset = this.collider.bounds.extents.y/2.0f;
		
		Vector3 topRaySource =    new Vector3(transform.position.x, this.transform.position.y+yOffset, this.transform.position.z);
		Vector3 middleRaySource = this.transform.position;
		Vector3 bottomRaySource = new Vector3(transform.position.x, this.transform.position.y-yOffset, this.transform.position.z);
		
		RaycastHit hitInfoTopRay;
		RaycastHit hitInfoMiddleRay;
		RaycastHit hitInfoBottomRay;

		/* Shoot 3 rays from the player to the right of his collision box. 
		 * If anything intersects these rays, then the player is considered colliding on the right. 
		 * Collisions with trigger colliders aren't counted. *
		bool isCollidingTop =    Physics.Raycast(topRaySource,    Vector2.right, out hitInfoTopRay,    distanceToCheck);
		bool isCollidingMiddle = Physics.Raycast(middleRaySource, Vector2.right, out hitInfoMiddleRay, distanceToCheck);
		bool isCollidingBottom = Physics.Raycast(bottomRaySource, Vector2.right, out hitInfoBottomRay, distanceToCheck);
		
		return (isCollidingTop    && !hitInfoTopRay.collider.isTrigger)    || 
			   (isCollidingMiddle && !hitInfoMiddleRay.collider.isTrigger) || 
			   (isCollidingBottom && !hitInfoBottomRay.collider.isTrigger);
	}*/
	
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
