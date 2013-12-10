using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;



public class HyperJump_2 : Pickup {
	public float jumpFactor = 0f;
	public float currentJumpFactor;
		
	private tk2dSprite sprite;
	private tk2dSpriteAnimator anim;
	
	private tk2dCamera mainCamera;

	private GameObject activeParticles;
	private GameObject inactiveParticles;

	string disintegrationAnimationPrefabPath = "Animations/resource_animation_hyperjump_disintegration";
	string disintegrationReverseAnimationPrefabPath = "Animations/resource_animation_hyperjump_disintegration_reverse";


	/* Use this for initialization */
	public override void Start () {
		base.Start();
		
		sprite = GetComponent<tk2dSprite>();
		anim = GetComponent<tk2dSpriteAnimator>();

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<tk2dCamera>();
		activeParticles = transform.FindChild("Active Particles").gameObject;
		inactiveParticles = transform.FindChild("Inactive Particles").gameObject;


		body.FixtureList[0].UserData = "HyperJump";
		body.FixtureList[0].UserTag = "HyperJump";
		
		if (LevelManager.IsPast()) {
			currentJumpFactor = 0f;
			GetComponent<MeshRenderer>().enabled = false;
		}
		else {
			currentJumpFactor = 0f;
		}

		if (LevelManager.Instance.CurrentEra == currentEraExistingIn) {
			activeParticles.SetActive(false);
			inactiveParticles.SetActive(true);
		}
		else {
			activeParticles.SetActive(false);
			inactiveParticles.SetActive(false);
		}
	}
	
	/* Update is called once per frame */
	public override void Update () {
		base.Update();
	}

	/* Are you allowed to pick up the pickup right now? */
	public override bool CanPickup() {
		bool isGrounded = GameObject.FindWithTag("Player").GetComponent<Player>().IsGrounded();
		return GetComponent<MeshRenderer>().enabled && isGrounded;// && currentJumpFactor == 0;
	}

	protected override bool OnCollisionEvent(Fixture A, Fixture B, Contact contact) {
		/*if (B.Body.UserTag == "Player") {
			if (!CanPickup()) {
				return false;
			}
			else if (currentEraExistingIn == TimePeriod.FUTURE && LevelManager.IsFuture()) {
				return false;
			}
			else if (currentEraExistingIn == TimePeriod.PAST && LevelManager.IsPast()) {
				if (this.transform.parent == null) {
					if (B.Body.LinearVelocity.Y < 0) {
						B.Body.ApplyLinearImpulse(new FVector2(0, -B.Body.LinearVelocity.Y * currentJumpFactor));

					}
					else {
						B.Body.ApplyLinearImpulse(new FVector2(0, B.Body.LinearVelocity.Y * currentJumpFactor));
					}
				
					if (currentJumpFactor > 0 && B.Body.LinearVelocity.Y > 0) {
						/* Play jumping sound. *
						Player attachedPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
						attachedPlayer.sfxPlayer.clip = attachedPlayer.jumpSound;
						attachedPlayer.sfxPlayer.loop = false;
						/* Give some variation to the jump pitch. *
						if (!attachedPlayer.NearVortex()) {
							attachedPlayer.sfxPlayer.pitch = 1.0f + 0.02f*UnityEngine.Random.Range(-11, 6);
						}
						attachedPlayer.sfxPlayer.Play();
					}
					return false;
				}
				else {
					return false;
				}
			}
		}
		else if (B.Body.UserTag == "Pickup") {
			return false;
		}
		
		A.Body.BodyType = BodyType.Static;*
		return true;*/

		if (B.Body.UserTag == "Player") {
			if (this.transform.parent != null) {
				return false;
			}
			else {
				if (LevelManager.IsPast()) {
					B.Body.ApplyLinearImpulse(new FVector2(0, Mathf.Abs(B.Body.LinearVelocity.Y * currentJumpFactor)));

					if (currentJumpFactor > 0 && B.Body.LinearVelocity.Y > 0) {
						/* Play jumping sound. */
						Player attachedPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
						attachedPlayer.sfxPlayer.clip = attachedPlayer.jumpSound;
						attachedPlayer.sfxPlayer.loop = false;
						/* Give some variation to the jump pitch. */
						if (!attachedPlayer.NearVortex()) {
							attachedPlayer.sfxPlayer.pitch = 1.0f + 0.02f*UnityEngine.Random.Range(-11, 6);
						}
						attachedPlayer.sfxPlayer.Play();
					}
				}

				return false;
			}
		}
		else if (B.Body.UserTag == "Pickup") {
			return false;
		}

		A.Body.BodyType = BodyType.Static;
		return true;
	}

