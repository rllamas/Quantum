/*  
 *  Direction PropertyState Class
 * 
 *  Written By: Russell Jahn
 * 
 */
using GameFramework.Animation;
using GameFramework.Interfaces;
using GameFramework.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.States {
	
	
	/* This is a class to define the direction of an entity. This state just holds data; 
	 * it must be updated by an outside being by through its interface. */
	public class Direction : PropertyState {	
		
		/* Mapping of direction names as strings to vectors that represent them. */
		private Dictionary<string, Vector3> possibleDirections = new Dictionary<string, Vector3>() {
			{ "left", 						new Vector3 ( -1f,   0f,   0f ) },
			{ "right", 						new Vector3 (  1f,   0f,   0f ) },
			{ "turning_left_to_right",		new Vector3 (-.5f,   0f,   0f ) },
			{ "turning_right_to_left", 		new Vector3 ( .5f,   0f,   0f ) },
			{ "null", 						new Vector3 (  0f,   0f,   0f ) }
		};
		
		private string currentDirection;
		
		
		
		/* Constructor. */
		public Direction (EntityState attachedEntityState) : base(attachedEntityState) {
			currentDirection = null;
		}
		
		
		
		 /* Constructor. DIRECTION_AS_STRING sets the initial direction and must be one of: 
		 * 
		 *  "left"
		 *  "right"
		 *  "turning_left_to_right"
		 *  "turning_right_to_left"
		 *  "null" (No Direction)
		 */
		public Direction (EntityState attachedEntityState, string directionAsString) : base(attachedEntityState) {
			if (possibleDirections.ContainsKey(directionAsString)) {
				currentDirection = directionAsString;
			}
			else {
				throw new Exception(String.Format("Invalid argument to Direction constructor: '{0}'", directionAsString));	
			}
		}
		
		
		
		/* Any logic that the GameState should carry out. This might include updating 
		 * aspects of the current state. */
		public override void Logic() {
			// Nothing to carry out.
		}
		
		
		
		/* A Vector3 representing the direction of the entity.
		 * 
		 * 	Vectors can be interpreted as follows: 
		 * 
		 * 	< -1, 0, 0>     Facing Left
		 *  <  1, 0, 0>     Facing Right
		 *  <-.5, 0, 0>     Turning From Left to Right
		 *  < .5, 0, 0>     Turning From Right to Left
		 *  <  0, 0, 0>     null / No Direction
		 * 
		 */
		public Vector3 CurrentDirectionVector {
			get {
				return possibleDirections[currentDirection];
			}
			
	//		set {
	//			if (possibleDirections.ContainsValue(value)) {
	//				currentDirection = possibleDirections.GetKey(value);
	//			}
	//			else {
	//				throw new Exception(String.Format("Invalid argument: '{0}'", value));	
	//			}
	//		}
		}
		
		
		
		/* Returns a string representing the current direction. The possible directions include:
		 * 
		 *  "left"
		 *  "right"
		 *  "turning_left_to_right"
		 *  "turning_right_to_left"
		 *  "null" (No Direction)
		 */
		public string CurrentDirection {
			get {
				return currentDirection;	
			}
			
			set {
				if (possibleDirections.ContainsKey(value)) {
					currentDirection = value;
				}
				else {
					throw new Exception(String.Format("Invalid argument: '{0}'", value));	
				}	
			}
		}
		
		
		
		/* Returns itself, as a Direction PropertyState has no other states it can lead to. */
		public override GameState NextState() {
			return this;		
		}
		
		
	}
	
	
}
