using UnityEngine;
using System.Collections;

public class script_enemy : MonoBehaviour {
	
	// public 
	public int numberOfClicks = 2;
	public float respawnTimeout = 2.0f;
	public Color [] respawnColors = 
		{Color.blue, Color.cyan, Color.red, Color.magenta, Color.green, Color.yellow, Color.white, Color.black};
	
	public Transform deathExplosion;
	public int deathPoints = 25;
	
	
	
	// private
	private int totalClicksUntilDeath;
	
	
	
	// Use this for initialization
	void Start () {
		totalClicksUntilDeath = numberOfClicks;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (numberOfClicks <= 0) {
			
			if (deathExplosion) {	
				Instantiate(deathExplosion, transform.position, transform.rotation);
			}
			
			if (this.audio) {
				this.audio.Play();
			}
			
			// Handle respawning
			StartCoroutine(RespawnWaitTime());
			
			// Reposition me after respawning
			int newX = Random.Range(-6, 6);
			int newY = Random.Range(-4, 4);
			Vector3 newPosition = new Vector3(newX, newY, 0);
			this.transform.position = newPosition;
			
			numberOfClicks = totalClicksUntilDeath;
		}
	}
	
	
	
	// Timeout for respawning
	private IEnumerator RespawnWaitTime() {
		renderer.enabled = false;
		RandomRespawnColor();
		yield return new WaitForSeconds(respawnTimeout);
		renderer.enabled = true;
	}
	
	
	
	// Choose a new respawn color
	private void RandomRespawnColor() {
		if (respawnColors.Length <= 0) {
			return;	
		}
		
		int newColorIndex = Random.Range(0, respawnColors.Length-1);
		renderer.material.color = respawnColors[newColorIndex];
	}
	
	
	
	// Hurt this enemy 
	public void damage() {
		--numberOfClicks;	
	}
	
	
	
	// Is this enemy dead?
	public bool isDead() {
		return numberOfClicks == 0;
	}
}
