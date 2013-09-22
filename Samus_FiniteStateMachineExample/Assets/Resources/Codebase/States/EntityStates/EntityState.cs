/*  
 *  Entity State Abstract Class
 * 
 *  Written By: Russell Jahn
 * 
 */

using GameFramework.Animation;
using GameFramework.States;
using GameFramework.Entities;
using GameFramework.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

	
/* Hide warnings of variables assigned but not used. */
#pragma warning disable 0219


namespace GameFramework.States {
	
	/* A class to define the current state of an entity. Entities should have at least two
	 * sub-states, an action state and a location state, and are expected to update them
	 * when necessary. 
	 * 
	 * There are several setup methods that should be called by children to set up their components.
	 * These include:
	 * 
	 * 		SetupSpriteAnimations();
	 *		SetupActionStates();
	 *
	 *
	 * */
    public abstract class EntityState : GameState {
		
		
		/* Entity that this state is attached to. */
        public Entity AttachedEntity {set; get;}
		
		/* The current AI state that the entity is in. */
		public abstract GameState CurrentActionState {set; get;}
				
		/* Reference to the SpriteAnimator of the entity. */
		public abstract SpriteAnimator SpriteAnimator {set; get;}
		
		
		
		/* Collection containing all of the AI states the entity can be in. */
		public Dictionary <string, ActionState> ActionStates {set; get;}
		
		/* Path where this entity's ActionState Xml file is located. */
		protected abstract string ActionStateXmlPath {get;}
		
		
		
		/* Collection containing all of the Property states the entity can be in. */
		public Dictionary <string, PropertyState> PropertyStates {set; get;}
	
		/* Path that this entity's PropertyState Xml file is located. */
		protected abstract string PropertyStateXmlPath {get;}
			 
			
		
		/* All SpriteAnimations that this entity contains. */
		public Dictionary <string, SpriteAnimation> SpriteAnimations {set; get;}
		
		/* Path where this entity's SpriteAnimation Xml file is located. */
		protected abstract string SpriteAnimationXmlPath {get;}
		
			
		
		/* Constructor setting up implementation details. A child class is expected to call this in its
		 * constructor or do the equivalent set up. */
		public EntityState (Entity entityToAttachTo) {
			Debug.Log("Setting up EntityState...");

			AttachedEntity = entityToAttachTo;
			SpriteAnimator = entityToAttachTo.SpriteAnimator;
			
			/* Load EntityStates's SpriteAnimations. */
			SetupSpriteAnimations();
		

			/* Set up EntityStates's ActionStates. */
			SetupActionStates();
			
			
			/* Set up EntityStates's PropertyStates. */
			SetupPropertyStates();

		}
		
		
		
		/* Model for children to adhere to. */
		public override void Logic() {
			UpdateSubStates();
		}	
		
		
		
		/* All children must guarantee to update their sub-states in whichever way they choose. */
		protected abstract void UpdateSubStates();
		
		
		#region Setup Methods.
		/* Load and sets up the Sprite Animations for this entity. */
		protected void SetupSpriteAnimations() {
			SpriteAnimations = new Dictionary<string, SpriteAnimation>();
			XmlParser.ParseSpriteAnimationDataXml(SpriteAnimationXmlPath, SpriteAnimations);	
		}
		
		
		
		/* Sets the Action States up for this entity. */
		protected void SetupActionStates () {
			ActionStates = new Dictionary<string, ActionState>();
			XmlParser.ParseActionStateDataXml(ActionStateXmlPath, ActionStates, this);	
		}
		
		
		
		/* Sets the Property States up for this entity. */
		protected void SetupPropertyStates () {
			PropertyStates = new Dictionary<string, PropertyState>();
			XmlParser.ParsePropertyStateDataXml(PropertyStateXmlPath, PropertyStates, this);	
		}
		#endregion	
		
		
	}
	
}

