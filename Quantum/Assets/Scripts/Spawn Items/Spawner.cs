using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public string [] itemsToSpawn; // Relative to resources folder
	public float timeBetweenSpawning = 2.0f;
	public Vector3 spawnRange = new Vector3(0.0f, 10.0f, 0.0f);

	public float maxRandomTimeBetweenSpawns = 1.0f;

	public bool shouldSpawn = true;
	


	/* Whenever this object is activated. */
	void OnEnable () {
		StartCoroutine("Spawn");
	}




	
	// Update is called once per frame
	void Update () {

	}




	IEnumerator Spawn() {
		while (shouldSpawn) {
			GameObject itemToSpawn = Resources.Load(itemsToSpawn[Random.Range(0, itemsToSpawn.Length-1)]) as GameObject; 

			Vector3 newSpawnLocalPosition = new Vector3(
				-spawnRange.x/2.0f + Random.Range(0.0f, spawnRange.x),
				-spawnRange.y/2.0f + Random.Range(0.0f, spawnRange.y),
				-spawnRange.z/2.0f + Random.Range(0.0f, spawnRange.z)
			);

			GameObject newItem = Instantiate(itemToSpawn.gameObject, this.transform.position + newSpawnLocalPosition, Quaternion.identity) as GameObject;
//			newItem.transform.parent = this.transform;

			float extraWaitTime = -0.5f*maxRandomTimeBetweenSpawns + maxRandomTimeBetweenSpawns;

			yield return new WaitForSeconds(Mathf.Max(0.0f, timeBetweenSpawning+extraWaitTime));
		}
	}





}
