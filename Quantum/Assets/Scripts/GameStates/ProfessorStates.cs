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
		
		private bool carryingLastFrame = false;
		
		/* Constructor. */
		public ProfessorStandingState(Player player) : base(player) {
			
			/* Play standing animation. */
			if (player.CarryingPickup()) {
				attachedPlayer.animator.Play("Standing Carry");
			}
			else {
				attachedPlayer.animator.Play("Standing");		
			}
		}
		
		
		public override void Logic() {
			
			/* Switch animations if picking something up. */
			if (attachedPlayer.CarryingPickup() && !carryingLastFrame) {
				attachedPlayer.animator.Play("Standing Carry");
			}
			else if (!attachedPlayer.CarryingPickup() && carryingLastFrame) {
				attachedPlayer.animator.Play("Standing");		
			}
			
			/* If player isn't allow to move, then just stay frozen until he can. */
			if (!attachedPlayer.canMove) {
				return;
			}
			
			HandleAnimationDirection();
			
			/* Add friction. */
			float currentVelocity = attachedPlayer.body.LinearVelocity.X;
			currentVelocity -= Mathf.Min(Mathf.Abs(currentVelocity), attachedPlayer.body.Friction) * Mathf.Sign(currentVelocity);
			attachedPlayer.body.LinearVelocity = new FVector2(currentVelocity, attachedPlayer.body.LinearVelocity.Y);
			
			carryingLastFrame = attachedPlayer.CarryingPickup();
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player isn't allow to move, then just stay in this state until he can. */
			if (!attachedPlayer.canMove) {
				return this;
			}
			/* If the player is now falling, then the next state is falling. */
			else if (!attachedPlayer.IsGrounded()) {
				return new ProfessorFallingState(attachedPlayer);
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
		
		private bool carryingLastFrame = false;
		
		/* Constructor. */
		public ProfessorWalkingState(Player player) : base(player) {
			
			/* Play walking animation. */
			if (player.CarryingPickup()) {
				attachedPlayer.animator.Play("Walking Carry");
			}
			else {
				attachedPlayer.animator.Play("Walking");
			}
		}
		
		
		public override void Logic() {	
			
			/* Switch animations if picking something up. */
			if (attachedPlayer.CarryingPickup() && !carryingLastFrame) {
				attachedPlayer.animator.Play("Walking Carry");
			}
			else if (!attachedPlayer.CarryingPickup() && carryingLastFrame) {
				attachedPlayer.animator.Play("Walking");		
			}
			
			/* If player isn't allow to move, then just stay frozen until he can. */
			if (!attachedPlayer.canMove) {
				return;
			}
			
			HandleAnimationDirection();
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 impulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			attachedPlayer.body.ApplyLinearImpulse(impulse);
			
			carryingLastFrame = attachedPlayer.CarryingPickup();
		}
		
		
		public override GameState NextState() {
			float xAxisTilt = Input.GetAxis("Horizontal");
			
			/* If player isn't allow to move, then just stay in this state until he can. */
			if (!attachedPlayer.canMove) {
				return this;
			}
			/* If the player is now falling, then the next state is falling. */
			else if (!attachedPlayer.IsGrounded()) {
				return new ProfessorFallingState(attachedPlayer);
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
		private bool releasedJumpButton = false;
		private bool carryingLastFrame = false;
		
		/* Constructor. */
		public ProfessorJumpingState(Player player) : base(player) {
			;
		}
		
		
		public bool IsGrounded() {
			return isGrounded;	
		}
		
		
		public override void Logic() {	
			
			/* Switch animations if picking something up. */
			if (attachedPlayer.CarryingPickup() && !carryingLastFrame) {
				attachedPlayer.animator.Play("Jump Lift Carry");
			}
			else if (!attachedPlayer.CarryingPickup() && carryingLastFrame) {
				attachedPlayer.animator.Play("Jump Lift");		
			}
			
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
				
				/* Play jumping sound. */
				attachedPlayer.sfxPlayer.clip = attachedPlayer.jumpSound;
				attachedPlayer.sfxPlayer.loop = false;
				/* Give some variation to the jump pitch. */
				if (!attachedPlayer.NearVortex()) {
					attachedPlayer.sfxPlayer.pitch = 1.0f + 0.02f*UnityEngine.Random.Range(-11, 6);
				}
				attachedPlayer.sfxPlayer.Play();
			}
			
			if (Input.GetButtonUp("Jump")) {
				releasedJumpButton = true;	
			}
			
			/* If you released the jump button, then player's jump should stop sooner. */
			if (releasedJumpButton) {
				attachedPlayer.body.ApplyLinearImpulse(
					new FVector2(
						0.0f,
						-attachedPlayer.body.LinearVelocity.Y * attachedPlayer.jumpReleaseVelocityFalloffRate
					)
				);
			}
			
			/* Handle left/right movement. */
			float velChange = (xAxisTilt * attachedPlayer.walkingVelocity) - attachedPlayer.body.LinearVelocity.X;
			FVector2 horizImpulse = new FVector2(attachedPlayer.body.Mass * velChange, 0f);
			attachedPlayer.body.ApplyLinearImpulse(horizImpulse);
			
			carryingLastFrame = attachedPlayer.CarryingPickup();
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
		
		private bool carryingLastFrame = false;
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			if (attachedPlayer.CarryingPickup()) {
				attachedPlayer.animator.Play("Jump Midair Carry");
			}
			else {
				attachedPlayer.animator.Play("Jump Midair");		
			}
		}
		
		
		public override void Logic () {
			
			/* Switch animations if picking something up. */
			if (attachedPlayer.CarryingPickup() && !carryingLastFrame) {
				attachedPlayer.animator.Play("Jump Midair Carry");
			}
			else if (!attachedPlayer.CarryingPickup() && carryingLastFrame) {
				attachedPlayer.animator.Play("Jump Midair");		
			}
			
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
			
			/* Play landing animation. */
			if (attachedPlayer.IsGrounded()) {
				if (attachedPlayer.CarryingPickup()) {
					attachedPlayer.animator.Play("Jump Landing Carry");
				}
				else {
					attachedPlayer.animator.Play("Jump Landing");			
				}
			}
			
			carryingLastFrame = attachedPlayer.CarryingPickup();
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