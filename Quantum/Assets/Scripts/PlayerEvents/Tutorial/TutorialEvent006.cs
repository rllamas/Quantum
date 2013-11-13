using UnityEngine;
using System.Collections;

public class TutorialEvent006 : PlayerEvent {
	
	
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
		player.SetDialogue("I bet these ^c0F0Fplants^cFFFF ^cF08Fgrow over time^cFFFF. Hmm, if I moved this ^c0F0Fsapling^cFFFF then ^cF08Ftime traveled^cFFFF...");
		
		alreadyActivated = true;	
		
		yield return new WaitForSeconds(1.0f);
		
		while (!Vortex.CurrentlyWarping()) {
			yield return null;
		}
			
		player.HideDialogueBox();
		
	}
	
	
}
