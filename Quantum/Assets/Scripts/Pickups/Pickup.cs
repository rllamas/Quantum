using UnityEngine;
using System;
using System.Collections;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;


public class Pickup : MonoBehaviour {
	
	/* Where the pickup should be positioned relative to the player picking it up. */
	public Vector3 offsetFromPlayer;
	public Vortex.TimePeriod currentEraExistingIn;
	
	protected Body body;
	
	
	public virtual void Start () {
		collider.isTrigger = true;
		
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixtureList[0].UserData = "Pickup";
		body.FixtureList[0].UserTag = "Pickup";
		body.FixedRotation = true;
		body.IsSensor = true;
		gameObject.tag = "Pickup";
		
		body.OnCollision += OnCollisionEvent;
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
		
		if (Vortex.isPast) {
			currentEraExistingIn = Vortex.TimePeriod.PAST;
		}
		else {
			currentEraExistingIn = Vortex.TimePeriod.FUTURE;	
		}
	}
	
	
	
	
	public virtual void Update () {
		HandlePosition();
	}
	
	
	
	
	protected virtual bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {

		if ((string) fixtureB.UserData == "Player") {
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
		HandlePosition();
	}
	
	
	
	
	/* The Player object is expected to call this method when dropping this object. */
	public virtual void OnDrop() {
		Debug.Log (this.name + ": OnDrop().");
		
		/* Re-enable collider triggering for this object when the player sets it down. */
		this.collider.enabled = true;	
		
		this.transform.parent = null;
		
		Vector3 newPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x + offsetFromPlayer.x,
											GameObject.FindGameObjectWithTag("Player").transform.position.y,
											GameObject.FindGameObjectWithTag("Player").transform.position.z);
		//GameObject.Instantiate(this, newPosition, Quaternion.identity);
		//GameObject.Destroy(this.gameObject);
		this.transform.position = newPosition;
	}
	
	
	
	
	/* Moves the pickup to the correct position relative to the player. */
	public virtual void HandlePosition() {
		
		/* If you have a parent, then move to where you should be relative to him. */
		if (this.transform.parent != null) {
			
			/* If the player turns right, turn the pickup with the player. */
			if (GetPlayer().currentDirection == Player.Direction.RIGHT) {
				this.transform.localPosition = new Vector3(-1.0f*offsetFromPlayer.x, offsetFromPlayer.y, 
					offsetFromPlayer.z);
			}
			/* Otherwise, keep pickup at the normal distance from the player. */
			else {
				this.transform.localPosition = offsetFromPlayer;
			}
		}
	}
	
	
	
	
	/* Handle whatever logic this object needs to do when changing eras. */
	public virtual void HandleChangeEra(Vortex.TimePeriod eraChangingTo) {
		;
	}
		
		
		
		
	
	/* Returns the player picking up this pickup. */
	private Player GetPlayer() {
		return this.transform.parent.GetComponent<Player>();	
	}

}
