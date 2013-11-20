using UnityEngine;
using System.Collections;

public class TitleScreenWithLevelSelect : MonoBehaviour {

	public LevelSlide levelSelect;

	public float amountToPanY = 11.0f;
	public float panRateY = 0.15f;

	private Camera mainCamera;
	
	// Use this for initialization
	void Start () {
		levelSelect = GameObject.Find("Level Select Wheel").GetComponent<LevelSlide>();
		levelSelect.buttonClicksLocked = true;
		levelSelect.gameObject.SetActive(false);

		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKeyDown) {
			//LevelManager.LoadLevelSelect();
			StartCoroutine( PanCamera() );
		}
	}


	IEnumerator PanCamera() {

		levelSelect.gameObject.SetActive(true);

		while (amountToPanY != 0.0f) {

			mainCamera.transform.Translate(0.0f, panRateY, 0.0f);
			amountToPanY = Mathf.Max(0f, amountToPanY - panRateY);

			yield return null;
		}
		levelSelect.buttonClicksLocked = false;
	}
	
}
