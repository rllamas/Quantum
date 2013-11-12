using UnityEngine;
using System.Collections;

public class MysteriousHeightsEvent002 : PlayerEvent {
	
	
	public static bool alreadyActivated; // Looked at by other scripts.
	private bool leftTrigger;
	
	
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
		else {
			yield return new WaitForSeconds(0.75f);
		}
			
		player.ShowDialogueBox();
		player.SetDialogue("How'd you get here, lil' guy?");
		
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
