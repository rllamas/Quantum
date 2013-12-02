using UnityEngine;
using System.Collections;

public class TutorialEvent002 : PlayerEvent {
	
	
	private bool alreadyActivated;
	private bool leftTrigger;
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated || !TutorialEvent001.leftTrigger) {

			//Debug.Log("alreadyActivated? " + alreadyActivated);
			//Debug.Log("TutorialEvent001.leftTrigger? " + TutorialEvent001.leftTrigger);
			return;	
		}

		//Debug.Log("Starting Event 2 Couroutine.");

		base.OnActivate(player);
		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {
		
		if (!player.IsDialogBoxHidden()) {
			yield return new WaitForSeconds(0.5f);
		}
			
		player.ShowDialogueBox();
		player.SetDialogue("Ouch, my brain is fried... Mmm, chicken fried...\n\nAnyways, is jumping ^cFF0F[spacebar]^cFFFF again...?");

		alreadyActivated = true;	
		
		while (!Input.GetButtonDown("Jump")) {
			yield return null;	
		}
		
		float timeUntilHideDialogBox = 4.0f;
		while (timeUntilHideDialogBox != 0.0f) {
			
			if (leftTrigger) {
				yield break;	
			}
			
			yield return null;
			timeUntilHideDialogBox = Mathf.Max (timeUntilHideDialogBox - Time.deltaTime, 0.0f);
			
		}

		player.HideDialogueBox(0.5f);
		
	}


	void OnTriggerExit() {
		leftTrigger = true;
	}

	
}
