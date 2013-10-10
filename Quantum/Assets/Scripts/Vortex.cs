using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vortex : MonoBehaviour {
	private tk2dTileMap pastMap;
	private tk2dTileMap futureMap;
	
	/* If true, then the past is loaded. */
	public static bool isPast = true;
	
	//public List<GameObject> itemsInScene;
	
	public ParticleSystem vortexActiveParticles;
	public ParticleSystem vortexInactiveParticles;
	
	public float particleStartSize = 1.0f;
	
	public AudioClip whenNearbySound;
	public float whenNearbySoundPitch = 1.75f;
	public float whenNearbySoundFadeTime = 1.5f;
	public AudioClip warpSound;
	private AudioSource sfxPlayer01;
	private AudioSource sfxPlayer02;
	
	public MeshFilter animationCurtain;
	private tk2dCamera mainCamera;
	
	
	
	
	// Use this for initialization
	void Start () {
		gameObject.tag = "Vortex";
		
		pastMap = GameObject.Find("Past Map").GetComponent<tk2dTileMap>();
		futureMap = GameObject.Find("Future Map").GetComponent<tk2dTileMap>();
		
		vortexActiveParticles.transform.position = this.transform.position;
		vortexInactiveParticles.transform.position = this.transform.position;
		
		vortexActiveParticles.Play();
		vortexActiveParticles.startSize = 0.0f;	
		//vortexActiveParticles.Clear();
		vortexInactiveParticles.Play();
		
		sfxPlayer01 = this.transform.FindChild("SFX Player 01").GetComponent<AudioSource>();
		sfxPlayer02 = this.transform.FindChild("SFX Player 02").GetComponent<AudioSource>();
		
		sfxPlayer01.clip = whenNearbySound;
		sfxPlayer01.loop = true;
		sfxPlayer01.volume = 0.0f;
		sfxPlayer01.Play();
		
		sfxPlayer02.clip = warpSound;
		sfxPlayer02.Pause();
		
		pastMap.transform.position = Vector3.zero;
		futureMap.transform.position = Vector3.zero;
		
		if (isPast) {
			futureMap.gameObject.SetActive(false);
		}
		else {
			pastMap.gameObject.SetActive(false);
		}
		
		animationCurtain =  this.transform.FindChild("Curtain").GetComponent<MeshFilter>();
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<tk2dCamera>();
		
		animationCurtain.gameObject.SetActive(true);
		animationCurtain.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
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
		StartCoroutine(PlayWarpAnimation());
		sfxPlayer02.Play();
		

	}
	
	
	
	void OnTriggerEnter(Collider collider) {
		Debug.Log ("Entering range of vortex...");
		vortexInactiveParticles.startSize = 0.0f;	
		vortexActiveParticles.startSize = particleStartSize;	
		iTween.AudioTo(sfxPlayer01.gameObject, 1.0f, whenNearbySoundPitch, whenNearbySoundFadeTime);
	}
	
	
	
	void OnTriggerExit(Collider collider) {
		Debug.Log ("Exiting range of vortex...");
		vortexInactiveParticles.startSize = particleStartSize;	
		vortexActiveParticles.startSize = 0.0f;	
		iTween.AudioTo(sfxPlayer01.gameObject, 0.0f, -whenNearbySoundPitch, whenNearbySoundFadeTime);
	}

	
	IEnumerator PlayWarpAnimation() {
		this.transform.Translate(new Vector3(0.0f, 0.0f, mainCamera.transform.position.z+1.0f));
		animationCurtain.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		
		iTween.FadeTo(animationCurtain.gameObject, 1.0f, 2.0f);
		yield return new WaitForSeconds(2.0f);
		
		TimeTravel();
		
		iTween.ColorFrom(animationCurtain.gameObject, Color.white, 1.0f);	
		this.transform.Translate(new Vector3(0.0f, 0.0f, -mainCamera.transform.position.z-1.0f));
		iTween.FadeTo(animationCurtain.gameObject, 0.0f, 1.0f);
		yield return new WaitForSeconds(1.0f);
		
		
		yield return 0;
	}
	
}
