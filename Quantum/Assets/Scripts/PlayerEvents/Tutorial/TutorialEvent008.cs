﻿using UnityEngine;
using System.Collections;

public class TutorialEvent008 : PlayerEvent {
	
	
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
		player.SetDialogue("Aha, a time machine piece!");
		
		alreadyActivated = true;	
		
		/* Wait for a little while to hide the dialog box, but if the player gets the goal, then
		 * hide the dialog box immediately. */
		float timeUntilHideDialogBox = 2.0f;
		while (timeUntilHideDialogBox != 0.0f) {
		
			if (player.HasWon()) {
				break;	
			}
			
			yield return null;
			timeUntilHideDialogBox = Mathf.Max (timeUntilHideDialogBox - Time.deltaTime, 0.0f);
			
		}
			
		player.HideDialogueBox();
		
	}
	
	
}