using UnityEngine;
using System;
using System.Collections;
using GameFramework.States;
using GameFramework.Animation;


namespace GameFramework.States {
	
	
	public class JumpingStraightLifting : ActionState {
	
		#region Internal variables.
		private SpriteAnimation liftingFacingLeftAnimation;
		private SpriteAnimation liftingFacingRightAnimation;
		
		/* When a character jumps, they should be given exactly one push upwards. */
		private bool liftedAlready = false;
		#endregion
	
		// JumpingStraightPreparing
		// JumpingStraightLifting
		// JumpingStraightApex
		// JumpingStraightFalling
		// JumpingStraightLanding
		
		
		/* Constructor. */
		public JumpingStraightLifting (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			liftingFacingLeftAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingLeftLifting"];
			liftingFacingRightAnimation = AttachedEntityState.SpriteAnimations["jumpingStraightFacingRightLifting"];
		}
		
		
		private void Render() {
			string currentDirection = ((Direction)AttachedEntityState.PropertyStates["Direction"]).CurrentDirection;
			
			if (currentDirection == "left" && SpriteAnimator.CurrentAnimation != liftingFacingLeftAnimation) {
				SpriteAnimator.LoadAnimation(liftingFacingLeftAnimation);
				SpriteAnimator.Loop = false;
				SpriteAnimator.Play();
			}
			
			else if (currentDirection == "right" && SpriteAnimator.CurrentAnimation != liftingFacingRightAnimation) {
				SpriteAnimator.LoadAnimation(liftingFacingRightAnimation);
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
		
		
		private void ApplyJump() {
			/* A force for the jump should only be applied once per jump. */
			
			/* Make sure that this is the first frame of this state. */
			if (SpriteAnimator.CurrentAnimation == liftingFacingLeftAnimation || 
				SpriteAnimator.CurrentAnimation == liftingFacingRightAnimation) {
				Debug.Log("In JumpingStraightLifting.ApplyJump(). Failed first 'if'.");
				return;
			}
			
			/* Make sure that you weren't currently turning in midair. If so, then you already started
			 * lifting. */
			if (SpriteAnimator.CurrentAnimation.Name == "jumpingStraightTurningLeftToRight" || 
				SpriteAnimator.CurrentAnimation.Name == "jumpingStraightTurningRightToLeft") {
				Debug.Log("In JumpingStraightLifting.ApplyJump(). Failed second 'if'.");
				return;
			}
			
			JumpingStraight parentJumpState = ((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]);
			
			/* Force to apply to entity to create jump. */
			float forceToApply = (float)(AttachedEntityState.AttachedEntity.rigidbody.mass * parentJumpState.MaxJumpHeight *
				parentJumpState.JumpVelocity / Math.Pow(parentJumpState.JumpTime, 2));
				
			Debug.Log ("ForceToApply: " + forceToApply);
				
			//AttachedEntityState.AttachedEntity.rigidbody.AddForce(Vector3.up * forceToApply);
			AttachedEntityState.AttachedEntity.rigidbody.AddForce( new Vector3(0, forceToApply), 0);
		}
		
		
		public override void Logic () {
			Move();
			ApplyJump();
			Render();
		}
		
		
		public override GameState NextState () {
			
			if (AttachedEntityState.AttachedEntity.rigidbody.velocity.y < 0) {
				return ((JumpingStraight)AttachedEntityState.ActionStates["JumpingStraight"]).Substates["JumpingStraightApex"];
			}
			
			return this;
		}
		
	}
	
	
}