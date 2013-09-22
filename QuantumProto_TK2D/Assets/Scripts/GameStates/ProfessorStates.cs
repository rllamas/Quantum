/*  
 *  Professor Game States
 * 	---------------------
 *  Collection of the GameStates that comprise Professor Quantum.
 * 
 *  Written By: Russell Jahn
 * 
 */

namespace Quantum.States {
	

	public class ProfessorStandingState : GameState {
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorStandingState class
	
	
	
	
	public class ProfessorWalkingState : GameState {
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorWalkingState class
	
	
	
	
	public class ProfessorJumpingState : GameState {
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorJumpingState class
	
	
	
	
	public class ProfessorFallingState : GameState {
		
		public override void Logic() {
			// Need to draw professor to the screen
		}
		
		public override GameState NextState() {
			// Need to return the next state (possibly out of many)
			return this;
		}
		
	} // end ProfesorFallingState class
		
}