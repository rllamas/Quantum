using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	#region private variables
	private bool inTrigger = false;
	
	#endregion
	
	// Use this for initialization
	void Start () {
		collider.isTrigger = true;
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	/* The Player object is expected to call this to pick up this object. */
	void OnPickup() {
		
		if (inTrigger && Input.GetButton("Fire1")){
			Debug.Log ("Boom.");
			Destroy(this.gameObject);
		}
	}
	
	
	
//	void OnTriggerEnter(Collider other) {
//		Debug.Log ("Pickup: Entering triggering range of player.");
//		inTrigger = true;
//    }
	
//	void OnTriggerStay(Collider other) {
//		Debug.Log ("Pickup: Currently in triggering range of player.");
//    }
	
//	void OnTriggerExit(Collider other) {
//		Debug.Log ("Pickup: Leaving triggering range of player.");
//		inTrigger = false;
//	}
}
