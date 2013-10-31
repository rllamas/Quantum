using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class BouncyPlatform : MonoBehaviour {
	public Body body;
	public float jumpFactor = 300f;
	
	
	// Use this for initialization
	void Start () {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.OnCollision += OnCollisionEvent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	bool OnCollisionEvent(Fixture A, Fixture B, Contact contact) {
		/*if (contact.IsTouching() && A.Body.UserTag == "Player") {
			A.Body.ApplyLinearImpulse(new FVector2(0, jumpFactor));
			//Debug.Log("A: " + A.Body.LinearVelocity);
			
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
		else*/ if (contact.IsTouching() && B.Body.UserTag == "Player") {
			B.Body.ApplyLinearImpulse(new FVector2(0, jumpFactor));
			//Debug.Log("B: " + B.Body.LinearVelocity);
			
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
		
		return true;
	}
}
