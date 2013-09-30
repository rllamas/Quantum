using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	/* Where the pickup should be positioned relative to the player picking it up. */
	public Vector3 offsetFromPlayer;
	
	
	
	
	void Start () {
		collider.isTrigger = true;
		gameObject.tag = "Pickup";
		
		/* Don't allow rotations. */
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | 
								RigidbodyConstraints.FreezeRotationZ;
	}
	
	
	
	
	void Update () {
		HandlePosition();
	}
	
	
	
	
	/* The Player object is expected to call this method when picking up this object. */
	public void OnPickup(Player player) {
		Debug.Log (this.name + ": OnPickup().");
		
		/* Disable collider triggering if the player picks this object up. We can't use 'isTrigger = false' 
		 * on the collider, however, or the collider will immediately have physics turned back on, and the
		 * player will immediately collide with this object. Instead, we just disable the collider altogether. */
		this.collider.enabled = false; 
		
		this.transform.parent = player.transform;	
		HandlePosition();
	}
	
	
	
	
	/* The Player object is expected to call this method when dropping this object. */
	public void OnDrop() {
		Debug.Log (this.name + ": OnDrop().");
		
		/* Re-enable collider triggering for this object when the player sets it down. */
		this.collider.enabled = true;	
		
		this.transform.parent = null;
	}
	
	
	
	
	/* Moves the pickup to the correct position relative to the player. */
	public void HandlePosition() {
		
		/* If you have a parent, then move to where you should be relative to him. */
		if (this.transform.parent != null) {
			
			/* If the player turns right, turn the pickup with the player. */
			if (GetPlayer().currentDirection == Player.Direction.RIGHT) {
				this.transform.localPosition = new Vector3(-1.0f*offsetFromPlayer.x, offsetFromPlayer.y, 
					offsetFromPlayer.z);
			}
			/* Otherwise, keep pickup at the normal distance from the player. */
			else {
				this.transform.localPosition = offsetFromPlayer;
			}
		}
	}
	
	
	
	
	/* Returns the player picking up this pickup. */
	private Player GetPlayer() {
		return this.transform.parent.GetComponent<Player>();	
	}

}
