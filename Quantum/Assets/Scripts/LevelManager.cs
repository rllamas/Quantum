using UnityEngine;
using System.Collections;

/* 
 * 	A LevelManager is a singleton containing information global to the game.
 * 
 * */
public class LevelManager : MonoBehaviour {
	
	public enum TimePeriod {
		FUTURE,
		PAST
	}
	
	public TimePeriod CurrentEra;
	private static LevelManager singletonInstance = null;
	
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
		return;
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
		
		/* Add necessary Farseer scripts to any Farseer shapes. This is done in Start(), after 
		 * any Farseer shape instances are dynamically created at runtime so that the scripts are
		 * attached to them, too. */
		FSShapeComponent [] shapeComponents = (FSShapeComponent [])FindObjectsOfType(typeof(FSShapeComponent));
		for (int i = 0; i < shapeComponents.Length; ++i) {
			FSShapeComponent currentShapeComponent = shapeComponents[i];
			
			if (!currentShapeComponent.gameObject.GetComponent<FarseerFollowParent>()) {
				currentShapeComponent.gameObject.AddComponent<FarseerFollowParent>();
			}
			
			if (!currentShapeComponent.gameObject.GetComponent<FarseerEnableDisable>()) {
				currentShapeComponent.gameObject.AddComponent<FarseerEnableDisable>();
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

 
 }



