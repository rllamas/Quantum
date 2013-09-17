using UnityEngine;
using System.Collections;

public class Tilemap : MonoBehaviour {
	
	private OTTileMap tilemap;
	
	// Use this for initialization
	void Start () {
		tilemap = this.GetComponent<OTTileMap>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
