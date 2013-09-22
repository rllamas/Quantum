using UnityEngine;
using System.Collections;
using GameFramework.States;
using GameFramework.Animation;


namespace GameFramework.States {
	
	
	public class JumpingStraightPreparing : ActionState {
	
		#region Internal variables.
		private SpriteAnimation preparingFacingLeftAnimation;
		private SpriteAnimation preparingFacingRightAnimation;
		#endregion
	
		// JumpingStraightPreparing
		// JumpingStraightLifting
		// JumpingStraightApex
		// JumpingStraightFalling
		// JumpingStraightLanding
		
		
		/* Constructor. */
		public JumpingStraightPreparing (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			preparingFacingLeftAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingLeftPreparing"];
			preparingFacingRightAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingRightPreparing"];
		}
		
		
		private void Render() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != preparingFacingLeftAnimation) {
				SpriteAnimator.LoadAnimation(preparingFacingLeftAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != preparingFacingRightAnimation) {
				SpriteAnimator.LoadAnimation(preparingFacingRightAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
		}
		
		
		public override void Logic () {
			Render();
		}
		
		
		public override GameState NextState () {
			
			if (SpriteAnimator.IsFinished()) {
				return ((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).Substates["JumpingStraightLifting"];
			}
			
			return this;
		}
		
	}
	
	
}