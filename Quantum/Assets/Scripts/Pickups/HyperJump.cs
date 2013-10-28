using UnityEngine;
using System.Collections;

public class HyperJump : Pickup {
	
	public GameObject pastPlatform;
	public GameObject futurePlatform;

	public override void Start() {
		base.Start();
		if (LevelManager.IsPast()) {
			HandleChangeEra(TimePeriod.FUTURE);
		}
		else {
			HandleChangeEra(TimePeriod.PAST);
		}
	}
	
	public override void Update() {
		base.Update();
	}
	
	/* Are you allowed to pick up the pickup right now? */
	public override bool CanPickup() {
		return pastPlatform.activeInHierarchy;
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
				pastPlatform.SetActive(false);
				futurePlatform.SetActive(true);
			}
			
			/* And I'm in the future... */
			else {
				Debug.Log("2");
				/* Currently Deactivated */
				pastPlatform.SetActive(false);
				futurePlatform.SetActive(false);
			}
			
		}
		/* Else if player is going to the past.. */
		else {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
				Debug.Log("3");
				/* Activate Power */
				pastPlatform.SetActive(true);
				futurePlatform.SetActive(false);
			}
			/* And I'm in the future... */
			else {
				Debug.Log("4");
				/* Currently Deactivated */
				pastPlatform.SetActive(false);
				futurePlatform.SetActive(false);
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
