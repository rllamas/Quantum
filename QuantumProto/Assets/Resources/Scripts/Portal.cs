using UnityEngine;
using System;
using System.Collections;


public class Portal : MonoBehaviour {
	
	public Player player; // Reference to the player
	public float activationDistance = 100.0f;
	
	
	// Use this for initialization
	void Start () {
		
		//if (!player) {
		//	throw new Exception("Player is has not been assigned in '" + this.name + "'!");	
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
		player = (Player)GameObject.Find("Player").GetComponent<Player>();
		float playerDistanceFromPortal = Vector3.Distance(this.transform.position, player.transform.position);
		
		// Anytime you move out of activation range, marked player as not just warping.
		if (playerDistanceFromPortal > activationDistance) {
			player.justWarped = false;	
		}
		else {
			// If within activation range but the player just warped, don't warp again.
			if (player.justWarped) {
				return;
			}
			// Warp player to other time period if he gets close to the portal
			else {
				Debug.Log ("Close enough to teleport.");
				
				// Warp from Past to Future
				if (player.currentTime == Player.playerTimes.PAST) {
					Debug.Log ("In the Past, going to the Future.");
					Application.LoadLevel("Tutorial_Future");
					player.currentTime = Player.playerTimes.FUTURE;
				}
				// Warp from Future to Past
				else if (player.currentTime == Player.playerTimes.FUTURE) {
					Debug.Log ("In the Future, going to the Past.");
					Application.LoadLevel("Tutorial_Past");
					player.currentTime = Player.playerTimes.PAST;
				}
				else {
					throw new Exception("Bad time period in '" + this.name + "'");	
				}
				player.justWarped = true;
			}
		}
	}
}
