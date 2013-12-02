using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	void OnGUI () {
		
		// Write text to screen
		GUI.Label(new Rect(10,10,100,90), "You win!");

		// Make the first button. If it is pressed, the level is reloaded
		if(GUI.Button(new Rect(20,40,80,20), "Replay?")) {
			Application.LoadLevel("scene_prototype_level_mk2");
		}
	}
}