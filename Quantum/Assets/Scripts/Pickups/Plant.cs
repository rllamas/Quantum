using UnityEngine;
using System.Collections;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;

public class Plant : Pickup {
	
	public GameObject pastPlant; // Plant object used in the past era.
	public GameObject futurePlant; // Plant object use in the future era.
		
	
	private Body body;
	
	public override void Start() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixedRotation = true;
		body.IsSensor = true;
		body.OnCollision += OnCollisionEvent;
		base.Start();
		HandleChangeEra(currentEraExistingIn);
	}
	
	bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		if ((string) fixtureB.UserData == "Player") {
			return false;
		}
		return true;
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
	public override void HandleChangeEra(Vortex.TimePeriod eraChangingTo) {
		base.HandleChangeEra(eraChangingTo);
		
		Debug.Log("Before check, currentEraExistingIn: " + currentEraExistingIn);
		Debug.Log("Before, eraChangingTo: " + eraChangingTo);
		
		
		
		/* Switch eras I'm in if player takes me through a portal. */
		if (this.transform.parent) {
			Debug.Log (this.name +  " - this.transform.parent: " + this.transform.parent.name);
			currentEraExistingIn = eraChangingTo;
		}
		else {
			Debug.Log (this.name +  " - no parent.");	
		}
		
		Debug.Log("After check, currentEraExistingIn: " + currentEraExistingIn);
			
		/* If player is going to the future... */
		if (eraChangingTo == Vortex.TimePeriod.FUTURE) {
		
			/* And I'm in the past... */
			if (currentEraExistingIn == Vortex.TimePeriod.PAST) {
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
			if (currentEraExistingIn == Vortex.TimePeriod.PAST) {
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
		
	
	

}
