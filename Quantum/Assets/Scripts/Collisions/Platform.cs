using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;



public class Platform : MonoBehaviour {
	
	/* The Farseer physics body of this object. */
	private Body body;
	


	// Use this for initialization
	void Start () {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
			
		/* Register collision callback. */
		body.OnCollision += OnCollisionEvent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	private bool OnCollisionEvent(Fixture thisObjectFixture, Fixture otherObjectFixture, Contact contact) {
		
		if (Input.GetAxis("Vertical") < 0) {
			return false;	
		}
		
		return true;
	}
}
