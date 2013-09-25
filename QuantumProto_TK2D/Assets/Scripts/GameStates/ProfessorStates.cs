/*  
 *  Professor Game States
 * 	---------------------
 *  Collection of the GameStates that comprise Professor Quantum.
 * 
 *  Written By: Russell Jahn
 * 
 */

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
			
			/* Move the player based on direction. */
			Vector2 movement = new Vector2(attachedPlayer.walkingVelocity * Time.deltaTime, 0);
			
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				/* If going left, then make x translation negative. */
				movement.x *= -1;
				attachedPlayer.transform.Translate(movement);
			}
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.transform.Translate(movement);
			}
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
			Vector2 movement;
			/* If the last state was standing, then jump straight up. */
			if (attachedPlayer.previousState.ToString() == "[PlayerState:ProfessorStandingState]") {
				Debug.Log ("Jumping straight up.");
				movement = new Vector2(0, 200f * Time.smoothDeltaTime);
				attachedPlayer.transform.Translate(movement);
			}
			else if (attachedPlayer.previousState.ToString() == "[PlayerState:ProfessorWalkingState]") {
				Debug.Log ("Jumping at a diagonal.");
				movement = new Vector2(attachedPlayer.walkingVelocity * Time.deltaTime, 200f * Time.smoothDeltaTime);
				attachedPlayer.transform.Translate(movement);
			}
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return new ProfessorFallingState(attachedPlayer);
		}
		
	} // end ProfesorJumpingState class
	
	
	
	
	public class ProfessorFallingState : PlayerState {
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			;		
		}
		
		public override void Logic() {
			// Need to draw professor to the screen
			//Vector2 movement = new Vector2(0, -200f * Time.smoothDeltaTime);
			//attachedPlayer.transform.Translate(movement);
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return new ProfessorStandingState(attachedPlayer);
		}
		
	} // end ProfesorFallingState class
		
}