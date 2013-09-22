/*  
 *  SamusState Class
 * 
 *  Written By: Russell Jahn
 * 
 */
using GameFramework.Animation;
using GameFramework.Entities;
using GameFramework.States;
using GameFramework.Utils;
using System.Collections.Generic;
using System;
using UnityEngine;


public class SamusState : EntityState {
	
		/* Reference to SpriteAnimator of Samus script. */
		public override SpriteAnimator SpriteAnimator {get; set;}
	
		#region Required properties to initialize from parent.
		/* The current AI state that the entity is in. */
		public override GameState CurrentActionState {set; get;}
		
		
		/* Path that this entity's ActionState Xml file is located. */
		protected override string ActionStateXmlPath { 
			get {
				return "Xml/ActionStateData/action_state_data_samus.xml";
			}
		}
	
	
		/* Path that this entity's PropertyState Xml file is located. */
		protected override string PropertyStateXmlPath { 
			get {
				return "Xml/PropertyStateData/property_state_data_samus.xml";
			}
		}
	
		
		/* Path where this entity's SpriteAnimation Xml file is located. */
		protected override string SpriteAnimationXmlPath { 
			get {
				return "Xml/AnimationData/animation_data_samus_test.xml";
			}
		}
		
		#endregion
		
	
	
		/* Constructor. */
		public SamusState (Entity entityToAttachTo) : base(entityToAttachTo) {
		
			Debug.Log("Setting up SamusState...");
		
			CurrentActionState = ActionStates["Standing"];
		
			/* Set initial direction as facing left. */
			((Direction)PropertyStates["Direction"]).CurrentDirection = "left";
			//Debug.Log("currentActionState of SamusState = " + CurrentActionState);
			//Debug.Log("((Direction)PropertyStates[Direction]).CurrentDirection:" +  
			//	((Direction)PropertyStates["Direction"]).CurrentDirection);
		}
	
	
	
		/* Updates all of this state's sub-states.*/
		protected override void UpdateSubStates() {
			CurrentActionState.Logic();
		}
	
	
	
		/* Updates this state and all of its sub-states. */
		public override void Logic() {
			//Debug.Log("Current Direction of Samus: '" + ((Direction)PropertyStates["Direction"]).CurrentDirection + "'");
			CurrentActionState = CurrentActionState.NextState();
			UpdateSubStates();
		}
		
	
	
		/* Returns itself, as a SamusState doesn't have any other states that should branch from it. */
		public override GameState NextState() {
			return this;
		}

}
