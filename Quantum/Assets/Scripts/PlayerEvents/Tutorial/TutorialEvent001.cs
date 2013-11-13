using UnityEngine;
using System.Collections;

public class TutorialEvent001 : PlayerEvent {
	
	
	private bool alreadyActivated;
	
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated) {
			return;	
		}

		base.OnActivate(player);
		
		player.canMove = false;
		
		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {
		
		yield return new WaitForSeconds(0.125f);
		
		player.ShowDialogueBox();
		player.SetDialogue("Time to get down and ^gF80F0F0FBOOGIE!^cFFFF \n\tIt seems I can move with [<-->].");
		
		
		alreadyActivated = true;	
		
		while (Input.GetAxis("Horizontal") == 0.0f) {
			yield return null;	
		}
		player.canMove = true;
		
		yield return new WaitForSeconds(1.0f);
		
		player.HideDialogueBox(0.5f);
		
	}

		
	
	
}
