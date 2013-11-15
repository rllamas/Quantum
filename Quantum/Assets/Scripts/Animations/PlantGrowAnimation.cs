using UnityEngine;
using System.Collections;

public class PlantGrowAnimation : MonoBehaviour {
	
	tk2dSpriteAnimator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<tk2dSpriteAnimator>();
		StartCoroutine( DoAnimation() );
	}
	
	
	IEnumerator DoAnimation() {
		animator.Play();
		yield return new WaitForSeconds(2.0f);
		Destroy(this.gameObject);
	}
}
