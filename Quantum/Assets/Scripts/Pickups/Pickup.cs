using UnityEngine;
using System;
using System.Collections;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class Pickup : MonoBehaviour {
	
	/* Where the pickup should be positioned relative to the player picking it up. */
	public Vector3 offsetFromPlayer;
	public LevelManager.TimePeriod currentEraExistingIn;
	
	protected Body body;
	
	
	public virtual void Start () {
		collider.isTrigger = true;
		
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixtureList[0].UserData = "Pickup";
		body.FixtureList[0].UserTag = "Pickup";
		body.FixedRotation = true;
		//body.IsSensor = true;
		gameObject.tag = "Pickup";
		
		body.OnCollision += OnCollisionEvent;
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
		
	}
	
	
	
	
	public virtual void Update () {
		HandlePosition();
	}
	
	
	
	
	protected virtual bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {

		if ((string) fixtureB.UserTag == "Player") {
			return false;
		}
		return true;
	}
	
	
	
	
	/* Are you allowed to pick up the pickup right now? */
	public virtual bool CanPickup() {
		return true;	
	}

	
	
	
	/* The Player object is expected to call this method when picking up this object. */
	public virtual void OnPickup(Player player) {
		Debug.Log (this.name + ": OnPickup().");
		
		/* Throw exception if this method is called but the pickup is not possible to pick up right now. */
		if (!CanPickup() ) {
			throw new Exception(this.name + ": " + player.name + " calling OnPickup(), but " + this.name + 
				" is currently not pickupable!");	
		}
		
		/* Disable collider triggering if the player picks this object up. We can't use 'isTrigger = false' 
		 * on the collider, however, or the collider will immediately have physics turned back on, and the
		 * player will immediately collide with this object. Instead, we just disable the collider altogether. */
		this.collider.enabled = false; 
		
		this.transform.parent = player.transform;
		this.body.BodyType = BodyType.Static;
		HandlePosition();
	}
	
	
	
	
	/* The Player object is expected to call this method when dropping this object. */
	public virtual void OnDrop() {
		Debug.Log (this.name + ": OnDrop().");
		
		/* Re-enable collider triggering for this object when the player sets it down. */
		this.collider.enabled = true;	
		
		this.transform.parent = null;
		this.body.BodyType = BodyType.Dynamic;
		
	}
	
	
	
	
	/* Moves the pickup to the correct position relative to the player. */
	public virtual void HandlePosition() {
		
		/* If you have a parent, then move to where you should be relative to him. */
		if (this.transform.parent != null) {
			
			if (GetPlayer().currentDirection == Player.Direction.LEFT) {
				this.body.Position = new FVector2(this.transform.parent.position.x + offsetFromPlayer.x,
													this.transform.parent.position.y + offsetFromPlayer.y);
				if (Input.GetKeyDown(KeyCode.A)) {
					this.body.ApplyLinearImpulse(new FVector2(this.transform.parent.position.x + offsetFromPlayer.x, 0));
				}
			}
			else if (GetPlayer().currentDirection == Player.Direction.RIGHT) {
				this.body.Position = new FVector2(this.transform.parent.position.x - offsetFromPlayer.x -0.15f,
													this.transform.parent.position.y + offsetFromPlayer.y);
				if (Input.GetKeyDown(KeyCode.S)) {
					this.body.ApplyLinearImpulse(new FVector2(this.transform.parent.position.x - offsetFromPlayer.x -0.15f, 0));
				}
			}
		}
	}
	
	
	
	
	/* Handle whatever logic this object needs to do when changing eras. */
	public virtual void HandleChangeEra(LevelManager.TimePeriod eraChangingTo) {
		;
	}
		
		
		
		
	
	/* Returns the player picking up this pickup. */
	private Player GetPlayer() {
		return this.transform.parent.GetComponent<Player>();	
	}

}
