using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour {
	
	private Player player;
	
	/* These are the audio players that are available to use. */
	private AudioSource musicPlayer01; // Reserved for past music.
	private AudioSource musicPlayer02; // Reserved for future music.
	private AudioSource sfxPlayer01; // Reserved for player sounds.
	private AudioSource sfxPlayer02; // Reserved for portal sounds.
	
	
	/* These are the different categories of sounds that can be played. */
	//public AudioClip [] sfxTracks;
	public AudioClip [] pastMusicTracks;
	public AudioClip [] futureMusicTracks;
	
	
	/* Audio parameters for when the player is near a vortex. */
	public float vortexFadeMusicVolume = 0.60f;
	public float vortexFadeMusicTime = 1.0f;
	public float vortexFadeMusicPitch = 1.0f;
	
	private float originalMusicVolume;
	private float originalMusicPitch;
	
	
	/* These keep track of state for the frame. */
	private bool isPast;
	private bool isPastLastFrame;
	
	private bool nearVortex;
	private bool nearVortexLastFrame;
	
	
	
	
	// Use this for initialization
	void Start () {
		
		if (pastMusicTracks.Length == 0) {
			throw new Exception("No past music tracks!");	
		}
		else if (futureMusicTracks.Length == 0) {
			throw new Exception("No future music tracks!");	
		}

		
		
		
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		musicPlayer01 = this.transform.FindChild("Music Player 01").GetComponent<AudioSource>();
		musicPlayer02 = this.transform.FindChild("Music Player 02").GetComponent<AudioSource>();
		sfxPlayer01 = this.transform.FindChild("SFX Player 01").GetComponent<AudioSource>();
		sfxPlayer02 = this.transform.FindChild("SFX Player 02").GetComponent<AudioSource>();
		
		
		originalMusicVolume = 1.0f;
		originalMusicPitch = 1.0f;
		
		
		musicPlayer01.clip = pastMusicTracks[0];
		musicPlayer02.clip = futureMusicTracks[0];
		
		musicPlayer01.loop = true;
		musicPlayer02.loop = true;
		
		
		/* Only audibly play the current era's track. */
		if (Vortex.isPast) {
			musicPlayer02.volume = 0.0f;
		}
		else {
			musicPlayer01.volume = 0.0f;
		}
		
		musicPlayer01.Play();
		musicPlayer02.Play();
		
		
	}
	
	
	
	
	
	// Update is called once per frame
	void Update () {
		
		isPastLastFrame = isPast;
		isPast = Vortex.isPast;
		
		nearVortexLastFrame = nearVortex;
		nearVortex = (player.currentActionButtonState == Player.ActionButtonStates.CAN_ACTIVATE_VORTEX);
		
		
		/* Change music if switching eras. */
		
		/* Future -> Past */
		if (isPast && !isPastLastFrame) {
			iTween.AudioTo(musicPlayer01.gameObject, vortexFadeMusicVolume, vortexFadeMusicPitch, vortexFadeMusicTime);
			iTween.AudioTo(musicPlayer02.gameObject, 0.0f, vortexFadeMusicPitch, vortexFadeMusicTime);
		}
		/* Past -> Future */
		else if (!isPast && isPastLastFrame) {
			iTween.AudioTo(musicPlayer02.gameObject, vortexFadeMusicVolume, vortexFadeMusicPitch, vortexFadeMusicTime);
			iTween.AudioTo(musicPlayer01.gameObject, 0.0f, vortexFadeMusicPitch, vortexFadeMusicTime);
		}
		
		/* Change volume and pitch if entering/leaving a vortex zone. */
		
		/* Dim music if player is near a vortex. */
		if (nearVortex && !nearVortexLastFrame) {
			if (isPast) {
				iTween.AudioTo(musicPlayer01.gameObject, vortexFadeMusicVolume, vortexFadeMusicPitch, vortexFadeMusicTime);
			}
			else {
				iTween.AudioTo(musicPlayer02.gameObject, vortexFadeMusicVolume, vortexFadeMusicPitch, vortexFadeMusicTime);
			}
		}
		/* Restore volume to normal if player if leaving a vortex. */
		else if (!nearVortex && nearVortexLastFrame) {
			if (isPast) {
				iTween.AudioTo(musicPlayer01.gameObject, originalMusicVolume, originalMusicPitch, vortexFadeMusicTime);
			}
			else {
				iTween.AudioTo(musicPlayer02.gameObject, originalMusicVolume, originalMusicPitch, vortexFadeMusicTime);
			}
			
		}
	}

	
	
	
}