	public override void HandleBeforeChangeEra(TimePeriod eraChangingTo) {
		base.HandleBeforeChangeEra(eraChangingTo);
		
		/* Switch eras I'm in if player takes me through a portal. */
		if (this.transform.parent) {
			currentEraExistingIn = eraChangingTo;
		}

		/* Don't play animation if I'm held. */
		if (transform.parent != null) {
			return;	
		}

		/* Disable all particle effects. */
		activeParticles.SetActive(false);
		inactiveParticles.SetActive(false);

		/* If player is going to the future... */
		if (eraChangingTo == TimePeriod.FUTURE) {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {

				/* Deactivate Power. */
				currentJumpFactor = 0f;
				sprite.SetSprite("amimation_hyperjump_pad01");
				GetComponent<MeshRenderer>().enabled = false;

				/* Play disintegration animation. */
				GameObject disintegrationAnimation = (GameObject)Instantiate(
					Resources.Load(disintegrationAnimationPrefabPath),
					new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z+0.5f),
					Quaternion.identity
					);

			}
			
			/* And I'm in the future... */
			else {

				/* Currently Deactivated */
				currentJumpFactor = 0f;
				sprite.SetSprite("amimation_hyperjump_pad01");
				GetComponent<MeshRenderer>().enabled = false;

				/* Play disintegration reverse animation. */
				GameObject disintegrationReverseAnimation = (GameObject)Instantiate(
					Resources.Load(disintegrationReverseAnimationPrefabPath),
					new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z+0.5f),
					Quaternion.identity
					);

			}
			
		}
		
		/* Else if player is going to the past.. */
		else {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
				/* Activate Power */
				currentJumpFactor = jumpFactor;
				GetComponent<MeshRenderer>().enabled = false;
				if (sprite.spriteId == sprite.GetSpriteIdByName("amimation_hyperjump_pad01")
					&& this.transform.parent == null) {
					sprite.SetSprite("amimation_hyperjump_pad12");
				}

				/* Play disintegration animation. */
				GameObject disintegrationReverseAnimation = (GameObject)Instantiate(
					Resources.Load(disintegrationReverseAnimationPrefabPath),
					new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z+0.5f),
					Quaternion.identity
				);

			}

			/* And I'm in the future... */
			else {
				/* Currently Deactivated */
				currentJumpFactor = 0f;
				sprite.SetSprite("amimation_hyperjump_pad01");
				GetComponent<MeshRenderer>().enabled = false;

				/* Play disintegration animation. */
				GameObject disintegrationAnimation = (GameObject)Instantiate(
					Resources.Load(disintegrationAnimationPrefabPath),
					new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z+0.5f),
					Quaternion.identity
				);
		
			}	
		}
	}
	
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
				/* Deactivate Power. */
				currentJumpFactor = 0f;
				sprite.SetSprite("amimation_hyperjump_pad01");
				GetComponent<MeshRenderer>().enabled = false;

				activeParticles.SetActive(false);
				inactiveParticles.SetActive(false);
			}
			
			/* And I'm in the future... */
			else {
				/* Currently Deactivated */
				currentJumpFactor = 0f;
				sprite.SetSprite("amimation_hyperjump_pad01");
				GetComponent<MeshRenderer>().enabled = true;

				activeParticles.SetActive(false);
				inactiveParticles.SetActive(true);
			}
			
		}
		
		/* Else if player is going to the past.. */
		else {
			
			/* And I'm in the past... */
			if (currentEraExistingIn == TimePeriod.PAST) {
				/* Activate Power */
				currentJumpFactor = jumpFactor;
				GetComponent<MeshRenderer>().enabled = true;
				if (sprite.spriteId == sprite.GetSpriteIdByName("amimation_hyperjump_pad01")
				    && this.transform.parent == null) {
					sprite.SetSprite("amimation_hyperjump_pad12");

				}

				if (!this.transform.parent) {
					activeParticles.SetActive(true);
					inactiveParticles.SetActive(false);
				}
			}
			/* And I'm in the future... */
			else {
				/* Currently Deactivated */
				currentJumpFactor = 0f;
				sprite.SetSprite("amimation_hyperjump_pad01");
				GetComponent<MeshRenderer>().enabled = false;

				activeParticles.SetActive(false);
				inactiveParticles.SetActive(false);
			}	
		}
	}
	
	void OnDestroy() {
		this.transform.parent = null;
	}
	
	public override void OnPickup (Player player) {
		base.OnPickup(player);
		
		if (currentEraExistingIn == TimePeriod.PAST && LevelManager.IsPast()) {
			anim.Play("deactivation");

			activeParticles.SetActive(false);
			inactiveParticles.SetActive(true);
		}
		
		/* Play digging up sound. */
		player.sfxPlayer.clip = player.pickUpPlantSound;
		player.sfxPlayer.loop = false;
		if (!player.NearVortex()) {
			player.sfxPlayer.pitch = 1.0f;
		}
		player.sfxPlayer.Play();
	}
	
	public override void OnDrop () {
		base.OnDrop();
		
		if (currentEraExistingIn == TimePeriod.PAST && LevelManager.IsPast()) {
			anim.Play("activation");

			activeParticles.SetActive(true);
			inactiveParticles.SetActive(false);
		}
		
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