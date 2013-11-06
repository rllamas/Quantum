using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {
	tk2dUIItem uiItem;
	GameObject wheel;
	
	// Use this for initialization
	void Start () {
		wheel = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Clicked(this.GetComponent<tk2dUIItem>());
		}
	}
	
	void OnEnable() {
		uiItem = GetComponent<tk2dUIItem>();
    	uiItem.OnClickUIItem += Clicked;
	}

	void Clicked(tk2dUIItem clickedUIItem) {
		
		Application.LoadLevel("level_select_main");
		wheel.SetActive(false);
		
	}

	//Also remember if you are adding event listeners to events you need to also remove them:
	void OnDisable() {
    	uiItem.OnClickUIItem -= Clicked;
	}
}
