using UnityEngine;
using System.Collections;

public class OneShotAnimation : MonoBehaviour {
	
	tk2dSpriteAnimator animator;
	public float timeUntilDestroyed = 2.0f;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<tk2dSpriteAnimator>();
		StartCoroutine( DoAnimation() );
	}
	
	
	IEnumerator DoAnimation() {
		animator.Play();
		yield return new WaitForSeconds(timeUntilDestroyed);
		Destroy(this.gameObject);
	}
}
