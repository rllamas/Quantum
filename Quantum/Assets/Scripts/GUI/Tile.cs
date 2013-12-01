using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	tk2dUIItem uiItem;
	public bool fade = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable() {
		uiItem = GetComponent<tk2dUIItem>();
    	uiItem.OnClickUIItem += Clicked;
	}

	void Clicked(tk2dUIItem clickedUIItem) {
		//if(!fade){
    		////Debug.Log("Clicked:" + clickedUIItem);
			//iTween.MoveTo(this.gameObject, new Vector3( 0.0f,0.0f,-1.3f), 1.5f);
		//}
	}
	

	//Also remember if you are adding event listeners to events you need to also remove them:
	void OnDisable() {
    	uiItem.OnClickUIItem -= Clicked;
	}
}
