using UnityEngine;
using System.Collections;

public class TutorialEvent004 : PlayerEvent {
	
	
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
//		player.SetDialogue("Wowza, check out the size of that ^c0F0Fplant^cFFFF!");
		player.SetDialogue("Crikey, that ^c0F0Fplant^cFFFF grew as big as a mouse... \n\n    Or a house...\n\n        Or whatever...");
		
		alreadyActivated = true;	
		
		yield return new WaitForSeconds(1.0f);
		
		while (!Input.GetButton("Jump")) {
			
			/* End dialogue if player goes into a vortex.  */
			if (Vortex.CurrentlyWarping()) {
				player.HideDialogueBox();
				yield break;
			}
			
			yield return null;	
		}
		
		yield return new WaitForSeconds(1.5f);
		player.HideDialogueBox();
		
	}

	
}
