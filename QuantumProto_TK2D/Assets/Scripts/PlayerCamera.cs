using UnityEngine;
using System;
using System.Collections;

/* A PlayerCamera has an attached player that it follows. */
public class PlayerCamera : MonoBehaviour {

	public Player attachedPlayer;
	private Vector3 originalDistanceFromPlayer;
	
	// Use this for initialization
	void Start () {
		if (!attachedPlayer) {
			throw new Exception("There is no Player attached to '" + this.name + "'!");	
		}
		else {
			originalDistanceFromPlayer = this.transform.position - attachedPlayer.transform.position;
		}
	}
	
	// LateUpdate is called once per frame
	void LateUpdate () {
		
		/* Follow the player. */
		if (attachedPlayer) {
			this.transform.position = attachedPlayer.transform.position + originalDistanceFromPlayer;
		}
	}
}
