using UnityEngine;
using System.Collections;

public class TimePeriodGUI : MonoBehaviour {
	
	
	/* The animation manager to display the current era on level transitions. */
	private tk2dSpriteAnimator timeTravelAnimator;
	
	/* Text manager displaying the current era. */
	//private tk2dTextMesh timePeriodText;
	private tk2dSprite timePeriodImage;
	
	
	private bool playingAnimation;
	

	// Use this for initialization
	void Start () {
		
		timeTravelAnimator =  this.transform.FindChild("Time Travel Animation").GetComponent<tk2dSpriteAnimator>();
		//timePeriodText =  this.transform.FindChild("Time Period Text").GetComponent<tk2dTextMesh>();
		timePeriodImage =  this.transform.FindChild("Time Period Image").GetComponent<tk2dSprite>();
	
		
		timeTravelAnimator.gameObject.SetActive(true);
		timeTravelAnimator.gameObject.SetActive(false);
		
		playingAnimation = false;
		
		if (LevelManager.IsFuture()) {
			//timePeriodText.text = "future";
			timePeriodImage.SetSprite("gui_future_text");
		}
		else {	
			//timePeriodText.text = "past";
			timePeriodImage.SetSprite("gui_past_text");
		}	
		
		//StartCoroutine("PlayTransitionAnimation");
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
	
	
	
	
	IEnumerator PlayTransitionAnimationHelper() {
		
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
			//timePeriodText.text = "future";
			timePeriodImage.SetSprite("gui_future_text");
		}
		else {
			timeTravelAnimator.Play("clockBCE");	
			//timePeriodText.text = "past";
			timePeriodImage.SetSprite("gui_past_text");
		}	
		
		yield return new WaitForSeconds(3.0f);
		
		timeTravelAnimator.gameObject.SetActive(false);	
		
	}


	public void PlayTransitionAnimation() {
		StartCoroutine("PlayTransitionAnimationHelper");
	}

	public void EnableTimePeriodImage() {
		timePeriodImage.gameObject.SetActive(true);
	}

	public void DisableTimePeriodImage() {
		timePeriodImage.gameObject.SetActive(false);
	}
}
