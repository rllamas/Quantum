using GameFramework.Animation;
using GameFramework.Interfaces;
using GameFramework.States;
using System;
using System.Collections;
using UnityEngine;


namespace GameFramework.States {
	
	
	public class Standing : ActionState {	
		
		
		#region Internal variables.
		private SpriteAnimation standingLeftAnimation;
		private SpriteAnimation standingRightAnimation;
		#endregion
		
		
		
		/* Constructor. */
		public Standing (EntityState attachedEntityState) : base(attachedEntityState) {
			standingLeftAnimation = AttachedEntityState.SpriteAnimations["facingLeft"];
			standingRightAnimation = AttachedEntityState.SpriteAnimations["facingRight"];
			
			//Debug.Log("EntityState 'Standing' is done being constructed.");
		
		}
		
		
		
		private void Render() {
			
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			Debug.Log(String.Format("In Standing.Render(). currentDirection is: '{0}'", currentDirection));
			
			//Debug.Log("In Standing.Render() Checking if should load " + standingLeftAnimation.Name);
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != standingLeftAnimation) {
				//Debug.Log("In Standing.Render(); Loading 'standingLeftAnimation'!");
				SpriteAnimator.LoadAnimation(standingLeftAnimation);
				SpriteAnimator.Loop = true;
				SpriteAnimator.Play();
			}
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != standingRightAnimation) {
				//Debug.Log("In Standing.Render(); Loading 'standingRightAnimation'!");
				SpriteAnimator.LoadAnimation(standingRightAnimation);
				SpriteAnimator.Loop = true;
				SpriteAnimator.Play();
			}
			
			//SpriteAnimator.UpdateAnimation();
			//Debug.Log(String.Format("In Standing.Render(). Is SpriteAnimator set up correctly? {0}", SpriteAnimator != null));
			
		}
		
		
		
		/* Any logic that the GameState should carry out. This might include updating 
		 * aspects of the current state. */
		public override void Logic() {
			//Debug.Log("In ActionState Standing.Logic()!!!");
			Render();
		}
		
		
		
		/* Used to retrieve the state that follows the current one.  Only a finite number
		 * of states can result from the current GameState, and the current GameState 
		 * should know what these states are and determine the correct state to follow it.
		 * The current state should be provided with enough information to do this. */
		public override GameState NextState() {
			
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			/* If pressing left... */
			if (Input.GetKey("left")) {
				/* If facing to the left & pressing left, then start walking to the left... */
				if (currentDirection == "left") {
					return AttachedEntityState.ActionStates["Walking"];
				}
				/* If facing to the right & pressing left, start turning around to the left... */
				else if (currentDirection == "right") {
					return AttachedEntityState.ActionStates["Turning"];
				}
			}
				
			/* If pressing right... */
			else if (Input.GetKey("right")) {
				/* If facing to the right & pressing right, then start walking to the right... */
				if (currentDirection == "right") {
					return AttachedEntityState.ActionStates["Walking"];
				}
				/* If facing to the left & pressing right, start turning around to the right... */
				else if (currentDirection == "left") {
					return AttachedEntityState.ActionStates["Turning"];
				}
			}
			
			/* Else if jumping from a straight position... */
			else if (Input.GetButton("Jump")) {
				return AttachedEntityState.ActionStates["JumpingStraight"];
			}
			
			/* Otherwise, you're just standing there. */
			return this;
			
		}
		
	}
	
}
