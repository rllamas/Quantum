using UnityEngine;
using System.Collections;

public class PauseGUI : MonoBehaviour {
	tk2dUIItem uiItem;
	Pause pause;
	bool mapping = false;
	GameObject resume;
	GameObject menu;
	GameObject reset;
	
	// Use this for initialization
	void Start () {
		pause = transform.parent.transform.parent.gameObject.GetComponent<Pause>();
		resume = GameObject.Find("Resume").gameObject;
		reset = GameObject.Find("Reset").gameObject;
		menu = GameObject.Find("Main Menu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnEnable() {
		uiItem = GetComponent<tk2dUIItem>();
		uiItem.OnClickUIItem += Clicked;
	}
	
	void Clicked(tk2dUIItem clickedUIItem) {
		if(this.gameObject.name.Equals("Resume")){
			//Debug.Log("Resuming game...");
			pause.pauseGame();
			pause.changeCamera();
		}else if(this.gameObject.name.Equals("Main Menu")){
			//transform.Find("LevelManager").gameObject.GetComponent<LevelManager>().LoadLevelSelect();
			Application.LoadLevel("scene_title_with_level_select");
			pause.pauseGame();
		}else if(this.gameObject.name.Equals("Reset")){
			//int level = transform.Find("LevelManager").gameObject.GetComponent<LevelManager>().GetCurrentLevelNumber();
			Application.LoadLevel(Application.loadedLevelName);
			pause.pauseGame();
		}else if(this.gameObject.name.Equals("Map")){
			if(!mapping){
				mapping = true;
				resume.SetActive(false);
				reset.SetActive(false);
				menu.SetActive(false);
			}else{
				mapping = false;
				resume.SetActive(true);
				reset.SetActive(true);
				menu.SetActive(true);
			}
		}
	}
	

	//Also remember if you are adding event listeners to events you need to also remove them:
	void OnDisable() {
		uiItem.OnClickUIItem -= Clicked;
	}
}
