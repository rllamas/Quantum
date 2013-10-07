using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	private Player player;
	
	private AudioSource musicPlayer01;
	private AudioSource musicPlayer02;
	private AudioSource sfxPlayer01;
	private AudioSource sfxPlayer02;
	
	
	public AudioClip pastMusic01;
	public AudioClip futureMusic01;
	
	public float vortexDimAudioVolume = 0.3f;
	public float vortexDimAudioTime = 1.0f;
	public float vortexDimAudioPitch = 1.0f;
	
	
	private bool isPast;
	private bool isPastLastFrame;
	
	private bool nearVortex;
	private bool nearVortexLastFrame;
	
	
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		musicPlayer01 = this.transform.FindChild("Music Player 01").GetComponent<AudioSource>();
		musicPlayer02 = this.transform.FindChild("Music Player 02").GetComponent<AudioSource>();
		sfxPlayer01 = this.transform.FindChild("SFX Player 01").GetComponent<AudioSource>();
		sfxPlayer02 = this.transform.FindChild("SFX Player 02").GetComponent<AudioSource>();
			
		
		musicPlayer01.clip = pastMusic01;
		musicPlayer02.clip = futureMusic01;
	}
	
	
	
	
	
	// Update is called once per frame
	void Update () {
		
		isPastLastFrame = isPast;
		isPast = Vortex.isPast;
		
		nearVortexLastFrame = nearVortex;
		nearVortex = player.currentActionButtonState == Player.ActionButtonStates.CAN_ACTIVATE_VORTEX;
		
		
		/* Change music if switching eras. */
		if (isPast && !isPastLastFrame) {
			//iTween.AudioTo(musicPlayer01.gameObject, 1.0f, 1.0f, vortexDimAudioTime);
			musicPlayer01.Play();
			musicPlayer02.Pause();
			//iTween.AudioTo(musicPlayer02.gameObject, 0.0f, 1.0f, vortexDimAudioTime);
		}
		else if (!isPast && isPastLastFrame) {
			//iTween.AudioTo(musicPlayer02.gameObject, 1.0f, 1.0f, vortexDimAudioTime);
			musicPlayer02.Play();
			musicPlayer01.Pause();
			//iTween.AudioTo(musicPlayer01.gameObject, 0.0f, 1.0f, vortexDimAudioTime);
		}
		
		/* Dim music if player is near a vortex. */
		if (nearVortex && !nearVortexLastFrame) {
			iTween.AudioTo(musicPlayer01.gameObject, vortexDimAudioVolume, vortexDimAudioPitch, vortexDimAudioTime);
		}
		/* Restore volume to normal if player if leaving a vortex. */
		if (!nearVortex && nearVortexLastFrame) {
			iTween.AudioTo(musicPlayer01.gameObject, 1.0f, 1.0f, vortexDimAudioTime);
		}
	}
	
	
	
	
}
