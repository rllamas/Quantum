using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	public Player player;
	public Vector3 playerSpawnLocation = new Vector3(-80.0f, -300.0f, 0.0f);
	
	// Use this for initialization
	void Start () {
		if (FindObjectOfType(typeof(Player)) == null) {
			player = Object.Instantiate(player, playerSpawnLocation, Quaternion.identity) as Player;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
