using UnityEngine;
using System.Collections;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;

public class Goal : MonoBehaviour {
	
	Player player;

	void Start() {
		gameObject.tag = "Goal";
		player = GameObject.FindWithTag("Player").GetComponent<Player>();

		/* 
			A hack. Something resets the goal's box collider to other settings when the game starts,
			so we overwrite those settings here.
		*/
		BoxCollider colliderComponent = GetComponent<BoxCollider>();
		colliderComponent.isTrigger = true;
		colliderComponent.center = Vector3.zero;
		colliderComponent.size = new Vector3(3f, 3f, 3f);
	}

	
	
	/* The Player object is expected to call this method when activating up this object. */
	public void OnActivate(Player player) {
		Debug.Log (this.name + ": OnActivate().");
		
		StartCoroutine("OnLevelCompleteAnimation");
	}


	IEnumerator OnLevelCompleteAnimation() {
		player.canMove = false;
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(2.0f);

		LevelManager.Instance.FadeLevelToBlack(1.0f);
		yield return new WaitForSeconds(1.0f);

		player.canMove = true;
		LevelManager.Instance.OnLevelComplete();
		
	}
		
	
	

}
