using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	

	void Start () {
		collider.isTrigger = true;
		gameObject.tag = "Pickup";
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
	}
	
	

	void Update () {
		
	}
	
	
	
	/* The Player object is expected to call this method when picking up this object. */
	public void OnObtain(Player player) {
		Debug.Log (this.name + ": OnObtain().");
		this.collider.enabled = false;
		this.transform.parent = player.transform;		
	}
	
	
	
	/* The Player object is expected to call this method when dropping this object. */
	public void OnRelease() {
		Debug.Log (this.name + ": OnRelease().");
		this.collider.enabled = true;	
	}
	
	
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log (this.name + ": Entering triggering range of player.");
    }
	
	
	
	void OnTriggerStay(Collider other) {
		//Debug.Log (this.name + ": Currently in triggering range of player.");
    }
	
	
	
	void OnTriggerExit(Collider other) {
		//Debug.Log (this.name + ": Leaving triggering range of player.");
	}
}
