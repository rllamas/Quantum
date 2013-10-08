/*  
 *  Professor Game States
 * 	---------------------
 *  Collection of the GameStates that comprise Professor Quantum.
 * 
 *  Written By: Russell Jahn
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
			float horizontalAxis = Input.GetAxis("Horizontal");
			
			/* If player is hitting jump, then next state is jumping. */
			if (Input.GetButtonDown("Jump")) {
				return new ProfessorJumpingState(attachedPlayer);
			}
			/* Otherwise if the player is pressing left or right, then the next state is walking. */
			else if (horizontalAxis != 0) {
				return new ProfessorWalkingState(attachedPlayer);	
			}
			else {
				return this;
			}
		}
		
		
	} // end ProfesorStandingState class
	
	
	
	
	
	public class ProfessorWalkingState : PlayerState {
		
		
		/* Constructor. */
		public ProfessorWalkingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Walking");
		}
		
		
		public override void Logic() {
			HandleAnimationDirection();
			
			/* Move the player based on the tilt of the control stick. */
			float xAxisTilt = Input.GetAxis("Horizontal");
			FVector2 movement = new FVector2(xAxisTilt * attachedPlayer.walkingVelocity * Time.deltaTime, 0);

			attachedPlayer.body.ApplyLinearImpulse(movement);
		}
		
		
		public override GameState NextState() {
			float horizontalAxis = Input.GetAxis("Horizontal");
			
			/* If player is hitting jump, then next state is jumping. */
			if (Input.GetButtonDown("Jump")) {
				return new ProfessorJumpingState(attachedPlayer);
			}
			/* Otherwise if the player is not pressing left or right, then the next state is standing. */
			else if (horizontalAxis == 0) {
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
			
			FVector2 horizontalMovement = new FVector2(xAxisTilt * attachedPlayer.walkingVelocity * Time.deltaTime, 0.0f);
			attachedPlayer.body.ApplyLinearImpulse(horizontalMovement);
			
			/* If player releases jump button, then stop jump. */
			if (Input.GetButtonUp("Jump")) {
				//Debug.Log("Key is up");
				attachedPlayer.body.LinearVelocity = new FVector2(0.0f, 0.0f);
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
			
			FVector2 horizontalMovement = new FVector2(
				fallingMovementRatio * xAxisTilt * attachedPlayer.walkingVelocity * Time.deltaTime, 
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