using UnityEngine;
using System.Collections;

public class ActionGUI : MonoBehaviour {
	
	public bool useExclamationPoint = false;
	
	private Player player;
	
	
	private tk2dSlicedSprite actionButtonSprite;
	
	private tk2dTextMesh actionText;
	public Color actionTextFadeInColor =  new Color(1.0f, 1.0f, 1.0f, 0.85f);
	public Color actionTextFadeOutColor = new Color(1.0f, 1.0f, 1.0f, 0.30f);


	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		actionButtonSprite = this.transform.FindChild("Action Button").GetComponent<tk2dSlicedSprite>();
		actionText = this.transform.FindChild("Action Text").GetComponent<tk2dTextMesh>();
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (useExclamationPoint) {
			if (player.currentActionButtonState != Player.ActionButtonStates.NONE) {
				actionText.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				actionText.transform.localPosition = new Vector3(actionText.transform.localPosition.x, 23.0f, actionText.transform.localPosition.z);
				actionText.text = "!";
				iTween.ShakeRotation(actionText.gameObject, new Vector3(0f, 0f, Random.Range(-5f, 5f)), Time.deltaTime);
				iTween.ShakePosition(actionText.gameObject, new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0f), Time.deltaTime);
				return;
			}
		}
		
		/* Update content and look of action gui based on if player can action or not. */
		switch (player.currentActionButtonState) {

			case (Player.ActionButtonStates.CAN_WIN):
			
				actionText.text = "Obtain Machine Piece";
				actionText.color = actionTextFadeInColor;
				actionButtonSprite.SetSprite("gui_key_x_fade_in");
				break;

			case (Player.ActionButtonStates.CAN_PICKUP):
			
				actionText.text = "Pick Up";
				actionText.color = actionTextFadeInColor;
				actionButtonSprite.SetSprite("gui_key_x_fade_in");
				break;
			
			case (Player.ActionButtonStates.CAN_DROP):
			
				actionText.text = "Drop";
				actionText.color = actionTextFadeInColor;
				actionButtonSprite.SetSprite("gui_key_x_fade_in");
				break;
			
			case (Player.ActionButtonStates.CAN_ACTIVATE_VORTEX):
				if (LevelManager.IsPast()) {
					actionText.text = "Warp To The Future!";
				}
				else {
					actionText.text = "Warp To The Past!";
				}
				actionText.color = actionTextFadeInColor;
				actionButtonSprite.SetSprite("gui_key_x_fade_in");
				break;
			
			case (Player.ActionButtonStates.NONE):
			
				actionText.text = "";
				actionText.color = actionTextFadeOutColor;
				actionButtonSprite.SetSprite("gui_key_x_fade_out");
				break;
			
		}

		
		
		/* If hitting the action button, animate the actionButton so that it looks like it's being pressed down. */
		if (Input.GetButtonDown("Action1")) {
			iTween.ScaleFrom(actionButtonSprite.gameObject, 0.8f*actionButtonSprite.transform.localScale, 0.2f);
		}

	}
}
