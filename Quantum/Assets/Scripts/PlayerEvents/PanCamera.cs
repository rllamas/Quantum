using UnityEngine;
using System;
using System.Collections;

public class PanCamera: PlayerEvent {
	
	
	private bool alreadyActivated;
	private bool finished;
	private tk2dCamera mainCamera;
	private TimePeriodGUI timePeriodGUI;
	private GameObject cameraPanGUI;


	public Vector3 [] keyframes;
	public float keyframeTransitionSpeed = 25.0f;
	public float timeAtKeyframe = 0.0f;
	public iTween.EaseType transitionEaseType = iTween.EaseType.easeInOutQuart;
	
	public float initialWaitingTime = 1.5f;

	public bool OnActivatedDisableTimeGUI = true;
	public bool OnFinishedEnableTimeGUI = true;

	public bool OnActivatedDisablePlayerMovement = true;
	public bool OnFinishedEnablePlayerMovement = true;

	public bool OnActivatedEnableCameraPanGUI = true;
	public bool OnFinishedDisableCameraPanGUI = true;

	public bool canSkip = true;

	private Vector3 initialPosition;

	
	void Start() {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<tk2dCamera>();
		timePeriodGUI = GameObject.Find("Time Period GUI").GetComponent<TimePeriodGUI>();
		cameraPanGUI = GameObject.Find("Camera Pan GUI");
			
		initialPosition = mainCamera.transform.localPosition;
		cameraPanGUI.SetActive(false);
	}
	
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated) {
			return;	
		}

		base.OnActivate(player);

		if (OnActivatedDisablePlayerMovement) {
			player.canMove = false;
			player.GetComponent<FSWorldComponent>().enabled = false; // Pause Farseer Physics simulation.
		}
		if (OnActivatedDisableTimeGUI) {
			timePeriodGUI.DisableTimePeriodImage();
		}
		if (OnActivatedEnableCameraPanGUI) {
			cameraPanGUI.SetActive(true);
		}

		StartCoroutine("DoEvent");
		
	}
	
	
	
	private IEnumerator DoEvent() {

		alreadyActivated = true;

		yield return new WaitForSeconds(initialWaitingTime);
	
		Hashtable iTweenSettings = new Hashtable();
		iTweenSettings["islocal"] = true;
		iTweenSettings["easetype"] = transitionEaseType;

		Vector3 currentCameraPosition = mainCamera.transform.position;
		for (int i = 0; i < keyframes.Length; ++i) {

			float distance = Vector3.Distance(currentCameraPosition, keyframes[i]);
			float timeBetweenKeyframes = Mathf.Sqrt(distance)/keyframeTransitionSpeed;

			iTweenSettings["position"] = keyframes[i];
			iTweenSettings["time"] = timeBetweenKeyframes;

			iTween.MoveTo(mainCamera.gameObject, iTweenSettings);
			yield return new WaitForSeconds(timeBetweenKeyframes + timeAtKeyframe);
			currentCameraPosition = keyframes[i];
		}

		OnFinish();
	}



	void Update() {
		 if (!finished && Input.GetButtonDown("Action1")) {
			StopCoroutine("DoEvent");

			float distance = Vector3.Distance(mainCamera.transform.localPosition, Vector3.zero);
			float timeBetweenKeyframes = Mathf.Sqrt(distance)/keyframeTransitionSpeed;

			Hashtable iTweenSettings = new Hashtable();
			iTweenSettings["islocal"] = true;
			iTweenSettings["easetype"] = transitionEaseType;
			iTweenSettings["position"] = initialPosition;
			iTweenSettings["time"] = timeBetweenKeyframes;


			iTween.MoveTo(mainCamera.gameObject, iTweenSettings);

			OnFinish();

		}

	}


	void OnFinish() {

		if (OnFinishedEnableTimeGUI) {
			timePeriodGUI.EnableTimePeriodImage();
			timePeriodGUI.PlayTransitionAnimation();
		}
		if (OnFinishedEnablePlayerMovement) {
			player.canMove = true;
			player.GetComponent<FSWorldComponent>().enabled = true; // Unpause Farseer Physics simulation.
		}
		if (OnFinishedDisableCameraPanGUI) {
			cameraPanGUI.SetActive(false);
		}
		finished = true;
	}
	
	
}
