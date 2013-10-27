using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Parallax : MonoBehaviour {
	
	public float speed = 0.25f;
	public GameObject [] layersToParallax;
	private Player player;
	private Vector3 lastPlayerPosition;
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		lastPlayerPosition = player.transform.position;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (lastPlayerPosition == player.transform.position) {
			return;	
		}
		
		for (int i = 0; i < layersToParallax.Length; ++i) {

			GameObject currentLayer = layersToParallax[i];
			if (currentLayer != null && currentLayer.transform != null) {
				float amountToTranslateX = (player.transform.position - lastPlayerPosition).x * Mathf.Pow(speed, i+1);
				currentLayer.transform.Translate(amountToTranslateX, 0.0f, 0.0f);
			}
			
		}
		lastPlayerPosition = player.transform.position;
	}
	
}
