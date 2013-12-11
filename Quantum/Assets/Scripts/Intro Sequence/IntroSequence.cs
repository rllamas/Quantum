using UnityEngine;
using System;
using System.Collections;

public class IntroSequence : MonoBehaviour {

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
				textMesh.text = "Click again to load tutorial.";
					++currentPage;
					break;
				case 1:
					LevelManager.LoadTutorial();
					break;
				default:
					throw new Exception("currentPage out of bounds!");
					break;
			}
		}
	}
	
}
