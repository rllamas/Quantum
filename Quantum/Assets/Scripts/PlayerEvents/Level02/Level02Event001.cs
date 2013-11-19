using UnityEngine;
using System.Collections;

public class Level02Event001 : PlayerEvent {
	
	
	private bool alreadyActivated;
	private bool leftTrigger;
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated || Level02Event002.alreadyActivated) {
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
		player.SetDialogue("There's no ^c0F0Fplant^cFFFF here, hmm...");
		
		alreadyActivated = true;	
		
		yield return new WaitForSeconds(1.0f);
		
		while (!leftTrigger) {
			yield return null;	
		}
		
		yield return new WaitForSeconds(0.5f);
		player.HideDialogueBox();
		
	}
	
	
	
	void OnTriggerExit() {
		leftTrigger = true;
	}
	
}
