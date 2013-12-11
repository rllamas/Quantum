using UnityEngine;
using System;
using System.Collections;

public class WinScreen : MonoBehaviour {

	tk2dTextMesh textMesh;
	int currentPage;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<tk2dTextMesh>();
	}
	
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKeyDown) {
			switch (currentPage) {
				case 0:
					textMesh.text = "Credits:\n\n\n    Esteben Zaldivar\n\n    Mitchell Crites-Krumm\n\n    Rene Garcia\n\n    Ricky Llamas\n\n    Russell Jahn";
					++currentPage;
					break;
				case 1:
					LevelManager.LoadLevelSelect();
					break;
				default:
					throw new Exception("currentPage out of bounds!");
					break;
			}
		}
	}
	
}
