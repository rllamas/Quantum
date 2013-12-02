using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {


	private GameObject dactylSpawner1;


	// Use this for initialization
	void Start () {
		dactylSpawner1 = this.transform.FindChild("Dactyl Spawner 1").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
		dactylSpawner1.SetActive( LevelManager.IsPast() );
	
	}
}
