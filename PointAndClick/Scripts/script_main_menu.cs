using UnityEngine;
using System.Collections;

public class script_main_menu : MonoBehaviour {

	
	void OnGUI () {
		
		bool playGame = GUI.Button(new Rect(140, 150, 100, 50), "Play game!");
		bool exitGame = GUI.Button(new Rect(260, 150, 100, 50), "Exit game...");
		
		if (playGame) {
			Debug.Log("Starting game...");
			Application.LoadLevel("scene_level_001");
		}
		else if (exitGame) {
			Debug.Log ("Exiting game...");
			Application.Quit();
		}
	}
}
