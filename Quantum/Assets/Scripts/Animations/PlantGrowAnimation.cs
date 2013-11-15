using UnityEngine;
using System.Collections;

public class PlantGrowAnimation : MonoBehaviour {
	
	tk2dSpriteAnimator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<tk2dSpriteAnimator>();
		animator.Play();
		//StartCoroutine( DoAnimation() );
	}
	
	
	IEnumerator DoAnimation() {
		//animator.Play();
		yield return new WaitForSeconds(animator.ClipTimeSeconds);
		//Destroy(this);
	}
}
