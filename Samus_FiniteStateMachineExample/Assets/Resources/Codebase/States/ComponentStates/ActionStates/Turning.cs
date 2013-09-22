using GameFramework.Animation;
using GameFramework.Interfaces;
using GameFramework.States;
using System;
using System.Collections;
using UnityEngine;


namespace GameFramework.States {

	
	public class Turning : ActionState {	
		
		
		#region Internal variables.
		private SpriteAnimation turningLeftToRightAnimation;
		private SpriteAnimation turningRightToLeftAnimation;
		#endregion
		
		
		
		/* Constructor. */
		public Turning (EntityState attachedEntityState) : base(attachedEntityState) {
			turningLeftToRightAnimation = AttachedEntityState.SpriteAnimations["turningLeftToRight"];
			turningRightToLeftAnimation = AttachedEntityState.SpriteAnimations["turningRightToLeft"];
			
			//Debug.Log("EntityState 'Turning' is done being constructed.");
		
		}
		
		
		
		private void Render() {
			/* Load animation if just starting to turn, otherwise don't do anything. */
			
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			/* If facing left, load the animation of turning right. */
			if (currentDirection == "turning_left_to_right" && SpriteAnimator.CurrentAnimation != turningLeftToRightAnimation) {
				Debug.Log("In Turning.Render(); Loading 'turningLeftToRightAnimation'!");
				SpriteAnimator.LoadAnimation(turningLeftToRightAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
			/* If facing right, load the animation of turning left. */
			else if (currentDirection == "turning_right_to_left" && SpriteAnimator.CurrentAnimation != turningRightToLeftAnimation) {
				Debug.Log("In Turning.Render(); Loading 'turningRightToLeftAnimation'!");
				SpriteAnimator.LoadAnimation(turningRightToLeftAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
		}
		
		
		/* Set the entity's current direction. */
		private void SetDirection() {
			
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			/* If facing right, then entity should turn left. */
			if (currentDirection == "right") {
				((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection = "turning_right_to_left";
			}
				
			/* If facing left, then entity should turn right. */
			else if (currentDirection == "left") {
				((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection = "turning_left_to_right";
			}
			
			/* If finished turning right to left. */
			else if ((SpriteAnimator.CurrentAnimation.Name == "turningRightToLeft") && SpriteAnimator.IsFinished()) {
				((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection = "left";
			}
			
			/* If finished turning left to right. */
			else if ((SpriteAnimator.CurrentAnimation.Name == "turningLeftToRight") && SpriteAnimator.IsFinished()) {
				((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection = "right";
			}	
		}
		
		
		/* Any logic that the GameState should carry out. This might include updating 
		 * aspects of the current state. */
		public override void Logic() {		
			SetDirection();
			Render();
		}
		
		
		
		/* Used to retrieve the state that follows the current one.  Only a finite number
		 * of states can result from the current GameState, and the current GameState 
		 * should know what these states are and determine the correct state to follow it.
		 * The current state should be provided with enough information to do this. */
		public override GameState NextState() {
			
			Debug.Log("In Turning.NextState(); Checking if next state...");
			
			/* If finished turning right to left. */
			if (SpriteAnimator.IsFinished()) {
				Debug.Log("In Turning.NextState(); Returning 'Standing'!");
				SetDirection();
				return AttachedEntityState.ActionStates["Standing"];
			}	
			
			/* Otherwise, entity is still in the process of turning. */
			else {
				Debug.Log("In Turning.NextState(); Returning 'Turning'!");
				return AttachedEntityState.ActionStates["Turning"];
			}
			
		}
		
	}
	
}
