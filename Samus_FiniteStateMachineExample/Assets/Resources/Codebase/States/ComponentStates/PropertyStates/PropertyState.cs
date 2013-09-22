/*  
 *  PropertyState Abstract Class
 * 
 *  Written By: Russell Jahn
 * 
 */
using UnityEngine;
using System.Collections;
using GameFramework.States;

namespace GameFramework.States {
	
	/* An PropertyState is a state representing a property of an entity, such as direction, health,
	 * weapons, armor, ect... */
	public abstract class PropertyState : ComponentState {
		
		/* Constructor. */
		public PropertyState (EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {	
		}
		
	}
}