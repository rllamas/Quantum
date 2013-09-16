using UnityEngine;
using System.Collections;

public class script_player : MonoBehaviour {
	
	// public
	public string enemyGroupTagName = "enemy";
	public float clickDepthLimit = 100f;
	
	public int playerScore = 0;
	public int scoreNeededToWin = 200;
	
	public float remainingTime = 10.0f;	

	private float loadWaitTime = 1.5f;
	
	
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("CountdownRemainingTime", 1.0f, 1.0f);
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0)) {
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			// Send out a ray checking for objects hit by a mouse click
			if (Physics.Raycast(ray, out hit, clickDepthLimit)) {
				
				// If you hit the enemy with a mouse click
				if (hit.transform.tag == enemyGroupTagName) {
					
					// Get enemy's script
					script_enemy enemyScript = hit.transform.GetComponent("script_enemy") as script_enemy;
					
					// Damage him
					enemyScript.damage();
					
					// If he's dead, reap the points from his soul
					if (enemyScript.isDead()) {
						playerScore += enemyScript.deathPoints;
					}
				}
			}
		}
	}
	
	
	
	// Decrement game time counter
	void CountdownRemainingTime () {
		
		if (remainingTime <= 0) {
			CancelInvoke("CountdownRemainingTime");
			StartCoroutine(HandleWinLose());
		}
		else {
			--remainingTime;	
		}
	}
	
	
	
	// Handle loading win/loss screen
	IEnumerator HandleWinLose() {
		// Loading timeout
		yield return new WaitForSeconds(loadWaitTime);
		
		if (playerScore >= scoreNeededToWin) {
			Application.LoadLevel("scene_win_game");
		}
		else {
			Application.LoadLevel("scene_lose_game");
		}
	}
	
	
	
	void OnGUI () {
		GUI.Label(new Rect(10, 10, 150, 20), "Score: " + playerScore);
		GUI.Label(new Rect(10, 25, 150, 35), "Time Remaining: " + remainingTime);
	}
}
