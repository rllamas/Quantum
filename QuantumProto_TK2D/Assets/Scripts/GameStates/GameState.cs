/*  
 *  Game State Abstract Classes
 * 
 *  Written By: Russell Jahn
 * 
 */

using UnityEngine;



namespace Quantum.States {
	
	
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
		
	} // end class GameState
	
	
	
	
	/* A MonoBehaviorState has an attached MonoBehavior. It represents a state of the attached MonoBehavior. */
	public abstract class MonoBehaviourState : GameState {
	
		/* MonoBehavior that is attached to this game state. */
		protected MonoBehaviour attachedMonoBehaviour;
		
		
		/* Constructor. */
		public MonoBehaviourState(MonoBehaviour behaviour) : base() {
			attachedMonoBehaviour = behaviour;
		}	
		
		
		public override abstract void Logic();
		public override abstract GameState NextState();
			
		
		/* Returns a string representation of the MonoBehaviourState. */
		public override string ToString() {
			return "[MonoBehaviourState:" + this.GetType().Name + "]";
		}
		
	} // end class MonoBehaviourState
	
	
	
	
	/* A PlayerState has an attached Player object. It represents a state of the attached Player. */
	public abstract class PlayerState : MonoBehaviourState {
	
		/* Player that is attached to this game state. */
		protected Player attachedPlayer;
		
		
		/* Constructor. */
		public PlayerState(Player player) : base(player) {
			attachedPlayer = player;
		}
		
		
		public override abstract void Logic();
		public override abstract GameState NextState();
		
		
		/* Sets the direction of the player's animation to the player's direction. */
		protected void HandleAnimationDirection() {
			
			/* Animation should be facing left if player is moving left. */
			if (attachedPlayer.currentDirection == Player.Direction.LEFT) {
				attachedPlayer.animator.Sprite.FlipX = true;
			}
			/* Animation should be facing right if player is moving right. */
			else if (attachedPlayer.currentDirection == Player.Direction.RIGHT) {
				attachedPlayer.animator.Sprite.FlipX = false;
			}	
			
		}
		
		
		/* Returns a string representation of the PlayerState. */
		public override string ToString() {
			return "[PlayerState:" + this.GetType().Name + "]";
		}
		
	} // end class PlayerState
		
	
	
}