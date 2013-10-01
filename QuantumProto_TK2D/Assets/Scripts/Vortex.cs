using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vortex : MonoBehaviour {
	public GameObject past_level;
	public GameObject future_level;
	
	// If set to true then level starts in the past
	public bool isPast = true;
	
	
	public float radiusOfVortex = 2.5f;
	
	// Determines how much time passes before vortex becomes usable again
	private float nextUseTime = 0f;
	public float rechargeRate = 2f;
	
	public List<GameObject> itemsInScene;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find("Player");
		
		// Checks to see if player is in vicinity of portal and has pressed action button
		if ( ((player.transform.position.x <= transform.position.x + radiusOfVortex)
				&& (player.transform.position.x >= transform.position.x - radiusOfVortex)) 
					&& ((player.transform.position.y <= transform.position.y + radiusOfVortex)
						&& (player.transform.position.y >= transform.position.y - radiusOfVortex))
							&& Input.GetKeyDown(KeyCode.V) && Time.time >= nextUseTime) {
			isPast = !isPast;
			TimeTravel();
			Debug.Log("Ready to Use");
			nextUseTime = Time.time + rechargeRate;
		}
	}
	
	void TimeTravel() {
		if (!isPast) {
			future_level.renderer.enabled = true;
			past_level.renderer.enabled = false;
			for (int i = 0; i < itemsInScene.Count; ++i) {
				itemsInScene[i].renderer.enabled = false;
				itemsInScene[i].transform.GetChild(0).renderer.enabled = true;
			}
		}
		else {
			future_level.renderer.enabled = false;
			past_level.renderer.enabled = true;
			for (int i = 0; i < itemsInScene.Count; ++i) {
				itemsInScene[i].renderer.enabled = true;
				itemsInScene[i].transform.GetChild(0).renderer.enabled = false;
			}
		}		
	}
}
