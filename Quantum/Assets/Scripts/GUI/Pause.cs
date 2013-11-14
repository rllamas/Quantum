using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	GameObject pCam;
	GameObject normCam;
	
	// Use this for initialization
	void Start () {
		pCam = transform.FindChild("PauseCamera").gameObject;
		normCam = GameObject.Find("tk2dCamera").gameObject;
		Debug.Log("Hello");
		pCam.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			
			pauseGame ();
			changeCamera();
		}
	}
	
	void changeCamera(){
		if(!pCam.activeSelf){
			normCam.SetActive(false);
			pCam.SetActive(true);
		}else{
			normCam.SetActive(true);
			pCam.SetActive(false);
		}
	}
	
	
	void pauseGame(){
		if (Time.timeScale == 1.0F)
            Time.timeScale = 0.0F;
        else
        	Time.timeScale = 1.0F;
	}
}
