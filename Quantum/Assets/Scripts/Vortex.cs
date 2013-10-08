using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vortex : MonoBehaviour {
	public tk2dTileMap pastMap;
	public tk2dTileMap futureMap;
	
	/* If true, then the past is loaded. */
	public static bool isPast = true;
	
	public List<GameObject> itemsInScene;
	
	public ParticleSystem vortexActiveParticles;
	public ParticleSystem vortexInactiveParticles;
	
	public AudioClip warpSound;
	private AudioSource sfxPlayer01;
	private AudioSource sfxPlayer02;
	
	
	
	
	// Use this for initialization
	void Start () {
		gameObject.tag = "Vortex";
		
		pastMap = GameObject.Find("Past Map").GetComponent<tk2dTileMap>();
		futureMap = GameObject.Find("Future Map").GetComponent<tk2dTileMap>();
		
		vortexActiveParticles.transform.position = this.transform.position;
		vortexInactiveParticles.transform.position = this.transform.position;
		
		vortexActiveParticles.Stop();
		vortexActiveParticles.Clear();
		vortexInactiveParticles.Play();
		
		
		
		pastMap.transform.position = Vector3.zero;
		futureMap.transform.position = Vector3.zero;
		
		if (isPast) {
			futureMap.gameObject.SetActive(false);
		}
		else {
			pastMap.gameObject.SetActive(false);
		}
	}
	
	
	
	
	// Update is called once per frame
	void Update () {

	}
	
	
	
	
	private void TimeTravel() {
		
		/* Going from past to future... */
		if (isPast) {
			pastMap.gameObject.SetActive(false);
			futureMap.gameObject.SetActive(true);
		}
		/* Going from future to past... */
		else {
			pastMap.gameObject.SetActive(true);
			futureMap.gameObject.SetActive(false);
		}		
		
		isPast = !isPast;
	}
	
	
	
	public void OnWarp() {
		Debug.Log (this.name + ": OnWarp().");	
		TimeTravel();

	}
	
	
	
	void OnTriggerEnter(Collider collider) {
		Debug.Log ("Entering range of vortex...");
		vortexInactiveParticles.Stop();
		vortexInactiveParticles.Clear();
		vortexActiveParticles.Play();		
	}
	
	
	
	
	void OnTriggerExit(Collider collider) {
		Debug.Log ("Exiting range of vortex...");
		vortexActiveParticles.Stop();
		vortexActiveParticles.Clear();
		vortexInactiveParticles.Play();
	}
	
	
	
}
