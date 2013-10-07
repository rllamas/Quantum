using UnityEngine;
using System.Collections.Generic;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;


/* 
 *   Other objects will only collide on top of the Farseer collider for this object.
 */
public class OneWayPlatform : MonoBehaviour {
	
	
	/* The Farseer physics body of this object. */
	private Body body;
	
	
	
	void Start () {

		body = this.GetComponent<FSBodyComponent>().PhysicsBody;
		
		/* Register collision callback. */
		body.OnCollision += OnCollisionEvent;
	}
	
	
	
	private bool OnCollisionEvent(Fixture thisObjectFixture, Fixture otherObjectFixture, Contact contact) {
		
		/* If other object's velocity is negative, then it is above the platform and thus a collision should occur. */
		return (otherObjectFixture.Body.LinearVelocity.Y <= 0);
		
	}
	
	
	
	
}
