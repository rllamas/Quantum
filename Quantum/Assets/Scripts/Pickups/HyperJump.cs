using UnityEngine;
using System.Collections;

public class HyperJump : Pickup {
	
	public GameObject activePlatform;
	public GameObject inactivePlatform;

	public override void Start() {
		base.Start();
		if (LevelManager.IsPast()) {
			HandleChangeEra(TimePeriod.PAST);
			//currentEraExistingIn = TimePeriod.PAST;
			//activePlatform.SetActive(false);
			//inactivePlatform.SetActive(false);
		}
		else {
			HandleChangeEra(TimePeriod.FUTURE);
		}
	}
	
	public override void Update() {
		base.Update();
	}
	
	/* Are you allowed to pick up the pickup right now? */
	public override bool CanPickup() {
		return true;
	}
	
	
	/* Handle whatever logic this object needs to do when changing eras. */
	public override void HandleChangeEra(TimePeriod eraChangingTo) {
		
		base.HandleChangeEra(eraChangingTo);
		
		/* Switch eras I'm in if player takes me through a portal. */
		if (this.transform.parent) {
			currentEraExistingIn = eraChangingTo;
		}

		/* If player is going to the future... */
		if (eraChangingTo == TimePeriod.FUTURE) {
		
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
				Debug.Log("1");
				/* Deactivate Power. */
				activePlatform.SetActive(false);
				inactivePlatform.SetActive(true);
			}
			
			/* And I'm in the future... */
			else {
				Debug.Log("2");
				/* Currently Deactivated */
				activePlatform.SetActive(false);
				inactivePlatform.SetActive(true);
			}
			
		}
		/* Else if player is going to the past.. */
		else {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
				Debug.Log("3");
				/* Activate Power */
				activePlatform.SetActive(true);
				inactivePlatform.SetActive(false);
			}
			/* And I'm in the future... */
			else {
				Debug.Log("4");
				/* Currently Deactivated */
				activePlatform.SetActive(false);
				inactivePlatform.SetActive(false);
			}
			
			
		}
		
	}

	void OnDestroy() {
		this.transform.parent = null;
	}
	
	public override void OnPickup (Player player) {
		base.OnPickup(player);
		
		/* Play digging up sound. */
		player.sfxPlayer.clip = player.pickUpPlantSound;
		player.sfxPlayer.loop = false;
		if (!player.NearVortex()) {
			player.sfxPlayer.pitch = 1.0f;
		}
		player.sfxPlayer.Play();
	}
}
