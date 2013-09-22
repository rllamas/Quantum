/*  
 *  ComponentState Abstract Class
 * 
 *  Written By: Russell Jahn
 * 
 */

using GameFramework.Animation;
using GameFramework.States;
using System.Collections;
using UnityEngine;


namespace GameFramework.States {
	
	/* A component state is a sub state of an entity state. For instance, a character might be currently 
	 * running, have a health bar, a weapon, and armor, all of which would be a component state. */
	public abstract class ComponentState : GameState {
		
		
		/* A component state should be attached to an entity state. */
		public EntityState AttachedEntityState {get; set;}
		
		/* A component state should have access to a SpriteAnimator to draw its state. */
		public SpriteAnimator SpriteAnimator {get; set;}
		
		
		/* Constructor. */
		public ComponentState (EntityState entityStateToAttachTo) {
			AttachedEntityState = entityStateToAttachTo;	
			SpriteAnimator = entityStateToAttachTo.SpriteAnimator;
		}
		
	}
}