using UnityEngine;
using System;
using System.Collections;


public class Portal : MonoBehaviour {
	
	public Player player; // Reference to the player
	public float activationDistance = 300.0f;
	
	
	// Use this for initialization
	void Start () {
		if (!player) {
			throw new Exception("Player is has not been assigned in '" + this.name + "'!");	
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Warp player to other time period if he gets close to the portal
		if (Vector3.Distance(this.transform.position, player.transform.position) < activationDistance) {
			Debug.Log ("Close enough to teleport.");
			
			// Warp from Past to Future
			if (player.currentTime == Player.playerTimes.PAST) {
				Debug.Log ("In the Past, going to the Future.");
				Application.LoadLevel("test_portal_001");
				player.currentTime = Player.playerTimes.FUTURE;
			}
			// Warp from Future to Past
			else if (player.currentTime == Player.playerTimes.FUTURE) {
				Debug.Log ("In the Future, going to the Past.");
				Application.LoadLevel("test_portal_000");
				player.currentTime = Player.playerTimes.PAST;
			}
			else {
				throw new Exception("Bad time period in '" + this.name + "'");	
			}
		}
	}
}
