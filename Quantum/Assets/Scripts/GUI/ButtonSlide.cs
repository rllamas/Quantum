using UnityEngine;
using System.Collections;

public class ButtonSlide : MonoBehaviour {
	tk2dUIItem uiItem;
	GameObject wheel;
	
	// Use this for initialization
	void Start () {
		wheel = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnEnable() {
		uiItem = GetComponent<tk2dUIItem>();
		uiItem.OnClickUIItem += Clicked;
	}
	
	void Clicked(tk2dUIItem clickedUIItem) {
		
		if(transform.tag.Equals("left"))
			wheel.GetComponent<LevelSlide>().ScrollLeft();
		
		if(transform.tag.Equals("right"))
			wheel.GetComponent<LevelSlide>().ScrollRight();
		
	}
	
	//Also remember if you are adding event listeners to events you need to also remove them:
	void OnDisable() {
		uiItem.OnClickUIItem -= Clicked;
	}
}
