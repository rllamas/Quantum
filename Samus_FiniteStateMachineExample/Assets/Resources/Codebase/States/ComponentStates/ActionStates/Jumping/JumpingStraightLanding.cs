using UnityEngine;
using System;
using System.Collections;
using GameFramework.States;
using GameFramework.Animation;


namespace GameFramework.States {
	
	
	public class JumpingStraightLanding : ActionState {
	
		#region Internal variables.
		private SpriteAnimation landingFacingLeftAnimation;
		private SpriteAnimation landingFacingRightAnimation;
		
		#endregion
	
		// JumpingStraightPreparing
		// JumpingStraightLanding
		// JumpingStraightLanding
		// JumpingStraightLanding
		// JumpingStraightLanding
		
		
		/* Constructor. */
		public JumpingStraightLanding (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			landingFacingLeftAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingLeftLanding"];
			landingFacingRightAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingRightLanding"];
		}
		
		
		private void Render() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != landingFacingLeftAnimation) {
				SpriteAnimator.LoadAnimation(landingFacingLeftAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != landingFacingRightAnimation) {
				SpriteAnimator.LoadAnimation(landingFacingRightAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
		}

		
		
		public override void Logic () {
			Debug.Log ("About to call Render() in JumpingStraightLanding!!!");
			Render();
		}
		
		
		public override GameState NextState () {
			
			if (SpriteAnimator.IsFinished()) {
				return null;
			}
			if (Input.GetKey ("left") || Input.GetKey ("right")) {
				return null;	
			}
			
			return this;
		}
		
	}
	
	
}