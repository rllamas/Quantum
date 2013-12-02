using UnityEngine;
using System.Collections;

public class ActionGUI : MonoBehaviour {
	
	private Player player;
	private tk2dTextMesh actionText;
	private tk2dSprite actionButton;
	
	private float maxTimeUntilFade = 4.5f;
	private float timeLeftUntilFade;
	
	private string actionTextLastFrame;


	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		actionText = this.transform.FindChild("Action Text").GetComponent<tk2dTextMesh>();
		actionButton = this.transform.FindChild("Action Button").GetComponent<tk2dSprite>();
	
		timeLeftUntilFade = maxTimeUntilFade;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		actionTextLastFrame = actionText.text;
		
		if (Vortex.CurrentlyWarping()) {
			actionText.text = "";
			actionButton.gameObject.SetActive(false);
			return;
		}
		
		
		/* Update content and look of action gui based on if player can action or not. */
		switch (player.currentActionButtonState) {

			case (Player.ActionButtonStates.CAN_WIN):
				actionText.text = "Grab ^cFF0FMachine Piece^cFFFF";
				break;

			case (Player.ActionButtonStates.CAN_PICKUP_PLANT):
				actionText.text = "Pick Up ^c0F0FSapling^cFFFF";
				break;
			
			case (Player.ActionButtonStates.CAN_PICKUP_BOUNCER):
				actionText.text = "^Pick Up ^c0DFFB.O.U.N.C.E.R.^cFFFF";
				break;
			
			case (Player.ActionButtonStates.CAN_DROP):
				actionText.text = "Drop";
				break;
			
			case (Player.ActionButtonStates.CAN_ACTIVATE_VORTEX):
				if (LevelManager.IsPast()) {
					actionText.text = "To The ^cF08FFuture^cFFFF!";
				}
				else {
					actionText.text = "To The ^cF08FPast^cFFFF!";
				}
				break;
			
			case (Player.ActionButtonStates.WON):	
				actionButton.gameObject.SetActive(false);
				actionText.text = "";
				return;
			
			case (Player.ActionButtonStates.NONE):		
				actionText.text = "";
				actionButton.gameObject.SetActive(false);
				break;
		}
		
		
		if (actionText.text.Equals("")) {
			actionButton.SetSprite("gui_button_none");	
		}
		else {
			actionButton.SetSprite("gui_button_shift");		
		}
		
		
		/* Redisplay the text if it has recently changed. */
		if (!actionText.text.Equals(actionTextLastFrame)) {
			actionText.gameObject.SetActive(true);	
			actionButton.gameObject.SetActive(true);
		}
		
		
		/* Decrement the remaining time until you should hide the text if the text hasn't changed
		 * since last frame. */
		if (actionTextLastFrame.Equals(actionText.text)) {
			timeLeftUntilFade = Mathf.Max(0, timeLeftUntilFade-Time.deltaTime);
		}
		else {
			timeLeftUntilFade = maxTimeUntilFade;
		}
		
		/* Hide text if it has been the same for too long. */
		if (timeLeftUntilFade == 0f) {
			timeLeftUntilFade = maxTimeUntilFade;
			actionText.gameObject.SetActive(false);
			actionButton.gameObject.SetActive(false);
		}
			
	}
}
