using UnityEngine;
using System.Collections;

public class TutorialEvent002 : PlayerEvent {
	
	
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
			yield return new WaitForSeconds(0.5f);
		}
			
		player.ShowDialogueBox();
		player.SetDialogue("Is jumping [SPACE] again...?");
		
		
		alreadyActivated = true;	
		
		while (!Input.GetButton("Jump")) {
			yield return null;	
		}
		
		yield return new WaitForSeconds(2.0f);
		
		player.HideDialogueBox();
		
	}

	
}
