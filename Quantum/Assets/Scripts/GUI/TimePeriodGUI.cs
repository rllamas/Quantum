using UnityEngine;
using System.Collections;

public class TimePeriodGUI : MonoBehaviour {
	
	private Player player;
	
	/* The animation manager to display the current era on level transitions. */
	private tk2dSpriteAnimator currentEraAnimator;
	
	/* These keep track of state for the frame. */
	private TimePeriod eraPreviousFrame;
	private TimePeriod eraCurrentFrame;
	
	private tk2dCamera mainCamera;
	
	private bool playingAnimation;

	// Use this for initialization
	void Start () {
		
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<tk2dCamera>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		currentEraAnimator =  this.transform.FindChild("Current Era Animation").GetComponent<tk2dSpriteAnimator>();
	
		eraPreviousFrame = LevelManager.Instance.CurrentEra;
		eraCurrentFrame = LevelManager.Instance.CurrentEra;
		
		if (eraCurrentFrame == TimePeriod.FUTURE) {
			currentEraAnimator.Play("clockCE");
		}
		else {
			currentEraAnimator.Play("clockBCE");	
		}
		
		playingAnimation = false;
		StartCoroutine("DelayedPauseAnimation");	
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		eraPreviousFrame = eraCurrentFrame;
		eraCurrentFrame = LevelManager.Instance.CurrentEra;

		
		if (Vortex.CurrentlyWarping() && !playingAnimation) {
			Debug.Log("Couroutine.");
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
		currentEraAnimator.Pause();	
	}
	
	
	
	
	IEnumerator PlayTransitionAnimation() {

		if (eraCurrentFrame == TimePeriod.FUTURE) {
			currentEraAnimator.Paused = false;
			currentEraAnimator.Play("clockCEToBCE");
			yield return new WaitForSeconds(1.5f);
			currentEraAnimator.Play("clockBCE");
			yield return new WaitForSeconds(2.0f);
			currentEraAnimator.Pause();
		}
		else {
			currentEraAnimator.Paused = false;
			currentEraAnimator.Play("clockBCEToCE");	
			yield return new WaitForSeconds(1.5f);
			currentEraAnimator.Play("clockCE");
			yield return new WaitForSeconds(2.0f);
			currentEraAnimator.Pause();
		}	
		
	}
}
