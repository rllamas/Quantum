using UnityEngine;
using System.Collections;

public class Sapling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		// Don't allow rotations
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
