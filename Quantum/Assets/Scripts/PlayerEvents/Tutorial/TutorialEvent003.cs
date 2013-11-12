using UnityEngine;
using System.Collections;

public class TutorialEvent003 : PlayerEvent {
	
	
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
		player.SetDialogue("I can use that vortex to [SHIFT] into the future!");
		
		alreadyActivated = true;	
		
		while (Input.GetAxis("Horizontal") == 0.0f) {
			yield return null;	
		}
		
		/* Wait for a little while to hide the dialog box, but if the player enters a portal, then
		 * hide the dialog box immediately. */
		float timeUntilHideDialogBox = 4.0f;
		while (timeUntilHideDialogBox != 0.0f) {
		
			if (Vortex.CurrentlyWarping()) {
				break;	
			}
			
			yield return null;
			timeUntilHideDialogBox = Mathf.Max (timeUntilHideDialogBox - Time.deltaTime, 0.0f);
			
		}
		
		player.HideDialogueBox();
		
	}

	
}
