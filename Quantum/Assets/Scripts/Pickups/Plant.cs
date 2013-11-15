using UnityEngine;
using System.Collections;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;

public class Plant : Pickup {
	
	public GameObject pastPlant; // Plant object used in the past era.
	public GameObject futurePlant; // Plant object use in the future era.
		
	private tk2dCamera mainCamera;
	string animationPrefabPath = "Animations/resource_animation_plant_grow";
	
	
	public override void Start() {
		
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<tk2dCamera>();
		
		base.Start();
		if (LevelManager.IsPast()) {
			HandleChangeEra(TimePeriod.PAST);
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
		/* Only allowed to pick the plant up if it's a sapling. */
		return pastPlant.activeInHierarchy; // basically 'return pastPlant.isActive?;'
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
				/* Grow plant into beanstalk. */
				pastPlant.SetActive(false);
				futurePlant.SetActive(true);
			}
			
			/* And I'm in the future... */
			else {
				/* Show small plant. */
				pastPlant.SetActive(true);
				futurePlant.SetActive(false);
			}
			
		}
		/* Else if player is going to the past.. */
		else {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
				/* Turn back into small plant. */
				pastPlant.SetActive(true);
				futurePlant.SetActive(false);
			}
			/* And I'm in the future... */
			else {
				/* Don't show plant at all. */
				pastPlant.SetActive(false);
				futurePlant.SetActive(false);
			}
			
			
		}
		
	}
	
	
	
	
	/* Handle whatever logic this object needs to do right before changing eras. */
	public override void HandleBeforeChangeEra(TimePeriod eraChangingTo) {
		
		base.HandleBeforeChangeEra(eraChangingTo);

		/* If player is going to the future... */
		if (eraChangingTo == TimePeriod.FUTURE) {
		
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {

				/* Play grow animation. */
				//this.transform.Translate(new Vector3(0.0f, 0.0f, mainCamera.transform.position.z+1.0f));
				Instantiate(
					Resources.Load(animationPrefabPath),
					new Vector3(0.0f, 0.0f, mainCamera.transform.position.z+1.0f),
					Quaternion.identity
				);
			}
			
			/* And I'm in the future... */
			else {
			}
			
		}
		/* Else if player is going to the past.. */
		else {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
			}
			/* And I'm in the future... */
			else {
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
		else {
			player.sfxPlayer.pitch = 0.5f;
		}
		player.sfxPlayer.Play();
	}
	
	
	
	public override void OnDrop () {
		base.OnDrop();
		
		/* Play digging up sound. */
		player.sfxPlayer.clip = player.dropPlantSound;
		player.sfxPlayer.loop = false;
		if (!player.NearVortex()) {
			player.sfxPlayer.pitch = 0.5f;
			player.sfxPlayer.volume = 0.4f;
		}
		else {
			player.sfxPlayer.pitch = 0.25f;
			player.sfxPlayer.volume = 0.4f;
		}
		player.sfxPlayer.Play();
	}
	
	

}
