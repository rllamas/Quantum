using UnityEngine;
using System.Collections;


/* 
 * 	A LevelManager is a singletonto govern switching in and out of levels.
 * 
 * */
public class LevelManager : MonoBehaviour {
	

	public TimePeriod CurrentEra;
	private static LevelManager singletonInstance = null;
	public int CurrentLevel = 1;
	public string[] Levels = {
		"scene_tutorial",
		"scene_level_001",
		"scene_level_easy_01",
		"scene_level_medium_01"
	};

	
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
 
	
	void Awake () {
		
		/* Add necessary tilemap scripts to any tilemaps. */
		tk2dTileMap [] tilemaps = (tk2dTileMap [])FindObjectsOfType(typeof(tk2dTileMap));
		for (int i = 0; i < tilemaps.Length; ++i) {
			tk2dTileMap currentTilemap = tilemaps[i];
			
			if (!currentTilemap.gameObject.GetComponent<GenerateTilemapColliders>()) {
				currentTilemap.gameObject.AddComponent<GenerateTilemapColliders>();
			}
			
		}
	}
	
	
   
    void OnApplicationQuit() {
        singletonInstance = null;

    }
 
	
	
	public static bool IsPast() {
		return Instance.CurrentEra == TimePeriod.PAST;
	}
	
	
	public static bool IsFuture() {
		return Instance.CurrentEra == TimePeriod.FUTURE;
	}


	public void OnLevelComplete() {
		Application.LoadLevel(Levels[++CurrentLevel]);
	}

 
 }



