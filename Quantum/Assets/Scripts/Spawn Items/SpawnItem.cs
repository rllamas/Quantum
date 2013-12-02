using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour {


	public Vector3 spawnVelocityMin = new Vector3(-10.0f, 0.0f, 0.0f);
	public Vector3 spawnVelocityMax = new Vector3( 0.0f,  0.0f, 0.0f);

	public float spawnSizeMin = 0.75f;
	public float spawnSizeMax = 1.20f;

	private Vector3 currentVelocity;
	private Vector3 initialPosition;

	public float timeUntilDeath = 5.0f;

	public bool killOnPast = false;
	public bool killOnFuture = true;

	// Use this for initialization
	void Start () {
		currentVelocity = new Vector3(
			0.5f*(spawnVelocityMax.x - spawnVelocityMin.x) + Random.Range(spawnVelocityMin.x, spawnVelocityMax.x),
			0.5f*(spawnVelocityMax.y - spawnVelocityMin.y) + Random.Range(spawnVelocityMin.y, spawnVelocityMax.y),
			0.5f*(spawnVelocityMax.z - spawnVelocityMin.z) + Random.Range(spawnVelocityMin.z, spawnVelocityMax.z)
		);

		initialPosition = new Vector3(
			this.transform.position.x,
			this.transform.position.y,
			this.transform.position.z + currentVelocity.z
		);

		this.transform.localScale = (new Vector3(1.0f, 1.0f, 1.0f)) * Random.Range(spawnSizeMin, spawnSizeMax);
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = new Vector3(
			this.transform.position.x + currentVelocity.x*Time.deltaTime,
			initialPosition.y + currentVelocity.y*Mathf.Sin(Time.time),
			initialPosition.z
		);

		timeUntilDeath = Mathf.Max(timeUntilDeath-Time.deltaTime, 0.0f);

		if (timeUntilDeath == 0.0f) {
			Destroy(this.gameObject);
		}
		if (killOnPast && LevelManager.IsPast()) {
			Destroy(this.gameObject);
		}
		if (killOnFuture && LevelManager.IsFuture()) {
			Destroy(this.gameObject);
		}
	}
}
