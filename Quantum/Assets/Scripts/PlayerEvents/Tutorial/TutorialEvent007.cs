using UnityEngine;
using System.Collections;

public class TutorialEvent007: PlayerEvent {

	
	private bool alreadyActivated;
	private bool currentlyActivated;
	
	public override void OnActivate(Player player) {
		
		base.OnActivate(player);
		
	}
	
	
	
	void OnTriggerEnter() {
		
		if (!player.CarryingPickup()) {
			return;	
		}
		
		StartCoroutine( DoEvent() );	
	}
	
	
	
	private IEnumerator DoEvent() {

		
		if (!player.IsDialogBoxHidden()) {
			yield return new WaitForSeconds(1.0f);
		}
			
		player.ShowDialogueBox();
		
		
		if (!alreadyActivated) {
			player.SetDialogue("Was it helpful to take this sapling to the future...?");	
		}
		else {
			player.SetDialogue("This again? Man, I'm getting old! At least I'm handsome.");
		}
		
		alreadyActivated = true;	
		
		
		yield return new WaitForSeconds(1.0f);
		
		while (!Vortex.CurrentlyWarping()) {
			yield return null;
		}
		
		player.HideDialogueBox();		
		
	}
	
	
}
