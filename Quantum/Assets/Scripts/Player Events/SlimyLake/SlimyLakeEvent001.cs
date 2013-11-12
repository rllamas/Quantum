using UnityEngine;
using System.Collections;

public class SlimyLakeEvent001 : PlayerEvent {
	
	
	private bool alreadyActivated;
	private bool leftTrigger;
	private GameObject compassGUI;
	
	private float animationCooldown;
	private float animationMaxCooldown = 3.0f;
	private bool shouldAnimate = false;
	
	void Start() {
		compassGUI = GameObject.Find("Compass GUI");	
	}
	
	
	
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
			yield return new WaitForSeconds(1f);
		}
			
		player.ShowDialogueBox();
		player.SetDialogue("Lucky I've got my handy dandy 'Wayfinder' to point out those hard-to-see time machine pieces!");
		
		alreadyActivated = true;	
		
		shouldAnimate = true;
		StartCoroutine ( AnimateCompass() );
		while (!leftTrigger) {
			yield return null;	
		}
		shouldAnimate = false;
		
		yield return new WaitForSeconds(0.5f);
		player.HideDialogueBox();
		
	}
	
	
	
	/* 
	 *  This is HORRIBLEEEEEEEEEE code to give the compass a throbbing effect, and the effect doesn't even look good. 
	 *  Please don't read it. =P
	 * 
	 * */
	private IEnumerator AnimateCompass() {
		Vector3 originalScale = compassGUI.transform.localScale;

		while (shouldAnimate) {
			if (animationCooldown == 0.0f) {
				animationCooldown = animationMaxCooldown;
				compassGUI.transform.localScale = new Vector3(1.1f*originalScale.x, 1.1f*originalScale.y, 1.0f*originalScale.z);
			}
			
			iTween.ScaleUpdate(compassGUI.gameObject, 
				Vector3.Max (compassGUI.transform.localScale-new Vector3(1f, 1f, 0f)*Time.deltaTime/20f,
					new Vector3(0.25f, 0.25f, 0.25f)
				),
				Time.deltaTime
			);
			animationCooldown = Mathf.Max(0.0f, animationCooldown - Time.deltaTime);

			yield return null;
		}
		
		iTween.ScaleTo(compassGUI.gameObject, originalScale, 1.0f);
	}
	
	
	
	void OnTriggerExit() {
		leftTrigger = true;
	}
	
}
