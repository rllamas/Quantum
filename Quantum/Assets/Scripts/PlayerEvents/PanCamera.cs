using UnityEngine;
using System;
using System.Collections;

public class PanCamera: PlayerEvent {
	
	
	private bool alreadyActivated;
	private tk2dCamera mainCamera;
	private TimePeriodGUI timePeriodGUI;


	public Vector3 [] keyframes;
	public float keyframeTransitionSpeed = 25.0f;
	public float timeAtKeyframe = 0.0f;
	public iTween.EaseType transitionEaseType = iTween.EaseType.easeInOutQuart;
	
	public float initialWaitingTime = 1.5f;

	public bool OnActivatedDisableTimeGUI = true;
	public bool OnFinishedEnableTimeGUI = true;

	public bool OnActivatedDisablePlayerMovement = true;
	public bool OnFinishedEnablePlayerMovement = true;



	
	void Start() {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<tk2dCamera>();
		timePeriodGUI = GameObject.Find("Time Period GUI").GetComponent<TimePeriodGUI>();
	}
	
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated) {
			return;	
		}

		base.OnActivate(player);

		if (OnActivatedDisablePlayerMovement) {
			player.canMove = false;
		}
		if (OnActivatedDisableTimeGUI) {
			timePeriodGUI.DisableTimePeriodImage();
		}

		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {

		//Debug.Log("In DoEvent()!");
		alreadyActivated = true;

		yield return new WaitForSeconds(initialWaitingTime);
	
		Hashtable iTweenSettings = new Hashtable();
		iTweenSettings["islocal"] = true;
		iTweenSettings["easetype"] = transitionEaseType;

		Vector3 currentCameraPosition = mainCamera.transform.position;
		for (int i = 0; i < keyframes.Length; ++i) {

			float distance = Vector3.Distance(currentCameraPosition, keyframes[i]);
			float timeBetweenKeyframes = Mathf.Sqrt(distance)/keyframeTransitionSpeed;

//			Debug.Log ("timeBetweenKeyFrames: " + timeBetweenKeyframes);

			iTweenSettings["position"] = keyframes[i];
			iTweenSettings["time"] = timeBetweenKeyframes;

			iTween.MoveTo(mainCamera.gameObject, iTweenSettings);
			yield return new WaitForSeconds(timeBetweenKeyframes + timeAtKeyframe);
			currentCameraPosition = keyframes[i];
		}

		if (OnFinishedEnableTimeGUI) {
			timePeriodGUI.EnableTimePeriodImage();
			timePeriodGUI.PlayTransitionAnimation();
		}
		if (OnFinishedEnablePlayerMovement) {
			player.canMove = true;
		}
	}
	

	
}
