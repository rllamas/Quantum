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
			;		
		}
		
		public override void Logic() {
			/* Sprite should be facing left if player is moving left. */
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				attachedPlayer.sprite.FlipX = false;
			}
			/* Sprite should be facing right if player is moving right. */
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.sprite.FlipX = true;
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
			;		
		}
		
		public override void Logic() {
			/* Sprite should be facing left if player is moving left. */
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				attachedPlayer.sprite.FlipX = false;
			}
			/* Sprite should be facing right if player is moving right. */
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.sprite.FlipX = true;
			}
			
			/* Move the player based on the . */
			float xAxisAmount = Input.GetAxis("Horizontal");
			Vector2 movement = new Vector2(xAxisAmount * attachedPlayer.walkingVelocity * Time.deltaTime, 0);
			
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
				
				float xAxisAmount = Input.GetAxis("Horizontal");
				float yAxisAmount = Input.GetAxis("Vertical");
				
				movement = new Vector2(
					xAxisAmount * (attachedPlayer.walkingVelocity+attachedPlayer.jumpingVelocity), 
					attachedPlayer.jumpingVelocity
				);
				attachedPlayer.rigidbody.AddForce(movement);
				
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
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			;		
		}
		
		public override void Logic () {
			;
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