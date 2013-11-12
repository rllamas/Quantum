using UnityEngine;
using System.Collections;

public class ActionGUI : MonoBehaviour {
	
	private Player player;
	private tk2dTextMesh actionText;
	

	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		actionText = this.transform.FindChild("Action Text").GetComponent<tk2dTextMesh>();
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		
		/* Update content and look of action gui based on if player can action or not. */
		switch (player.currentActionButtonState) {

			case (Player.ActionButtonStates.CAN_WIN):
				actionText.text = "[SHIFT] Grab Machine Piece";
				break;

			case (Player.ActionButtonStates.CAN_PICKUP_PLANT):
				actionText.text = "[SHIFT] Pick Up Sapling";
				break;
			
			case (Player.ActionButtonStates.CAN_PICKUP_BOUNCER):
				actionText.text = "[SHIFT] Pick Up B.O.U.N.C.E.R.";
				break;
			
			case (Player.ActionButtonStates.CAN_DROP):
				actionText.text = "[SHIFT] Drop";
				break;
			
			case (Player.ActionButtonStates.CAN_ACTIVATE_VORTEX):
				if (LevelManager.IsPast()) {
					actionText.text = "[SHIFT] To The Future!";
				}
				else {
					actionText.text = "[SHIFT] To The Past!";
				}
				break;
			
			case (Player.ActionButtonStates.WON):		
				actionText.text = "";
				break;
			
			case (Player.ActionButtonStates.NONE):		
				actionText.text = "";
				break;
			
		}

		
	}
}
