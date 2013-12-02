using UnityEngine;
using System.Collections;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;

public abstract class PlayerEvent : MonoBehaviour {
	
	protected Player player;
	
	void Awake() {
		gameObject.tag = "Player Event";	
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
	}
	
	
	/* The Player object is expected to call this method when activating up this object. */
	public virtual void OnActivate(Player player) {
		//Debug.Log (this.name + ": OnActivate().");		
	}
		
	
	

}
