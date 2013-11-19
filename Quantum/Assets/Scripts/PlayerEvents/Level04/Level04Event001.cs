using UnityEngine;
using System.Collections;

public class Level04Event001 : PlayerEvent {
	
	
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
			yield return new WaitForSeconds(0.15f);
		}
			
		player.ShowDialogueBox();
		player.SetDialogue("WHHHEEEEEE!!!!");
		
		alreadyActivated = true;	
		
		
		while (!leftTrigger) {
			yield return null;	
		}
		
		yield return new WaitForSeconds(0.25f);
		player.HideDialogueBox();
		
	}
	
	
	
	void OnTriggerExit() {
		leftTrigger = true;
	}
	
}
