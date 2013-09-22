using UnityEngine;
using System.Collections;

/*
 *      Continuously scrolls the main texture on a surface.
 */ 


public class scrollBackground : MonoBehaviour {


    public Vector2 uvSpeed = new Vector2(0.0f, 1.0f);
	public Vector2 uvOffset = Vector2.zero;

	void Update () {
        uvOffset += (uvSpeed * Time.deltaTime);
        renderer.materials[0].SetTextureOffset("_MainTex", uvOffset);
	}
}
