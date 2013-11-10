using UnityEngine;
using System.Collections;

public class TutorialEvent007: PlayerEvent {

	
	private bool alreadyActivated;
	private bool currentlyActivated;
	
	public override void OnActivate(Player player) {
		
		if (currentlyActivated || !player.CarryingPickup()) {
			return;	
		}
		
		base.OnActivate(player);

		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {
		
		currentlyActivated = true;
		
		if (!player.IsDialogBoxHidden()) {
			yield return new WaitForSeconds(1.0f);
		}
			
		player.ShowDialogueBox();
		
		Debug.Log ("Already activated? " + alreadyActivated);
		
		if (!alreadyActivated) {
			player.SetDialogue("Was it helpful to take this sapling to the future...?");	
		}
		else {
			player.SetDialogue("This again? Man, I'm getting old! At least I'm handsome.");
		}
		
		alreadyActivated = true;	
		
		Debug.Log ("Already activated? " + alreadyActivated);
		
		yield return new WaitForSeconds(1.0f);
		
		while (!Vortex.CurrentlyWarping()) {
			yield return null;
		}
		
		player.HideDialogueBox();
		
		yield return new WaitForSeconds(1.0f);
		currentlyActivated = false;
		
		
		
		
		
	}
	
	
}
