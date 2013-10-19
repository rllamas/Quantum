using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class FarseerFollowParent : MonoBehaviour {

	private Body body;
	private Vector3 offsetFromParent;
	private UnityEngine.Transform parent;
	
	// Use this for initialization
	void Start () {
		body = this.GetComponent<FSBodyComponent>().PhysicsBody;
		offsetFromParent = this.transform.localPosition;
		parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		body.Position = new FVector2(
			parent.position.x + offsetFromParent.x, 
			parent.position.y + offsetFromParent.y
		);
	}
}
