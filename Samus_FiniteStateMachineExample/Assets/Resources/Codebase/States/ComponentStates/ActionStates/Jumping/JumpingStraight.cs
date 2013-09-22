using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameFramework.States;
using GameFramework.Animation;

namespace GameFramework.States {
	
	
	public class JumpingStraight : ActionState {
	
		#region Internal variables.
		/* Dictionary containing sub-states for a JumpingStraight State. Names as strings are mapped to states. */
		private Dictionary<string, ActionState> substates;
		
		/* Current sub-state that the jump is in. */
		private ActionState currentJumpState;
		
		
		/* How fast to jump. */
		private float jumpVelocity = 225.0f;
		
		/* Time it should take to reach the apex of a jump. */
		private float jumpTime = 3.0f;
		
		/* The height of the apex of a jump. */
		private float maxJumpHeight = 15.0f;
		
		/* The height of the apex of a jump. */
		private float moveVelocity = 6.5f;
		#endregion
		
		/* Whether the jump has landed or not. */
		private bool landed;
		
		/* Whether currently joumping or not. */
		private bool jumping;
		
		
		public Dictionary<string, ActionState> Substates {
			get {
				return substates;	
			}
		}
		
		public float JumpVelocity {
			get {
				return jumpVelocity;
			}	
		}
		
		public float JumpTime {
			get {
				return jumpTime;
			}	
		}
		
		public float MaxJumpHeight {
			get {
				return maxJumpHeight;
			}	
		}
		
		public float MoveVelocity {
			get {
				return moveVelocity;
			}	
		}
				
		public bool Jumping {
			get {
				return jumping;
			}	
			set {
				jumping = value;	
			}
		}
		
		public bool Landed {
			get {
				return landed;
			}	
			set {
				landed = value;	
			}
		}
		
		// JumpingStraightPreparing
		// JumpingStraightLifting
		// JumpingStraightApex
		// JumpingStraightFalling
		// JumpingStraightLanding
		
		
		/* Constructor. */
		public JumpingStraight (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
			
			substates = new Dictionary<string, ActionState>() {
				{ "JumpingStraightPreparing", 	new JumpingStraightPreparing (AttachedEntityState) },
				{ "JumpingStraightLifting", 	new JumpingStraightLifting   (AttachedEntityState) },
				{ "JumpingStraightApex",		new JumpingStraightApex      (AttachedEntityState) },
				{ "JumpingStraightFalling", 	new JumpingStraightFalling   (AttachedEntityState) },
				{ "JumpingStraightLanding", 	new JumpingStraightLanding   (AttachedEntityState) }
			};
			
			currentJumpState = substates["JumpingStraightPreparing"];
		}
		
		
		public override void Logic () {
			if (!jumping) {
				currentJumpState = substates["JumpingStraightPreparing"];
				landed = false;
				jumping = true;
			}
			
			currentJumpState.Logic();
			currentJumpState = (ActionState)currentJumpState.NextState();
			
		}
		
		
		public override GameState NextState() {
			
			/* When 'JumpingStraightLanding' is finished, it returns 'null' from NextState. */
			if (currentJumpState == null) {
				jumping = false;
				Debug.Log ("JUMPING IS FINISHED!!!");
				return AttachedEntityState.ActionStates["Standing"];
			}
			
			return this;
		}
		
	}
	
	
}