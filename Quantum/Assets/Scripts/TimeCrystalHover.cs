using UnityEngine;
using System.Collections;

public class TimeCrystalHover : MonoBehaviour {

	public float hoverAmount = 1.0f;
	public float hoverTime = 2.0f;

	private float timeUntilChangeDirection;


	// Use this for initialization
	void Start () {
//		timeUntilChangeDirection = hoverTime;
		Hashtable iTweenSettings = new Hashtable();
		iTweenSettings["y"] = hoverAmount;
		iTweenSettings["looptype"] = "pingPong";
		iTweenSettings["time"] = hoverTime;
		iTweenSettings["easetype"] = "easeInOutQuad";

		iTween.MoveBy(this.gameObject, iTweenSettings);
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}
