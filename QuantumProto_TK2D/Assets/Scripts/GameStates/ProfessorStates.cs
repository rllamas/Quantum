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
			/* */
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				attachedPlayer.sprite.FlipX = false;
			}
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.sprite.FlipX = true;
			}
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorStandingState class
	
	
	
	
	public class ProfessorWalkingState : PlayerState {
		
		/* Constructor. */
		public ProfessorWalkingState(Player player) : base(player) {
			;		
		}
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorWalkingState class
	
	
	
	
	public class ProfessorJumpingState : PlayerState {
		
		/* Constructor. */
		public ProfessorJumpingState(Player player) : base(player) {
			;		
		}
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorJumpingState class
	
	
	
	
	public class ProfessorFallingState : PlayerState {
		
		/* Constructor. */
		public ProfessorFallingState(Player player) : base(player) {
			;		
		}
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorFallingState class
		
}