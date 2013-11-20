using UnityEngine;
using System.Collections;

public class Level02Event002 : PlayerEvent {
	
	
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
		player.SetDialogue("'Sup lil' guy? How'd you get here?\n\n^c888F*No response.*^cFFFF\n...Wait, I'm talking to ^c0F0Fplants^cFFFF now!?");
		
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
