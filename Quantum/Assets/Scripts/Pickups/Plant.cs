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
		body.OnCollision += OnCollisionEvent;
		base.Start();
		HandleEra();
	}
	
	bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		if ((string) fixtureB.UserData == "Player") {
			return false;
		}
		return true;
	}
	
	
	public override void Update() {
		base.Update();
		HandleEra();
	}
	
	
	
	
	/* Sets the plant object being used based on the time era. */
	private void HandleEra() {
		if (Vortex.isPast) {
			pastPlant.SetActive(true);	
			futurePlant.SetActive(false);	
		}
		else {
			pastPlant.SetActive(false);
			futurePlant.SetActive(true);	
		}
	}
		
	
	

}
