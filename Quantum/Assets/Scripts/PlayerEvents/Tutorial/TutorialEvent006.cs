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
		player.SetDialogue("I bet these plants grow over time. Hmm, if I moved this sapling and time traveled...");
		
		alreadyActivated = true;	
		
		yield return new WaitForSeconds(1.0f);
		
		while (!Vortex.CurrentlyWarping()) {
			yield return null;
		}
			
		player.HideDialogueBox();
		
	}
	
	
}
