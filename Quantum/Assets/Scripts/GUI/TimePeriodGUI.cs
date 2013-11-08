using UnityEngine;
using System.Collections;

public class TimePeriodGUI : MonoBehaviour {
	
	
	/* The animation manager to display the current era on level transitions. */
	private tk2dSpriteAnimator timeTravelAnimator;
	
	/* Text manager displaying the current era. */
	private tk2dTextMesh timePeriodText;
	
	
	private bool playingAnimation;
	

	// Use this for initialization
	void Start () {
		
		timeTravelAnimator =  this.transform.FindChild("Time Travel Animation").GetComponent<tk2dSpriteAnimator>();
		timePeriodText =  this.transform.FindChild("Time Period Text").GetComponent<tk2dTextMesh>();
	
		
		timeTravelAnimator.gameObject.SetActive(true);
		timeTravelAnimator.gameObject.SetActive(false);
		
		playingAnimation = false;
		
		if (LevelManager.IsFuture()) {
			timePeriodText.text = "future";
		}
		else {	
			timePeriodText.text = "past";
		}	
		
		StartCoroutine("PlayTransitionAnimation");
	}
	
	
	
	// Update is called once per frame
	void Update () {
		

		if (Vortex.CurrentlyWarping() && !playingAnimation) {
			playingAnimation = true;
			StopCoroutine("PlayTransitionAnimation");
			StartCoroutine("PlayTransitionAnimation");	
		}
		
		else if (!Vortex.CurrentlyWarping()) {
			playingAnimation = false;	
		}
		

	}
	
	
	
	
	IEnumerator DelayedPauseAnimation() {
		yield return new WaitForSeconds(2.0f);
		timeTravelAnimator.Pause();	
	}
	
	
	
	
	IEnumerator PlayTransitionAnimation() {
		
		timeTravelAnimator.gameObject.SetActive(true);
		
		if (LevelManager.IsPast()) {
			timeTravelAnimator.Play("clockCEToBCE");
		}
		else {
			timeTravelAnimator.Play("clockBCEToCE");	
		}	
		
		yield return new WaitForSeconds(2.0f);
		
		if (LevelManager.IsFuture()) {
			timeTravelAnimator.Play("clockCE");
			timePeriodText.text = "future";
		}
		else {
			timeTravelAnimator.Play("clockBCE");	
			timePeriodText.text = "past";
		}	
		
		yield return new WaitForSeconds(3.0f);
		
		timeTravelAnimator.gameObject.SetActive(false);	
		
	}
}
