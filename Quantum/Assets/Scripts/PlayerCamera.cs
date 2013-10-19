using UnityEngine;
using System;
using System.Collections;

/* A PlayerCamera has an attached player that it follows. */
public class PlayerCamera : MonoBehaviour {

	public Player attachedPlayer;
	private Vector3 offsetFromPlayer; // Determined from initial camera position
	
	// Use this for initialization
	void Start () {
		if (!attachedPlayer) {
			throw new Exception("There is no Player attached to '" + this.name + "'!");	
		}
		else {
			offsetFromPlayer = this.transform.position - attachedPlayer.transform.position;
		}
	}
	
	// LateUpdate is called once per frame
	void LateUpdate () {
		
		/* Follow the player. */
		if (attachedPlayer) {
			this.transform.position = attachedPlayer.transform.position + offsetFromPlayer;
		}
	}
}
