using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class FarseerEnableDisable : MonoBehaviour {

	private FSShapeComponent shapeComponent;
	private FSBodyComponent bodyComponent;
	
	private Body body;
	
	private List<Fixture> fixtures;
	
	/* For each fixture, stores if it is initially a sensor. */
	private List<bool> isSensor; 
	
	
	
	
	void Start () {
		shapeComponent = GetComponent<FSShapeComponent>() as FSShapeComponent;
		bodyComponent = GetComponent<FSBodyComponent>() as FSBodyComponent;
		
		Body body = bodyComponent.PhysicsBody;
		
		fixtures = body.FixtureList;
		isSensor = new List<bool>(fixtures.Count);
		
		/* Save the initial state of all fixtures. */
		for (int i = 0; i < fixtures.Count; ++i) {
	        isSensor.Add(fixtures[i].IsSensor);
	    }
	}
	
	
	
	void OnEnable() {
		if (shapeComponent) {
			shapeComponent.enabled = true;
		}
		
		if (bodyComponent) {
			bodyComponent.enabled = true;
		
			
			/* Restore all fixtures to initial state. */
			for (int i = 0; i < fixtures.Count; ++i) {
		        fixtures[i].IsSensor = isSensor[i];
		    }
		}
	}
	
	
	
	void OnDisable() {
		if (shapeComponent) {
			shapeComponent.enabled = false;
		}
		
		if (bodyComponent) {
			bodyComponent.enabled = false;
			
			/* Set all fixtures as sensors to disable collison detection. */
			for (int i = 0; i < fixtures.Count; ++i) {
		        fixtures[i].IsSensor = true;
		    }
		}
	}
	

}
