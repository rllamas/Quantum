using UnityEngine;
using System;
using System.Collections;
using GameFramework.States;
using GameFramework.Animation;


namespace GameFramework.States {
	
	
	public class JumpingStraightFalling : ActionState {
	
		#region Internal variables.
		private SpriteAnimation fallingFacingLeftAnimation;
		private SpriteAnimation fallingFacingRightAnimation;
		 
		bool landed = false;
		#endregion
	
		// JumpingStraightPreparing
		// JumpingStraightFalling
		// JumpingStraightFalling
		// JumpingStraightFalling
		// JumpingStraightLanding
		
		
		/* Constructor. */
		public JumpingStraightFalling (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			fallingFacingLeftAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingLeftFalling"];
			fallingFacingRightAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingRightFalling"];
		}
		
		
		private void Render() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != fallingFacingLeftAnimation) {
				SpriteAnimator.LoadAnimation(fallingFacingLeftAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != fallingFacingRightAnimation) {
				SpriteAnimator.LoadAnimation(fallingFacingRightAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
		}
		
		
		private void Move() {
		
			/* If pressing left, move position of entity to the left. */
			if (Input.GetKey("left")) {
            	AttachedEntityState.AttachedEntity.transform.Translate( 
					new Vector2( Time.deltaTime * 
					((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).MoveVelocity, 0)
				);
       		 }
			
			/* If pressing right, move position of entity to the right. */
			else if (Input.GetKey("right")) {
            	AttachedEntityState.AttachedEntity.transform.Translate( 
					new Vector2( -1 * Time.deltaTime * 
					((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).MoveVelocity, 0)
				);
       		 }
		}
		
		
		
		
		public override void Logic () {
			if (SpriteAnimator.CurrentAnimation != fallingFacingLeftAnimation || 
				SpriteAnimator.CurrentAnimation != fallingFacingRightAnimation) {
				landed = false;	
			}
			
			Move();
			Render();
		}
		
		
		public override GameState NextState () {
			
			//if (AttachedEntityState.AttachedEntity.rigidbody.velocity.y == 0) {
			if (((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).Landed) {
				return ((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).Substates["JumpingStraightLanding"];
			}
			
			return this;
		}
		
	}
	
	
}