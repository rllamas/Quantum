using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Vortex : MonoBehaviour {
	private static tk2dTileMap pastMap;
	private static tk2dTileMap futureMap;
	
	#region class globals	
	public static List<Pickup> pickups; 
	private static Player player;
	#endregion
	
	
	public ParticleSystem vortexActiveParticles;
	public ParticleSystem vortexInactiveParticles;
	
	public float particleStartSize = 1.0f;
	
	public AudioClip whenNearbySound;
	public float whenNearbySoundPitch = 1.75f;
	public float whenNearbySoundFadeTime = 1.5f;
	public AudioClip warpSound;
	private AudioSource sfxPlayer01;
	private AudioSource sfxPlayer02;
	
	private Light glow;
	
	public MeshFilter animationCurtain;
	private tk2dCamera mainCamera;
	
	private static bool currentlyWarping;
	
	void Awake() {
		gameObject.tag = "Vortex";	
	}
	
	
	// Use this for initialization
	void Start () {
		
		/* Since these are class variables, only do if not initialized yet. */
		if (!pastMap) {
			pastMap = GameObject.Find("Past Map").GetComponent<tk2dTileMap>();
			futureMap = GameObject.Find("Future Map").GetComponent<tk2dTileMap>();
			
			/* Set the time period. */
			if (LevelManager.IsPast()) {
					futureMap.gameObject.SetActive(false);
			}
			else {
				pastMap.gameObject.SetActive(false);
			}
		}
		
		vortexActiveParticles.transform.position = this.transform.position;
		vortexInactiveParticles.transform.position = this.transform.position;
		
		vortexActiveParticles.Play();
		vortexActiveParticles.startSize = 0.0f;	
		vortexInactiveParticles.Play();
		
		sfxPlayer01 = this.transform.FindChild("SFX Player 01").GetComponent<AudioSource>();
		sfxPlayer02 = this.transform.FindChild("SFX Player 02").GetComponent<AudioSource>();
		
		sfxPlayer01.clip = whenNearbySound;
		sfxPlayer01.loop = true;
		sfxPlayer01.volume = 0.0f;
		sfxPlayer01.Play();
		
		sfxPlayer02.clip = warpSound;
		sfxPlayer02.Pause();
		
		glow = this.transform.FindChild("Point light").GetComponent<Light>();
		
		pastMap.transform.position = Vector3.zero;
		futureMap.transform.position = Vector3.zero;
		
		animationCurtain =  this.transform.FindChild("Curtain").GetComponent<MeshFilter>();
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<tk2dCamera>();
		
		animationCurtain.gameObject.SetActive(true);
		animationCurtain.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		
		/* Since this is a class variable, only do if not initialized yet. */
		if (!player) {
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
		
		/* Since this is a class variable, only do if not initialized yet. */
		if (pickups == null) {
			/* Get all pickups from the scene and put variable pickups. */
			GameObject [] pickupGameObjects = GameObject.FindGameObjectsWithTag("Pickup");
	
			pickups = new List<Pickup>();
			for (int i = 0; i < pickupGameObjects.Length; ++i) {
				pickups.Add( pickupGameObjects[i].GetComponent<Pickup>() );
			}
		}
		
		currentlyWarping = false;
	}
	
	
	
	
	// Update is called once per frame
	void Update () {

	}
	
	
	
	/* Any logic to handle for the actual time travel stage. */
	private void TimeTravel() {
		
		/* Going from past to future... */
		if (LevelManager.IsPast()) {
			/* Switches out maps. */
			pastMap.gameObject.SetActive(false);
			futureMap.gameObject.SetActive(true);
			
			/* Tell every pickup in the scene to do whatever it needs to do when switching eras. */
			for (int i = 0; i < pickups.Count; ++i) {
				pickups[i].HandleChangeEra(TimePeriod.FUTURE);
			}
			
		}
		/* Going from future to past... */
		else {
			/* Switches out maps. */
			pastMap.gameObject.SetActive(true);
			futureMap.gameObject.SetActive(false);
			
			/* Tell every pickup in the scene to do whatever it needs to do when switching eras. */
			for (int i = 0; i < pickups.Count; ++i) {
				pickups[i].HandleChangeEra(TimePeriod.PAST);
			}
		}		
		
		if (LevelManager.IsPast()) {
			LevelManager.Instance.CurrentEra = TimePeriod.FUTURE;
		}
		else {
			LevelManager.Instance.CurrentEra = TimePeriod.PAST;
		}

	}
	
	
	
	/* Any logic to handle right before the actual time travel stage. */
	private void BeforeTimeTravel() {
		
		/* Going from past to future... */
		if (LevelManager.IsPast()) {

			/* Tell every pickup in the scene to do whatever it needs to do when switching eras. */
			for (int i = 0; i < pickups.Count; ++i) {
				pickups[i].HandleBeforeChangeEra(TimePeriod.FUTURE);
			}
			
		}
		/* Going from future to past... */
		else {
		
			/* Tell every pickup in the scene to do whatever it needs to do when switching eras. */
			for (int i = 0; i < pickups.Count; ++i) {
				pickups[i].HandleBeforeChangeEra(TimePeriod.PAST);
			}
		}		

	}
	
	
	
	
	public void OnWarp() {
		//Debug.Log (this.name + ": OnWarp().");	
		StartCoroutine(PlayWarpAnimation());
		sfxPlayer02.Play();
		// TimeTravel() is called within PlayWarpAnimation();

	}
	
	
	
	void OnTriggerEnter(Collider collider) {
		//Debug.Log ("Entering range of vortex...");
		vortexInactiveParticles.startSize = 0.0f;	
		vortexActiveParticles.startSize = particleStartSize;	
		iTween.AudioTo(sfxPlayer01.gameObject, 1.0f, whenNearbySoundPitch, whenNearbySoundFadeTime);
		iTween.ColorTo(glow.gameObject, Color.red, 2.0f*whenNearbySoundFadeTime);
	}
	
	
	
	void OnTriggerExit(Collider collider) {
		//Debug.Log ("Exiting range of vortex...");
		vortexInactiveParticles.startSize = particleStartSize;	
		vortexActiveParticles.startSize = 0.0f;	
		iTween.AudioTo(sfxPlayer01.gameObject, 0.0f, -whenNearbySoundPitch, whenNearbySoundFadeTime);
		iTween.ColorTo(glow.gameObject, Color.blue, 2.0f*whenNearbySoundFadeTime);
	}

	
	
	IEnumerator PlayWarpAnimation() {
		
		currentlyWarping = true;
		
		/* Lock player movement. */
		player.canMove = false;
		player.GetComponent<FSWorldComponent>().enabled = false; // Pause Farseer Physics simulation.
		
		/* Move portal & curtain in front of main camera. */
		this.transform.Translate(new Vector3(0.0f, 0.0f, mainCamera.transform.position.z+1.0f));
		animationCurtain.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		
		BeforeTimeTravel();
		
		/* Fade to black animation. */
		iTween.FadeTo(animationCurtain.gameObject, 1.0f, 2.0f);
		yield return new WaitForSeconds(2.0f);
		
		TimeTravel();
		
		currentlyWarping = false;
		
		/* Unlock player movement. */
		player.canMove = true;
		player.GetComponent<FSWorldComponent>().enabled = true; // Unpause Farseer Physics simulation.
		
		/* White flash animation. */
		iTween.ColorFrom(animationCurtain.gameObject, Color.white, 1.0f);	
		this.transform.Translate(new Vector3(0.0f, 0.0f, -mainCamera.transform.position.z-1.0f));
		iTween.FadeTo(animationCurtain.gameObject, 0.0f, 1.0f);
		yield return new WaitForSeconds(1.0f);
		
		yield return 0;
		
		
	}
	


	void OnDestroy() {
		pickups = null;
	}
	
	
	public static bool CurrentlyWarping() {
		return currentlyWarping;	
	}
	

	
}
