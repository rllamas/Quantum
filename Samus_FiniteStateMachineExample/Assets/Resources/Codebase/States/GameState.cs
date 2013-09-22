/*  
 *  Game State Abstract Class
 * 
 *  Written By: Russell Jahn
 * 
 */

namespace GameFramework.States {
	
	/* A GameState can represent the state of any notion in the game, including
	 * the state of character animations, AI, menu state, game options, ect. */
	public abstract class GameState {
		
		/* Any logic that the GameState should carry out. This might include updating 
		 * aspects of the current state. */
		public abstract void Logic();
		
		/* Used to retrieve the state that follows the current one.  Only a finite number
		 * of states can result from the current GameState, and the current GameState 
		 * should know what these states are and determine the correct state to follow it.
		 * The current state should be provided with enough information to do this. */
		public abstract GameState NextState();
		
		/* Returns a string representation of the GameState. */
		public override string ToString() {
			return "[GameState:" + this.GetType().Name + "]";
		}
	}
		
}