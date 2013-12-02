using UnityEngine;
using System.Collections;

public class TutorialEvent001 : PlayerEvent {
	
	
	private bool alreadyActivated;
	public static bool leftTrigger;
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated) {
			return;	
		}

		base.OnActivate(player);
		
		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {
		
		yield return new WaitForSeconds(0.125f);
		
		player.ShowDialogueBox();
		player.SetDialogue("No time machine... ^gF80F0F0FNO BOOZE!^cFFFF\n\tShoulda invented a hangover cure. \n\nAnyways, I can move with those \n^cFF0F[arrow keys]^cFFFF.");
		
		
		alreadyActivated = true;	
		
		while (Input.GetAxis("Horizontal") == 0.0f) {
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
