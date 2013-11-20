using UnityEngine;
using System;
using System.Collections;

public class Level01Event001: PlayerEvent {
	
	
	private bool alreadyActivated;
	private bool leftTrigger;
	public GameObject compassGUI;
	
	private float currentAnimationCooldown;
	private float animationCooldownMaxTime = 0.3f;
	private bool shouldAnimate = false;
	
	private Vector3 compassOriginalSize;
	
	void Start() {
		if (compassGUI == null) {
			throw new Exception("Compass GUI instance not set; it must be set through the Inspector!");	
		}
		compassOriginalSize = compassGUI.transform.localScale;
		compassGUI.transform.localScale = new Vector3(0.0f, 0.0f, compassGUI.transform.localScale.z);
	}
	
	
	
	public override void OnActivate(Player player) {
		
		if (alreadyActivated) {
			return;	
		}

		base.OnActivate(player);

		StartCoroutine( DoEvent() );
		
	}
	
	
	
	private IEnumerator DoEvent() {
	
		player.ShowDialogueBox();
//		player.SetDialogue("Lucky I've got my handy dandy ^cFF0FCOMPASS^cFFFF to point out those 'hard-to-see' ^cFF0Ftime machine pieces^cFFFF!");
		player.SetDialogue("Lucky I've got my ^cFF0FTIME COMPASS^cFFFF! \n\nLazy geezers like me NEED help finding 'hard-to-see' ^cFF0FTIME CRYSTALS^cFFFF.");
		
		compassGUI.gameObject.SetActive(true);
		iTween.ScaleTo(compassGUI.gameObject, new Vector3(1.3f*compassOriginalSize.x, 1.3f*compassOriginalSize.y, 1.0f), 1.5f);
		yield return new WaitForSeconds(1.5f);
		
		
		alreadyActivated = true;	
		
		
		shouldAnimate = true;
		while (!leftTrigger) {
			yield return null;	
		}
		shouldAnimate = false;
		
		
		iTween.ScaleTo(compassGUI.gameObject, compassOriginalSize, 2.0f);
		yield return new WaitForSeconds(0.5f);
		player.HideDialogueBox();
		
	}
	
	
	void Update() {
		
		/* Play the compass throbbing animation if you should. The animation happens here rather than in
		 * DoEvent() because the timing of resuming the coroutine doesn't play well with iTween. */
		if (shouldAnimate) {
			currentAnimationCooldown = Mathf.Max(0.0f, currentAnimationCooldown-Time.deltaTime);
		
			/* Play pulse animation if player is close enough to goal. */
			if (currentAnimationCooldown == 0.0f) {
				iTween.ScaleFrom(
					compassGUI.gameObject, 
					new Vector3(1.5f*compassOriginalSize.x, 1.5f*compassOriginalSize.y, compassOriginalSize.z), 
					animationCooldownMaxTime
				);
				currentAnimationCooldown = animationCooldownMaxTime;
			}	
		}
	}
	
	

	void OnTriggerExit() {
		leftTrigger = true;
	}
	
}
