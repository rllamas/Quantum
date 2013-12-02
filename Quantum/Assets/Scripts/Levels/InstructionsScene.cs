using UnityEngine;
using System.Collections;

public class InstructionsScene : MonoBehaviour {
	
	private int currentPage = 0;
	tk2dSprite currentPageSprite;
	
	public float clickCooldown = 0.25f;
	private float clickCooldownTimeRemaining = 0.0f;
	
	
	// Use this for initialization
	void Start () {
		currentPageSprite = GetComponent<tk2dSprite>();
		currentPageSprite.SetSprite("gui_tutorial_screen_01");
	}
	
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKeyDown && clickCooldownTimeRemaining == 0.0f) {
			switch (currentPage) {
				case 0:
					++currentPage;
					currentPageSprite.SetSprite("gui_tutorial_screen_02");
					break;
				
				case 1:
					LevelManager.LoadLevel(0);	
					break;
			}
			clickCooldownTimeRemaining = clickCooldown;
		}
		else {
			clickCooldownTimeRemaining = Mathf.Max(0.0f, clickCooldownTimeRemaining - Time.deltaTime);	
		}
	}
	
}
