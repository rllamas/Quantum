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
			//player.SetDialogue("Was it helpful to take this ^c0F0Fsapling^cFFFF to the ^cF08Ffuture^cFFFF...?");	
			player.SetDialogue("Bringing this ^c0F0Fsapling^cFFFF to the ^cF08Ffuture^cFFFF didn't seem too helpful, but... \n\n^cF80FYOU ARE NOW THE FIRST TIME TRAVELING PLANT!!!^cFFFF");
		}
		else {
			player.SetDialogue("This again? I’m getting too old for this sh...tuff. \n\nPuzzles and drunk old men don't mix.");
		}
		
		alreadyActivated = true;	
		
		
		yield return new WaitForSeconds(1.0f);
		
		while (!Vortex.CurrentlyWarping()) {
			yield return null;
		}
		
		player.HideDialogueBox();		
		
	}
	
	
}
