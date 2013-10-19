using UnityEngine;
using System.Collections;

public class ActionGUI : MonoBehaviour {
	
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
		
		/* Update content and look of action gui based on if player can action or not. */
		switch (player.currentActionButtonState) {
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
			
				actionText.text = "Activate Time Vortex";
				actionText.color = actionTextFadeInColor;
				actionButtonSprite.SetSprite("gui_key_x_fade_in");
				break;
			
			case (Player.ActionButtonStates.NONE):
			
				actionText.text = "---";
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
