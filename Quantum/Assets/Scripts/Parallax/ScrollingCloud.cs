using UnityEngine;
using System.Collections;

public class ScrollingCloud : MonoBehaviour {
	
	public float scrollSpeed = 0.5f;
	
	public float amplitude = 1.0f;
	public float frequency = 1.0f;
	public float cycle = 0.0f;
	
	private Vector3 originalPosition;
	
	// Use this for initialization
	void Start () {
		originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.material.SetTextureOffset("_MainTex", new Vector2(scrollSpeed*Time.time, 0.0f));
		
		this.transform.position = new Vector3(0.0f, amplitude*Mathf.Sin(cycle*Mathf.PI + frequency*Time.time*Mathf.PI)) + originalPosition;
	}
}
