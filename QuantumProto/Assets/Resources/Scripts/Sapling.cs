using UnityEngine;
using System.Collections;

public class Sapling : MonoBehaviour {
	
	#region private variables
	private bool inTrigger = false;
	
	#endregion
	
	// Use this for initialization
	void Start () {
		collider.isTrigger = true;
		
		
		// Don't allow rotations
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		collider.isTrigger = true;
		
		if (inTrigger && Input.GetButton("Fire1")){
			Debug.Log ("Boom.");
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("In range.");
		inTrigger = true;
    }
	
	void OnTriggerExit(Collider other) {
		Debug.Log ("Out of range.");
		inTrigger = false;
	}
}
