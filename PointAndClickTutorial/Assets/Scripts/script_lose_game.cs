using UnityEngine;
using System.Collections;

public class script_lose_game : MonoBehaviour {

	void OnGUI () {
		GUI.Label(new Rect(210, 100, 100, 50), "You lose... :(");
		bool playGame = GUI.Button(new Rect(140, 150, 100, 50), "Replay game!");
		bool exitGame = GUI.Button(new Rect(260, 150, 100, 50), "Exit game...");
		
		if (playGame) {
			Debug.Log("Restarting game...");
			Application.LoadLevel("scene_level_001");
		}
		else if (exitGame) {
			Debug.Log ("Exiting game...");
			Application.Quit();
		}
	}
}
