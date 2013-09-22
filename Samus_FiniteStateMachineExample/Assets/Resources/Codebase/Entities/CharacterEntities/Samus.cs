using UnityEngine;
using System.Collections;
using GameFramework.Animation;
using GameFramework.Entities;
using GameFramework.States;
using System;


namespace GameFramework.Entities {
	
	
	/* Script to attach to Samus character. */
	public class Samus : Entity {
		
		
		// Use this for initialization
		void Start () {
			
			/* Ensure there is exactly one Samus script instance. */
			if (GameObject.FindGameObjectsWithTag("Samus").Length != 1) {
				throw new Exception ("Should be exactly 1 instance of Samus GameObject but found '" + 
					GameObject.FindGameObjectsWithTag("Samus").Length + "'!");
			}
			
			/* Set up Sprite Animator for Samus. */
			AttachedGameObject = GameObject.FindGameObjectWithTag("Samus");
			SpriteAnimator = (SpriteAnimator)AttachedGameObject.AddComponent("SpriteAnimator");
			SpriteAnimator.ObjectToAnimate = AttachedGameObject;
			
			/* Set up the state representing Samus. */
			EntityState = new SamusState(this);
		}
		
		
		// Update is called once per frame
		void Update () {
			EntityState.NextState();
			EntityState.Logic();
		}
		
		
		void OnCollisionEnter(Collision collision) {
			if (collision.gameObject.name == "Terrain" && ((JumpingStraight)EntityState.ActionStates["JumpingStraight"]).Jumping) {
				Debug.Log("SAMUS HIT THE GROUND!!!");
				((JumpingStraight)EntityState.ActionStates["JumpingStraight"]).Landed = true;
			}
		}
		
	}
	
	
}
