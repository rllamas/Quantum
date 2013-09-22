/*  
 *  Location State Class
 * 
 *  Written By: Russell Jahn
 * 
 */

using System;
using UnityEngine;
using GameFramework.Entities;
using GameFramework.States;

namespace GameFramework.States
{
	/* State of an Entity's position and direction.  Store both current and previous values. */
	public class LocationComponent : ComponentState
	{
		Vector3 previousDirection;
		Vector3 currentDirection;
		
		float previousX {get; set;}
		float previousY {get; set;}
		float previousZ {get; set;}
		
		float currentX {get; set;}
		float currentY {get; set;}
		float currentZ {get; set;}
		
		//public override EntityState AttachedEntityState {get; set;}
		
		public LocationComponent(EntityState entityStateToAttachTo) : base(entityStateToAttachTo) {
		}
		
		
		public Vector3 GetPreviousDirection() {
			return new Vector3(previousDirection.x, previousDirection.y, previousDirection.z);	
		}
		
		public void SetPreviousDirection(Vector3 xyz) {
			previousDirection.x = xyz.x;
			previousDirection.y = xyz.y;
			previousDirection.z = xyz.z;
		}
		
		/* Defaults z to 0. */
		public void SetPreviousDirection(Vector2 xy) {
			previousDirection.x = xy.x;
			previousDirection.y = xy.y;
			previousDirection.z = 0;
		}
		
		
		
		public Vector3 GetCurrentDirection() {
			return new Vector3(currentDirection.x, currentDirection.y, currentDirection.z);	
		}
		
		public void SetCurrentDirection(Vector3 xyz) {
			currentDirection.x = xyz.x;
			currentDirection.y = xyz.y;
			currentDirection.z = xyz.z;
		}
		
		/* Defaults z to 0. */
		public void SetCurrentDirection(Vector2 xy) {
			currentDirection.x = xy.x;
			currentDirection.y = xy.y;
			currentDirection.z = 0;
		}
		
		
		
		public Vector3 GetPreviousXYZ() {
			return new Vector3(previousX, previousY, previousZ);	
		}
		
		public void SetPreviousXYZ(float x, float y, float z) {
			previousX = x;	
			previousY = y;
			previousZ = z;
		}
		
		public void SetPreviousXYZ(Vector3 xyz) {
			previousX = xyz.x;	
			previousY = xyz.y;
			previousZ = xyz.z;
		}
		
		/* Z is defaulted to 0.*/
		public void SetPreviousXYZ(Vector2 xy) {
			previousX = xy.x;	
			previousY = xy.y;
			previousZ = 0;
		}
		
		
		
		public Vector3 GetCurrentXYZ() {
			return new Vector3(currentX, currentY, currentZ);	
		}
		
		public void SetCurrentXYZ(float x, float y, float z) {
			currentX = x;	
			currentY = y;
			currentZ = z;
		}
		
		public void SetCurrentXYZ(Vector3 xyz) {
			currentX = xyz.x;	
			currentY = xyz.y;
			currentZ = xyz.z;
		}
		
		/* Z is defaulted to 0.*/
		public void SetCurrentXYZ(Vector2 xy) {
			currentX = xy.x;	
			currentY = xy.y;
			currentZ = 0;
		}
		
		
		
		/* Updates position and direction. */
		public override void Logic() {
			
			//SetPreviousXYZ(GetCurrentXYZ);
			//SetCurrentXYZ(attachedEntityState.attachedEntity.gameObject.transform.position);
		}
		
		
		/* Returns itself; a LocationState has no other states that it should lead to. */
		public override GameState NextState() {
			return this;
		}
		
	}
}

