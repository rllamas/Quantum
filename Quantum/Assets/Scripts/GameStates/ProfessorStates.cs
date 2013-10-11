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
			float currentVelocity = attachedPlayer.body.LinearVelocity.X;
			currentVelocity -= Mathf.Min(Mathf.Abs(currentVelocity), attachedPlayer.body.Friction) * Mathf.Sign(currentVelocity);
			attachedPlayer.body.LinearVelocity = new FVector2(currentVelocity, attachedPlayer.body.LinearVelocity.Y);
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player is hitting jump, then next state is jumping. */
			if (Input.GetButtonDown("Jump")) {
				return new ProfessorJumpingState(attachedPlayer);
			}
			/* Otherwise if the player is pressing left or right, then the next state is walking. */
			else if (xAxisTilt != 0.0f) {
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
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			//Debug.Log ("Player linear velocity: " + attachedPlayer.body.LinearVelocity);
			
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 impulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			attachedPlayer.body.ApplyLinearImpulse(impulse);
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player is hitting jump, then next state is jumping. */
			if (Input.GetButtonDown("Jump")) {
				return new ProfessorJumpingState(attachedPlayer);
			}
			/* Otherwise if the player is not pressing left or right, then the next state is standing. */
			else if (xAxisTilt == 0.0f) {
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
				float impulse = attachedPlayer.jumpingVelocity * attachedPlayer.body.Mass;
				FVector2 verticalMovement = new FVector2(0.0f, impulse);
				attachedPlayer.body.ApplyLinearImpulse(verticalMovement);				
			}
			
			/* Handle left/right movement. */
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 horizImpulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			//FVector2 horizontalMovement = new FVector2(xAxisTilt * attachedPlayer.walkingVelocity * Time.deltaTime, 0.0f);
			attachedPlayer.body.ApplyLinearImpulse(horizImpulse);
			
			/* If player releases jump button, then stop jump. */
			//if (Input.GetButtonUp("Jump")) {
			//	attachedPlayer.body.LinearVelocity = new FVector2(0.0f, 0.0f);
			//}
			
			/* If now falling, go ahead and play jump apex animation. */
			if (attachedPlayer.IsFalling()) {
				attachedPlayer.animator.Play("Jump Midair");	
			}
			
			//Debug.Log ("Player linear velocity: " + attachedPlayer.body.LinearVelocity);
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
		//float fallingMovementRatio = 0.5f;
		
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Jump Landing");		
		}
		
		
		public override void Logic () {
			HandleAnimationDirection();
			
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* Handle left/right movement. */
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 horizImpulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			//FVector2 horizontalMovement = new FVector2(
			//	fallingMovementRatio * xAxisTilt * attachedPlayer.walkingVelocity * Time.deltaTime, 
			//	0.0f
			//);
			attachedPlayer.body.ApplyLinearImpulse(horizImpulse);
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