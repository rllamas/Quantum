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
				actionText.text = "Obtain Machine Piece";
				break;

			case (Player.ActionButtonStates.CAN_PICKUP):
				actionText.text = "Pick Up";
				break;
			
			case (Player.ActionButtonStates.CAN_DROP):
				actionText.text = "Drop";
				break;
			
			case (Player.ActionButtonStates.CAN_ACTIVATE_VORTEX):
				if (LevelManager.IsPast()) {
					actionText.text = "Warp To The Future!";
				}
				else {
					actionText.text = "Warp To The Past!";
				}
				break;
			
			case (Player.ActionButtonStates.NONE):		
				actionText.text = "";
				break;
			
		}

		
	}
}
