using UnityEngine;
using System.Collections;

public class Wheel5 : MonoBehaviour {

	public int totalLevels = 5;
	bool levelPicked = false;
	
	int selectedLevel = 0;
	GameObject[] tiles;
	
	
	
	/* Used to control rotation speed when user holds down left/right/analog tilt. */
	float maxScrollTimeout = 0.2f;
	float currentScrollTimeout = 0.0f;
	
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
			if (selectedLevel == 0) {
				//iTween.ShakePosition(tiles[selectedLevel], new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f)), 2.0f);
				LevelManager.LoadTutorial();
				
			}
		
		}
		
	}
	
	
	
	/* Scroll the wheel left by 1 tile. */
	public void ScrollLeft() {
		
		++selectedLevel;
		
		if (selectedLevel >= totalLevels) {
			selectedLevel = 0;
		}
		
		ScrollAnimation();
		
	}
	
	
	
	/* Scroll the wheel right by 1 tile. */
	public void ScrollRight() {
		
		--selectedLevel;
		
		if (selectedLevel < 0) {
			selectedLevel = totalLevels - 1;
		}
		
		ScrollAnimation();
		
	}
	
	
	
	void ScrollAnimation() {
		Debug.Log ("currentLevel: " + selectedLevel);
		
		switch (selectedLevel) {
			case 0:
        		//Debug.Log("Case 1");
				iTween.MoveTo(tiles[0], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[1], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[3], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[4], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
        		break;
    		case 1:
        		//Debug.Log("Case 2");
				iTween.MoveTo(tiles[1], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[2], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[3], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[4], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
        		break;
    		case 2:
        		//Debug.Log("Case 3");
				iTween.MoveTo(tiles[2], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[3], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[4], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
        		break;
			case 3:
        		//Debug.Log("Case 4");
				iTween.MoveTo(tiles[3], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[4], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
        		break;
			case 4:
        		//Debug.Log("Case 5");
				iTween.MoveTo(tiles[4], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[0], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[3], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
        		break;
		}
	}
}
