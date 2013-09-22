/*  
 *  ActionState Abstract Class
 * 
 *  Written By: Russell Jahn
 * 
 */
using UnityEngine;
using System.Collections;
using GameFramework.States;

namespace GameFramework.States {
	
	/* An ActionState is a state representing an action of an entity, such as running,
		walking, jumping, sleeping, thinking, cooking, ect... */
	public abstract class ActionState : ComponentState {
		
		/* Constructor. */
		public ActionState (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {	
			SpriteAnimator = entityStateToAttachTo.SpriteAnimator;
		}
		
	}
}