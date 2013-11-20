using UnityEngine;
using System.Collections;

public class Level05Event001 : PlayerEvent {
	
	
	private bool alreadyActivated;
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
			yield return new WaitForSeconds(0.25f);
		}
			
		player.ShowDialogueBox();
		//player.SetDialogue("This must be one of those new-fangled ^c0DFFsolar jump pads^cFFFF! It's too dark to use here, tho.");
		player.SetDialogue("A ^c0DFFSOLAR POWERED JUMP PAD^cFFFF!  \nWhile the ^cF08Ffuture's^cFFFF bleak, foreboding darkness is easy on my hangover, it doesn't seem to activate this doohickey.");

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
