using UnityEngine;
using System.Collections;

public class ActionGUI : MonoBehaviour {
	
	private Player player;
	private tk2dTextMesh actionText;
	
	private float maxTimeUntilFade = 4.5f;
	private float timeLeftUntilFade;
	
	private string actionTextLastFrame;


	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		actionText = this.transform.FindChild("Action Text").GetComponent<tk2dTextMesh>();
	
		timeLeftUntilFade = maxTimeUntilFade;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		actionTextLastFrame = actionText.text;
		
		if (Vortex.CurrentlyWarping()) {
			actionText.text = "";
			return;
		}
		
		
		/* Update content and look of action gui based on if player can action or not. */
		switch (player.currentActionButtonState) {

			case (Player.ActionButtonStates.CAN_WIN):
				actionText.text = "[SHIFT] Grab ^cFF0FMachine Piece^cFFFF";
				break;

			case (Player.ActionButtonStates.CAN_PICKUP_PLANT):
				actionText.text = "[SHIFT] Pick Up ^c0F0FSapling^cFFFF";
				break;
			
			case (Player.ActionButtonStates.CAN_PICKUP_BOUNCER):
				actionText.text = "^[SHIFT] Pick Up ^c0DFFB.O.U.N.C.E.R.^cFFFF";
				break;
			
			case (Player.ActionButtonStates.CAN_DROP):
				actionText.text = "[SHIFT] Drop";
				break;
			
			case (Player.ActionButtonStates.CAN_ACTIVATE_VORTEX):
				if (LevelManager.IsPast()) {
					actionText.text = "[SHIFT] To The ^cF08FFuture^cFFFF!";
				}
				else {
					actionText.text = "[SHIFT] To The ^cF08FPast^cFFFF!";
				}
				break;
			
			case (Player.ActionButtonStates.WON):		
				actionText.text = "";
				break;
			
			case (Player.ActionButtonStates.NONE):		
				actionText.text = "";
				break;
		}
		
		/* Redisplay the text if it has recently changed. */
		if (!actionText.text.Equals(actionTextLastFrame)) {
			actionText.gameObject.SetActive(true);	
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
		}
			
	}
}
