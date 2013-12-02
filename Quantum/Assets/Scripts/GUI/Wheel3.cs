using UnityEngine;
using System.Collections;

public class Wheel3 : MonoBehaviour {
	public int totalLevels = 3;
	bool levelPicked = false;
	
	int selectedLevel = 0;
	GameObject[] tiles;
	
	// Use this for initialization
	void Start () {
		tiles = new GameObject[totalLevels];
		for (int i = 0; i < totalLevels; ++i) {
			GameObject currentTile = transform.FindChild("Tile" + i).gameObject;
			
			/* Disable button click tweens. */
			currentTile.transform.FindChild("ButtonGraphic").GetComponent<tk2dUITweenItem>().enabled = false;
			currentTile.GetComponent<tk2dUIItem>().OnClickUIItem += OnClick;
			
			tiles[i] = currentTile;
		}
		
		tiles[0].GetComponent<Tile>().fade = false;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			ScrollLeft();	
		}

		if(Input.GetKeyDown(KeyCode.RightArrow)){
			ScrollRight();	
		}
		
		if(!levelPicked && Input.GetKeyDown(KeyCode.Return)){
			OnClick(tiles[selectedLevel].GetComponent<tk2dUIItem>());
			levelPicked = true;
		}
	}
	
	void OnClick(tk2dUIItem clickedUIItem) {
		/* If clicked center button. */
		if (tiles[selectedLevel] == clickedUIItem.gameObject) {
			/* 
			 * TODO: Do checking for if the level is available.
			 * */
			
			//iTween.ShakePosition(tiles[selectedLevel], new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f)), 2.0f);

			Application.LoadLevel("level_select_group_" + selectedLevel);
			//Debug.Log("Loading:	level_select_group_" + selectedLevel);
//				foreach (GameObject tile in tiles)
//					tile.SetActive(false);
//				transform.FindChild("LeftButton").gameObject.SetActive(false);
//				transform.FindChild("RightButton").gameObject.SetActive(false);
				transform.gameObject.SetActive(false);
		}
		
	}
	
	
	public void ScrollLeft() {
		++selectedLevel;
		
		if (selectedLevel >= totalLevels) {
			selectedLevel = 0;
		}
		
		ScrollAnimation();
		
	}
	
	
	
	public void ScrollRight() {
		--selectedLevel;
		
		if (selectedLevel < 0) {
			selectedLevel = totalLevels - 1;
		}
		
		ScrollAnimation();
		
	}
	
	
	
	void ScrollAnimation() {
		switch (selectedLevel) {
			case 0:
        		//Debug.Log("Case 1");
				iTween.MoveTo(tiles[0], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[1], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
        		break;
    		case 1:
        		//Debug.Log("Case 2");
				iTween.MoveTo(tiles[1], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[2], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
        		break;
    		case 2:
        		//Debug.Log("Case 3");
				iTween.MoveTo(tiles[2], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[0], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
        		break;	
		}
	}
	
	
}
