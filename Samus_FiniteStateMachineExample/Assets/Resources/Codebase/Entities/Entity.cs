/*  
 *  Entity Abstract Class
 * 
 *  Written By: Russell Jahn
 * 
 */


using UnityEngine;
using System.Collections;
using GameFramework.Animation;
using GameFramework.States;

namespace GameFramework.Entities {
	
	/* An abstract script class to define the characteristics of an entity.  An entity can be thought of 
	 * as a physical being in a game, such as a player character, an enemy, or even a chess piece. 
	 */
	public abstract class Entity : MonoBehaviour {
	
		/* Reference to the GameObject that this script is attached to. */
		public GameObject AttachedGameObject {get; set;}
		
		/* Reference to the SpriteAnimator component of the GameObject that this script is attached to. */
		public SpriteAnimator SpriteAnimator {get; set;}
		
		/* An EntityState that defines the current state of this entity. */
		public EntityState EntityState {get; set;}
		
		
		/* Use this for initialization */
		void Start () {
		}
		
		/* Everytime the entity is updated, its underlying EntityState will be updated and do any 
		 * logic it needs to do. */
		void Update () {
			EntityState.NextState();
			EntityState.Logic();
		}
	}
}