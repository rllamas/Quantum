using UnityEngine;
using System.Collections;

public class JumpGUI : MonoBehaviour {
	
	private Player player;
	
	
	private tk2dSlicedSprite jumpButtonSprite;
	
	
	private tk2dTextMesh jumpText;
	public Color jumpTextFadeInColor =  new Color(1.0f, 1.0f, 1.0f, 0.85f);
	public Color jumpTextFadeOutColor = new Color(1.0f, 1.0f, 1.0f, 0.30f);


	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		jumpButtonSprite = this.transform.FindChild("Jump Button").GetComponent<tk2dSlicedSprite>();	
		jumpText = this.transform.FindChild("Jump Text").GetComponent<tk2dTextMesh>();
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		/* Update content and look of jump gui based on if player can jump or not. */
		if (player.IsGrounded()) {
			jumpText.text = "Jump";
			jumpText.color = jumpTextFadeInColor;
			
			jumpButtonSprite.SetSprite("gui_key_z_fade_in");
		}
		else {
			jumpText.text = "---";	
			jumpText.color = jumpTextFadeOutColor;
			
			jumpButtonSprite.SetSprite("gui_key_z_fade_out");
		}
		
		
		/* If hitting the jump button, animate the jumpButton so that it looks like it's being pressed down. */
		if (Input.GetButtonDown("Jump")) {
			iTween.ScaleFrom(jumpButtonSprite.gameObject, 0.8f*jumpButtonSprite.transform.localScale, 0.2f);
		}

	}
}
