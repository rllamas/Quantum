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
			if (player.CarryingPickup()) {
				attachedPlayer.animator.Play("Standing Carry");
			}
			else {
				attachedPlayer.animator.Play("Standing");		
			}
		}
		
		
		public override void Logic() {
			/* If player isn't allow to move, then just stay frozen until he can. */
			if (!attachedPlayer.canMove) {
				return;
			}
			
			HandleAnimationDirection();
			
			/* Add friction. */
			float currentVelocity = attachedPlayer.body.LinearVelocity.X;
			currentVelocity -= Mathf.Min(Mathf.Abs(currentVelocity), attachedPlayer.body.Friction) * Mathf.Sign(currentVelocity);
			attachedPlayer.body.LinearVelocity = new FVector2(currentVelocity, attachedPlayer.body.LinearVelocity.Y);
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player isn't allow to move, then just stay in this state until he can. */
			if (!attachedPlayer.canMove) {
				return this;
			}
			/* If player is hitting jump, then next state is jumping. */
			else if (Input.GetButtonDown("Jump")) {
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
			if (player.CarryingPickup()) {
				attachedPlayer.animator.Play("Walking Carry");
			}
			else {
				attachedPlayer.animator.Play("Walking");
			}
		}
		
		
		public override void Logic() {	
			/* If player isn't allow to move, then just stay frozen until he can. */
			if (!attachedPlayer.canMove) {
				return;
			}
			
			HandleAnimationDirection();
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 impulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			attachedPlayer.body.ApplyLinearImpulse(impulse);
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player isn't allow to move, then just stay in this state until he can. */
			if (!attachedPlayer.canMove) {
				return this;
			}
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
		
		private bool isGrounded = true;
		
		/* Constructor. */
		public ProfessorJumpingState(Player player) : base(player) {
			;
		}
		
		
		public bool IsGrounded() {
			return isGrounded;	
		}
		
		
		public override void Logic() {	
			/* If player isn't allow to move, then just stay frozen until he can. */
			if (!attachedPlayer.canMove) {
				return;
			}
			
			HandleAnimationDirection();
			
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player is touching the ground. */
			if (isGrounded) {
				
				/* Play lifting animation. */
				if (attachedPlayer.CarryingPickup()) {
					attachedPlayer.animator.Play("Jump Lift Carry");
				}
				else {
					attachedPlayer.animator.Play("Jump Lift");			
				}
				
				isGrounded = false;
				float impulse = attachedPlayer.jumpingVelocity * attachedPlayer.body.Mass;
				FVector2 verticalMovement = new FVector2(0.0f, impulse);
				attachedPlayer.body.ApplyLinearImpulse(verticalMovement);				
			}
			
			/* Handle left/right movement. */
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 horizImpulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			attachedPlayer.body.ApplyLinearImpulse(horizImpulse);
			
			
		}
		
		
		public override GameState NextState() {
			/* If player isn't allow to move, then just stay in this state until he can. */
			if (!attachedPlayer.canMove) {
				return this;
			}
			else if (attachedPlayer.IsFalling()) {
				return new ProfessorFallingState(attachedPlayer);
			}
			else {
				return this;	
			}
		}
		

	} // end ProfesorJumpingState class
	
	
	
	
	
	public class ProfessorFallingState : PlayerState {
		
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			if (player.CarryingPickup()) {
				attachedPlayer.animator.Play("Jump Landing Carry");
			}
			else {
				attachedPlayer.animator.Play("Jump Landing");		
			}
		}
		
		
		public override void Logic () {
			/* If player isn't allow to move, then just stay frozen until he can. */
			if (!attachedPlayer.canMove) {
				return;
			}
			
			HandleAnimationDirection();
			
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* Handle left/right movement. */
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 horizImpulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			attachedPlayer.body.ApplyLinearImpulse(horizImpulse);
			
			if (attachedPlayer.IsGrounded()) {
				attachedPlayer.animator.Stop();	
			}
		}
		
		
		public override GameState NextState() {
			
			/* If player isn't allow to move, then just stay in this state until he can. */
			if (!attachedPlayer.canMove) {
				return this;
			}
			else if (attachedPlayer.IsGrounded()) {
				return new ProfessorStandingState(attachedPlayer);
			}
			else {
				return this;	
			}
		}
		
		
	} // end ProfesorFallingState class	
	
	
	
	
}