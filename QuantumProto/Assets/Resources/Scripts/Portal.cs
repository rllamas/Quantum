using UnityEngine;
using System;
using System.Collections;


public class Portal : MonoBehaviour {
	
	#region private variables
	public Player player; // Reference to the player
	public float activationDistance = 100.0f;
	#endregion
	
	#region private variables
	private bool inTrigger = false;
	#endregion
	
	// Use this for initialization
	void Start () {
		
		//if (!player) {
		//	throw new Exception("Player is has not been assigned in '" + this.name + "'!");	
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
		player = (Player)GameObject.Find("Player").GetComponent<Player>();
		
		// If you're in range and hit up, teleport
		if (inTrigger && Input.GetButton("Vertical")){
			Debug.Log ("Teleport!");
			
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
		}
		
		
	}

	// If you're in range of portal, set variable to true.
	void OnTriggerEnter(Collider other) {
		Debug.Log ("In range of portal");
		inTrigger = true;
    }
	
	// 
	void OnTriggerExit(Collider other) {
		Debug.Log ("Out of range of portal");
		inTrigger = false;
	}
	
}