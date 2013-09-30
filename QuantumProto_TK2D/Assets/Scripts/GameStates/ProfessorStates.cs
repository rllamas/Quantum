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


namespace Quantum.States {
	
	
	
	public class ProfessorStandingState : PlayerState {
		
		/* Constructor. */
		public ProfessorStandingState(Player player) : base(player) {
			attachedPlayer.animator.Play("Standing");		
		}
		
		public override void Logic() {
			/* Animation should be facing left if player is moving left. */
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				attachedPlayer.animator.Sprite.FlipX = true;
			}
			/* Animation should be facing right if player is moving right. */
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.animator.Sprite.FlipX = false;
			}
			
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
			/* Animation should be facing left if player is moving left. */
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				attachedPlayer.animator.Sprite.FlipX = true;
			}
			/* Animation should be facing right if player is moving right. */
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.animator.Sprite.FlipX = false;
			}
			
			/* Move the player based on the tilt of the control stick. */
			float xAxisTilt = Input.GetAxis("Horizontal");
			Vector2 movement = new Vector2(xAxisTilt * attachedPlayer.walkingVelocity * Time.deltaTime, 0);
			
			attachedPlayer.transform.Translate(movement);
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
			;		
		}
		
		public override void Logic() {	
			
			/* If player is touching the ground. */
			if (attachedPlayer.IsGrounded()) {
				
				Vector2 movement;
				float xAxisTilt = Input.GetAxis("Horizontal");
				
				movement = new Vector2(
					xAxisTilt * (attachedPlayer.walkingVelocity+attachedPlayer.jumpingVelocity), 
					attachedPlayer.jumpingVelocity
				);
				attachedPlayer.rigidbody.AddForce(movement);
				
			}
			/* If player releases jump button, then stop jump. */
			else if (Input.GetButtonUp("Jump")) {
				attachedPlayer.rigidbody.velocity = Vector2.zero;
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
		 * to the player's walking velocty. */
		float fallingMovementRatio = 0.5f;
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			;		
		}
		
		public override void Logic () {
			/* Move the player based on the tilt of the control stick. */
			float xAxisTilt = Input.GetAxis("Horizontal");
			Vector2 movement = new Vector2(xAxisTilt * attachedPlayer.walkingVelocity * 
				fallingMovementRatio * Time.deltaTime, 0);
			
			attachedPlayer.transform.Translate(movement);
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