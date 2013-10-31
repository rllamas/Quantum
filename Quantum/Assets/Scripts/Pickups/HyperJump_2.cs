using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class HyperJump_2 : Pickup {
	public float jumpFactor = 0f;
	private float currentJumpFactor;
	
	private Body body;
	
	//private tk2dSprite sprite = GetComponent<tk2dSprite>();

	// Use this for initialization
	public override void Start () {
		base.Start();
		
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.OnCollision += OnCollisionEvent;
		
		if (LevelManager.IsPast()) {
			currentJumpFactor = 0f;
			
		}
		else {
			currentJumpFactor = jumpFactor;
			//sprite.SetSprite("animation_hyperjump_pad");
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
	
	public bool OnCollisionEvent(Fixture A, Fixture B, Contact contact) {
		if (this.transform.parent == null) {
			if (contact.IsTouching() && A.Body.UserTag == "Player") {
				A.Body.ApplyLinearImpulse(new FVector2(0, currentJumpFactor));
				
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
			else if (contact.IsTouching() && B.Body.UserTag == "Player") {
				B.Body.ApplyLinearImpulse(new FVector2(0, currentJumpFactor));
				
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
		
		if (B.Body.UserTag == "Pickup") {
			return false;
		}
		
		return true;
	}
	
	public override void HandleChangeEra(TimePeriod eraChangingTo) {
		base.HandleChangeEra(eraChangingTo);
	}
}