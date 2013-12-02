using UnityEngine;
using System.Collections;

public class TutorialEvent008 : PlayerEvent {
	
	
	private bool alreadyActivated;
	
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated) {
			return;	
		}

		base.OnActivate(player);

		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {
		
		if (!player.IsDialogBoxHidden()) {
			yield return new WaitForSeconds(1.0f);
		}
			
		player.ShowDialogueBox();
		//player.SetDialogue("Aha, a ^cFF0Ftime machine piece^cFFFF!");
		player.SetDialogue("BOOM BABY! ONE OF MY ^cFF0FTIME MACHINE CRYSTALS^cFFFF!\n\nI'll be back in my time in no time... pun intended.");

		alreadyActivated = true;	
		
		/* Wait for a little while to hide the dialog box, but if the player gets the goal, then
		 * hide the dialog box immediately. */
		float timeUntilHideDialogBox = 6.5f;
		while (timeUntilHideDialogBox != 0.0f) {
			
			yield return null;
			timeUntilHideDialogBox = Mathf.Max (timeUntilHideDialogBox - Time.deltaTime, 0.0f);
			
		}
			
		player.HideDialogueBox();
		
	}
	
	
}
