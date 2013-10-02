using UnityEngine;
using System.Collections;

public class OneWayPlatformTrigger : MonoBehaviour {
	
	/*
	 The colliders should be placed like this, with this trigger under the platform,
	 but NOT COMPLETELY BOUNDING the platform.
	 
	 When the player jumps through this trigger, the platform's collider will be disabled, and when the
	 player is above this trigger, the platform's collider will be re-enabled.
	   ___________
	  | platform |
	---------------
	| |_________| |
	|   trigger   |
	---------------
			
	*/
	
	/* The platform for the player to step on. This should be a non-trigger BoxCollider. */
	public BoxCollider platform;	
	
	public Player player;
	
	
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
	}
	
	
	
	
	void OnTriggerEnter() {
		Debug.Log("Entering OneWayTrigger!");
		Physics.IgnoreCollision(player.gameObject.collider, platform.collider, true);
	}
	
	
	
	
	void OnTriggerExit() {
		Debug.Log("Exiting OneWayTrigger()!");
		Physics.IgnoreCollision(player.gameObject.collider, platform.collider, false);
	}
	
	
	
	
}
