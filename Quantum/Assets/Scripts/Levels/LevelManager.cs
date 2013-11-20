using UnityEngine;
using System;
using System.Collections;


/* 
 * 	A LevelManager is a singleton to govern switching in and out of levels.
 * 
 * */
public class LevelManager : MonoBehaviour {
	

	public TimePeriod CurrentEra;
	private static LevelManager singletonInstance = null;
	private int CurrentLevel = 0; // -1 is used for controls scene

	private string[] Levels = {
		"scene_level_00",
		"scene_level_01",
		"scene_level_02",
		"scene_level_03",
		"scene_level_04",
		"scene_level_05",
		"scene_level_06",
	};
	private string winScene = "scene_win_01";
	private string levelSelectScene = "level_select_beta";

	public MeshFilter animationCurtain;
	private tk2dTextMesh levelNameText;
	
	public static LevelManager Instance {
		get {
			
			/* If first time accessing instance, then find it... */
		    if (singletonInstance == null) {
		        singletonInstance =  FindObjectOfType(typeof (LevelManager)) as LevelManager;
		    }
		
		    /* If instance is null, then no GameManager exists in the scene, so create one. */
		    if (singletonInstance == null) {
		        GameObject obj = new GameObject("LevelManager");
		        singletonInstance = obj.AddComponent(typeof (LevelManager)) as LevelManager;
				obj.name = "Level Manager";
		        Debug.Log ("Could not find a LevelManager object, so automatically generated one.");
		    }
		
		    return singletonInstance;
		}
		
    }
 
	
	void Awake() {
		
		/* Add necessary tilemap scripts to any tilemaps. */
		tk2dTileMap [] tilemaps = (tk2dTileMap [])FindObjectsOfType(typeof(tk2dTileMap));
		for (int i = 0; i < tilemaps.Length; ++i) {
			tk2dTileMap currentTilemap = tilemaps[i];
			
			if (!currentTilemap.gameObject.GetComponent<GenerateTilemapColliders>()) {
				currentTilemap.gameObject.AddComponent<GenerateTilemapColliders>();
			}
			
		}
	}



	void Start() {
		CurrentLevel = GetCurrentLevelNumber();
		
		Transform curtain = this.transform.FindChild("Curtain");
		if (curtain != null) {
			animationCurtain =  curtain.GetComponent<MeshFilter>();
			animationCurtain.gameObject.SetActive(true);
			
			levelNameText = this.transform.FindChild("Level Name").GetComponent<tk2dTextMesh>();
			levelNameText.gameObject.SetActive(true);
	
			StartCoroutine("StartLevelAnimation");
		}
	}
	
	
   
    void OnApplicationQuit() {
        singletonInstance = null;

    }

    public void FadeLevelToBlack(float time) {
    	iTween.FadeTo(animationCurtain.gameObject, 1.0f, time);
    }


    private void FadeBlackToLevel(float time) {
    	iTween.FadeTo(animationCurtain.gameObject, 0.0f, time);
    }
	
	
	private IEnumerator FadeLevelName(float time, float rate) {
    	while (true) {
			time = Mathf.Max(0.0f, time-rate*Time.deltaTime);
			
			Color oldColor = levelNameText.color;
			levelNameText.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1.0f*time);
			
			if (time == 0.0f) {
				yield break;
			}
			
			yield return null;

		}
    }
 

	IEnumerator StartLevelAnimation() {
		if (CurrentLevel == -1) { // Controls scene
			levelNameText.text = "";
		}
		else if (CurrentLevel == 0) {
			levelNameText.text = "Tutorial";
		}
		else if (CurrentLevel < 10) {
			levelNameText.text = "Level 0" + CurrentLevel;
		}
		else {
			levelNameText.text = "Level " + CurrentLevel;	
		}
			
		yield return new WaitForSeconds(1.0f);
		
		StartCoroutine( FadeLevelName(1.0f, 2.0f) );
		FadeBlackToLevel(1.0f);
		yield return new WaitForSeconds(2.5f);

	}


	public static bool IsPast() {
		return Instance.CurrentEra == TimePeriod.PAST;
	}
	
	
	public static bool IsFuture() {
		return Instance.CurrentEra == TimePeriod.FUTURE;
	}


	public void OnLevelComplete() {
		if (CurrentLevel == -1) {
			throw new Exception("There's no subsequent level to go to!");
		}

		++CurrentLevel;
		if (CurrentLevel >= Levels.Length) {
			Application.LoadLevel(winScene);
		}
		else {
			Application.LoadLevel(Levels[CurrentLevel]);
		}
	}
	


	private int GetCurrentLevelNumber() {
		for (int i = 0; i < Levels.Length; ++i) {

			if (Levels[i].Equals(Application.loadedLevelName)) {
				return i;
			}
		}
		return -1;
	}
	
	
	public static void LoadLevel(int levelNumber) {
		if (levelNumber < 0 || levelNumber >= Instance.Levels.Length) {
			throw new Exception("Bad value for level number!");	
		}
		Instance.CurrentLevel = levelNumber;
		Application.LoadLevel(Instance.Levels[levelNumber]);
	}
	
	
	public static void LoadLevelSelect() {

		Instance.CurrentLevel = -1;
		Application.LoadLevel(Instance.levelSelectScene);
	}
	
	
	public int GetCurrentLevel () {
		return GetCurrentLevelNumber();	
	}
 
}



