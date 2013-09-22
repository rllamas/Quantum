using UnityEngine;
using System;
using System.Collections;
using GameFramework.States;
using GameFramework.Animation;


namespace GameFramework.States {
	
	
	public class JumpingStraightApex : ActionState {
	
		#region Internal variables.
		private SpriteAnimation apexFacingLeftAnimation;
		private SpriteAnimation apexFacingRightAnimation;
		
		#endregion
	
		// JumpingStraightPreparing
		// JumpingStraightApex
		// JumpingStraightApex
		// JumpingStraightFalling
		// JumpingStraightLanding
		
		
		/* Constructor. */
		public JumpingStraightApex (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			apexFacingLeftAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingLeftApex"];
			apexFacingRightAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingRightApex"];
		}
		
		
		private void Render() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != apexFacingLeftAnimation) {
				SpriteAnimator.LoadAnimation(apexFacingLeftAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != apexFacingRightAnimation) {
				SpriteAnimator.LoadAnimation(apexFacingRightAnimation);
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
			Move();
			Render();
		}
		
		
		public override GameState NextState () {
			
			if (SpriteAnimator.IsFinished()) {
				return ((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).Substates["JumpingStraightFalling"];
			}
			
			return this;
		}
		
	}
	
	
}