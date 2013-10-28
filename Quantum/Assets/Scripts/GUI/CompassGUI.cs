using UnityEngine;
using System.Collections;

public class CompassGUI : MonoBehaviour {
	
	private Player player;
	private Goal goal;
	
	private tk2dSprite arrowSprite;
	private float cooldownMaxTime = 0.25f;
	private float currentCooldown = 0.0f;
	
	private float pulseAnimationDistance = 10.0f;
		
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		goal = GameObject.FindWithTag("Goal").GetComponent<Goal>();
		
		arrowSprite = this.transform.FindChild("Arrow Sprite").GetComponent<tk2dSprite>();

	}
	
	
	
	// Update is called once per frame
	void Update () {
			currentCooldown = Mathf.Max(0.0f, currentCooldown-Time.deltaTime);
		
			/* Play pulse animation if player is close enough to goal. */
			if (Vector3.Distance(player.transform.position, goal.transform.position) <= pulseAnimationDistance && currentCooldown == 0.0f) {
				iTween.ScaleFrom(arrowSprite.gameObject, new Vector3(1.5f, 1.5f, 1.5f), cooldownMaxTime);
				currentCooldown = cooldownMaxTime;
			}
		
			/* Make the arrow turn smoothly towards the goal. */
			Quaternion newRotation = Quaternion.LookRotation(player.transform.position - goal.transform.position, Vector3.forward);
			newRotation.x = 0.0f;
			newRotation.y = 0.0f;
	
	    	arrowSprite.transform.rotation = Quaternion.Slerp(arrowSprite.transform.rotation, newRotation, Time.deltaTime * 8.0f);

	}
}
