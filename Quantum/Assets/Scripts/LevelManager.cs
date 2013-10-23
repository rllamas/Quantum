using UnityEngine;
using System;
using System.Collections;


/* 
 * 	A LevelManager is a singletonto govern switching in and out of levels.
 * 
 * */
public class LevelManager : MonoBehaviour {
	

	public TimePeriod CurrentEra;
	private static LevelManager singletonInstance = null;
	private int CurrentLevel = 0;
	private string[] Levels = {
		"scene_level_tutorial",
		"scene_level_001",
		"scene_level_easy_01",
		"scene_level_medium_01"
	};

	public MeshFilter animationCurtain;

	/* The animation manager to display the current era on level transitions. */
	public tk2dSpriteAnimator eraAnimator;

	
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

		animationCurtain =  this.transform.FindChild("Curtain").GetComponent<MeshFilter>();
		animationCurtain.gameObject.SetActive(true);
		//animationCurtain.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

		eraAnimator =  this.transform.FindChild("Current Era Animation").GetComponent<tk2dSpriteAnimator>();
		StartCoroutine("StartLevelAnimation");
	}
	
	
   
    void OnApplicationQuit() {
        singletonInstance = null;

    }

    public void FadeLevelToBlack(float time) {
    	iTween.FadeTo(animationCurtain.gameObject, 1.0f, time);
    }


    public void FadeBlackToLevel(float time) {
    	iTween.FadeTo(animationCurtain.gameObject, 0.0f, time);
    }
 

	IEnumerator StartLevelAnimation() {
		eraAnimator.gameObject.SetActive(true);
		if (IsPast()) {
			eraAnimator.Play("clockBCE");
		}
		else {
			eraAnimator.Play("clockCE");
		}
		yield return new WaitForSeconds(1.0f);

		FadeBlackToLevel(1.0f);
		yield return new WaitForSeconds(2.5f);

		eraAnimator.gameObject.SetActive(false);
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

		Application.LoadLevel(Levels[++CurrentLevel]);
	}


	private int GetCurrentLevelNumber() {
		for (int i = 0; i < Levels.Length; ++i) {

			if (Levels[i].Equals(Application.loadedLevelName)) {
				return i;
			}
		}
		return -1;
	}

 
}



