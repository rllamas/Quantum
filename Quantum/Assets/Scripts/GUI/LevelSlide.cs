using UnityEngine;
using System.Collections;

public class LevelSlide : MonoBehaviour {
	
	public int totalLevels = 7;
	bool levelPicked = false;
	
	int selectedLevel = 0;
	int highestLevelUnlocked = 0;
	public GameObject[] tiles;

	private Vector3 originalTile0Position;
	public Color fadeColor = new Color(0.7f, 0.45f, 0.45f, 1.0f);

	public bool buttonClicksLocked = false; // Can be set in other scripts.
	
	/* Used to control rotation speed when user holds down left/right/analog tilt. */
	//float maxScrollTimeout = 0.2f;
	//float currentScrollTimeout = 0.0f;
	
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

		originalTile0Position = tiles[0].transform.position;
		highestLevelUnlocked = 0; // TODO: Replace this with the furthest level the player has gotten to.

		FadeUnavailableLevels();
	}
	
	
	
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			ScrollLeft();	
		}
		
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			ScrollRight();	
		}
		
		if(!levelPicked && Input.GetKeyDown(KeyCode.Return)){
			OnClick(tiles[selectedLevel].GetComponent<tk2dUIItem>());
			levelPicked = true;
		}

		/* If hitting back button, load main menu. */
		if (Input.GetKeyDown(KeyCode.Escape)) {

			Application.LoadLevel("level_select_main");	

			foreach (GameObject tile in tiles) {
				tile.SetActive(false);
			}

			transform.FindChild("LeftButton").gameObject.SetActive(false);
			transform.FindChild("RightButton").gameObject.SetActive(false);
		}

		if (Input.GetButtonDown("Action1") || Input.GetButtonDown("Jump")) {
			HandleButtonClick();
		}
	}
	
	
	void OnClick(tk2dUIItem clickedUIItem) {
		HandleButtonClick();
	}
	
	
	
	/* Scroll the wheel left by 1 tile. */
	public void ScrollLeft() {
		
		++selectedLevel;
		
		if (selectedLevel >= totalLevels) {
			selectedLevel = 0;
		}

		for (int i = 0; i < totalLevels; i++) {
				
			if (i == selectedLevel) {
				iTween.MoveTo(tiles[i], originalTile0Position, 1.00f);
			}
			else {
				float curX = tiles[i].transform.position.x;
				iTween.MoveTo(tiles[i], new Vector3(curX - 8.0f, originalTile0Position.y, originalTile0Position.z + 2.0f), 1.00f);
			}
		}
	}
	
	
	
	/* Scroll the wheel right by 1 tile. */
	public void ScrollRight() {
		
		--selectedLevel;
		
		if (selectedLevel < 0) {
			selectedLevel = totalLevels - 1;
		}

		for (int i = 0; i < totalLevels; i++){
				
			if (i == selectedLevel) {
				iTween.MoveTo(tiles[i], originalTile0Position, 1.00f);
			}
			else {
				float curX = tiles[i].transform.position.x;
				iTween.MoveTo(tiles[i], new Vector3(curX + 8.0f, originalTile0Position.y, originalTile0Position.z + 2.0f), 1.00f);
			}
		}
	}


	public void FadeUnavailableLevels() {

		/* Fade out any levels that haven't been unlocked. */
		for (int i = 0; i < totalLevels; i++) {

			tk2dSlicedSprite currentTileSprite = tiles[i].transform.Find("ButtonGraphic").GetComponent<tk2dSlicedSprite>();

			if (i <= highestLevelUnlocked) {
				currentTileSprite.color = Color.white; // Fully opaque if unlocked.
			}
			else {
				currentTileSprite.color = fadeColor; // Transparent if not unlocked yet.
			}
		}

	}


	public void HandleButtonClick() {
	
		if (!buttonClicksLocked) {
			if (selectedLevel <= highestLevelUnlocked) {
				LevelManager.LoadLevel(selectedLevel);
			}
		}
	}
}
