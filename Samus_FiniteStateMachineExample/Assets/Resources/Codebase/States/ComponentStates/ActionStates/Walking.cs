using UnityEngine;
using System.Collections;
using GameFramework.Animation;
using GameFramework.States;


namespace GameFramework.States {
	
	
	public class Walking : ActionState {
	
		#region Internal variables.
		private SpriteAnimation walkingLeftAnimation;
		private SpriteAnimation walkingRightAnimation;
		
		/* Speed that the entity will walk at. */
		private float walkingVelocity = 100.0f;
		#endregion
	
		
		/* Constructor. */
		public Walking (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			walkingLeftAnimation = AttachedEntityState.SpriteAnimations["walkingLeft"];
			walkingRightAnimation = AttachedEntityState.SpriteAnimations["walkingRight"];
		}
		
		
		/* Any logic that the GameState should carry out. This might include updating 
		 * aspects of the current state. */
		public override void Logic() {
			Move();
			Render();
		}
		
		
		private void Move() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
		
			/* If facing left, move position of entity to the left. */
			if (currentDirection == "left") {
            	AttachedEntityState.AttachedEntity.transform.Translate( new Vector2(-1 * walkingVelocity * Time.deltaTime, 0) );
       		 }
			
			/* If facing right, move position of entity to the right. */
			else if (currentDirection == "right") {
            	AttachedEntityState.AttachedEntity.transform.Translate( new Vector2(walkingVelocity * Time.deltaTime, 0) );
       		 }
		}
			
			
		private void Render() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			/* If the first time walking left, load the walking left animation. */
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != walkingLeftAnimation) {
				Debug.Log("In Walking.Render(); Loading 'walkingLeftAnimation'!");
				SpriteAnimator.LoadAnimation(walkingLeftAnimation);
				SpriteAnimator.Loop = true;
				SpriteAnimator.Play();
			}
			
			/* If the first time walking right, load the walking right animation. */
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != walkingRightAnimation) {
				Debug.Log("In Walking.Render(); Loading 'walkingRightAnimation'!");
				SpriteAnimator.LoadAnimation(walkingRightAnimation);
				SpriteAnimator.Loop = true;
				SpriteAnimator.Play();
			}
		}
	
	
		/* Used to retrieve the state that follows the current one.  Only a finite number
		 * of states can result from the current GameState, and the current GameState 
		 * should know what these states are and determine the correct state to follow it.
		 * The current state should be provided with enough information to do this. */
		public override GameState NextState() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			/* If pressing left... */
			if (Input.GetKey("left")) {
				/* If facing left, keep walking left. */
				if (currentDirection == "left") {
					return this;
				}
				/* If facing right, then start turning right. */
				else if (currentDirection == "right") {
					return AttachedEntityState.ActionStates["Turning"];	
				}
			}
			
			/* If pressing right... */
			else if (Input.GetKey("right")) {
				/* If facing left, start turning left. */
				if (currentDirection == "left") {
					return AttachedEntityState.ActionStates["Turning"];
				}
				/* If facing right, keep walking right. */
				else if (currentDirection == "right") {
					return this;
				}
			}
			
			/* Otherwise, stop walking and stand still. */
			return AttachedEntityState.ActionStates["Standing"];
		}
		
		
	}
	
	
}