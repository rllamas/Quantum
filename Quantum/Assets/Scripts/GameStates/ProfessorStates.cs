/*  
 *  Professor Game States
 * 	---------------------
 *  Collection of the GameStates that comprise Professor Quantum.
 * 
 *  Written By: Russell Jahn & Rene Garcia
 * 
 */
using System;
using UnityEngine;
using FarseerPhysics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

namespace Quantum.States {
	
	
	public class ProfessorStandingState : PlayerState {

		
		/* Constructor. */
		public ProfessorStandingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Standing");		
		}
		

		public override void Logic() {
			HandleAnimationDirection();
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player is hitting jump, then next state is jumping. */
			if (Input.GetButtonDown("Jump")) {
				return new ProfessorJumpingState(attachedPlayer);
			}
			/* Otherwise if the player has been pressing left/right long enough, the next state is walking. */
			else if (xAxisTilt != 0.0f) {
				return new ProfessorWalkingState(attachedPlayer);	
			}
			else {
				return this;
			}
		}
		
		
	} // end ProfesorStandingState class
	
	
	
	
	
	public class ProfessorWalkingState : PlayerState {
		
		/* If true, then the player should come to a stop then go the other way. */
		private bool changedDirections = false;
		
		
		/* Constructor. */
		public ProfessorWalkingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Walking");
		}
		
		
		public override void Logic() {
			HandleAnimationDirection();
			
<<<<<<< HEAD
			//Debug.Log ("Player linear velocity: " + attachedPlayer.body.LinearVelocity);
=======
			if (attachedPlayer.currentDirection != attachedPlayer.previousDirection) {
				changedDirections = true;	
			}
>>>>>>> 29345dc7ef45b8e8f5bc2eb699d8c25e8f67cc49
			
			float xAxisTilt = Input.GetAxis("Horizontal");
			FVector2 currentVelocity = attachedPlayer.body.LinearVelocity;
			
			/* If player is moving, then apply opposing force until he stops. */
			if (xAxisTilt == 0.0f || changedDirections) {
				attachedPlayer.body.ApplyLinearImpulse(
					new FVector2(-attachedPlayer.walkSlowingRate*currentVelocity.X, 
						0.0f
					)
				);
			}
			/* If the player hasn't hit the max velocity limit, then apply force to move the player. */
			else if (Mathf.Abs(currentVelocity.X) < attachedPlayer.walkingMaxVelocity) {
				
				Debug.Log ("Walking State: Player velocity: " + attachedPlayer.body.LinearVelocity);
				
				/* Move the player based on the tilt of the control stick. */
				FVector2 movement = new FVector2(
					currentVelocity.X + xAxisTilt*attachedPlayer.walkingAcceleration*Time.deltaTime, 
					0.0f
				);
				attachedPlayer.body.ApplyLinearImpulse(movement);
			}
			
			/* If stopped, then reset the player as no longer changing directions. */
			if (currentVelocity.X == 0.0f) {
				changedDirections = false;	
			}
			
		}
		
		
		public override GameState NextState() {
			FVector2 currentVelocity = attachedPlayer.body.LinearVelocity;
			
			/* If player is hitting jump, then next state is jumping. */
			if (Input.GetButtonDown("Jump")) {
				return new ProfessorJumpingState(attachedPlayer);
			}
			/* Otherwise if the player is stationary, then the next state is standing. */
			else if (currentVelocity.X == 0.0f && !changedDirections) {
				return new ProfessorStandingState(attachedPlayer);	
			}
			else {
				return this;
			}
		}
		
		
	} // end ProfesorWalkingState class
	
	
	
	
	
	public class ProfessorJumpingState : PlayerState {
		
		
		/* Constructor. */
		public ProfessorJumpingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Jump Lift");			
		}
		
		
		public override void Logic() {	
			HandleAnimationDirection();
			
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player is touching the ground. */
			if (attachedPlayer.IsGrounded()) {
				FVector2 verticalMovement = new FVector2(0.0f, attachedPlayer.jumpingVelocity);
				attachedPlayer.body.ApplyLinearImpulse(verticalMovement);				
			}
			
			/* Handle left/right movement. */
			FVector2 horizontalMovement = new FVector2(xAxisTilt*attachedPlayer.walkingAcceleration*Time.deltaTime, 0.0f);
			attachedPlayer.body.ApplyLinearImpulse(horizontalMovement);
			
			/* If player releases jump button, then stop jump. */
			//if (Input.GetButtonUp("Jump")) {
			//	attachedPlayer.body.LinearVelocity = new FVector2(0.0f, 0.0f);
			//}
			
			/* If now falling, go ahead and play jump apex animation. */
			if (attachedPlayer.IsFalling()) {
				attachedPlayer.animator.Play("Jump Midair");	
			}
		}
		
		
		public override GameState NextState() {
			if (attachedPlayer.IsFalling()) {
				return new ProfessorFallingState(attachedPlayer);
			}
			else {
				return this;	
			}
		}
		

	} // end ProfesorJumpingState class
	
	
	
	
	
	public class ProfessorFallingState : PlayerState {
		
		
		/* How fast the player should be able to move left or right during falling in relation 
		 * to the player's walking velocity. */
		float fallingMovementRatio = 0.5f;
		
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Jump Landing");		
		}
		
		
		public override void Logic () {
			HandleAnimationDirection();
			
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* Handle left/right movement. */
			FVector2 horizontalMovement = new FVector2(
				fallingMovementRatio*xAxisTilt*attachedPlayer.walkingAcceleration*Time.deltaTime, 
				0.0f
			);
			attachedPlayer.body.ApplyLinearImpulse(horizontalMovement);
		}
		
		
		public override GameState NextState() {
			
			if (attachedPlayer.IsGrounded()) {
				return new ProfessorStandingState(attachedPlayer);
			}
			else {
				return this;	
			}
		}
		
		
	} // end ProfesorFallingState class	
	
	
	
	
}