using UnityEngine;
using System.Collections;

public class PlatformerCharacterController_mk2 : MonoBehaviour {

    
    public Texture2D _facingLeft;
    public int _facingLeftFirstFrame;
    public int _facingLeftLastFrame;

    public Texture2D _facingRight;
    public int _facingRightFirstFrame;
    public int _facingRightLastFrame;

    public float _facingFPS = 30f;


    public Texture2D _turningLeftToRight;
    public int _turningLeftToRightFirstFrame;
    public int _turningLeftToRightLastFrame;

    public Texture2D _turningRightToLeft;
    public int _turningRightToLeftFirstFrame;
    public int _turningRightToLeftLastFrame;

    public float _turningFPS = 30f;


    public Texture2D _walkingLeft;
    public int _walkingLeftFirstFrame;
    public int _walkingLeftLastFrame;

    public Texture2D _walkingRight;
    public int _walkingRightFirstFrame;
    public int _walkingRightLastFrame;

    public float _walkingFPS = 30f;
    public float _walkingMovementSpeed = 1.0f;


    public Texture2D _jumpingLeft;
    public int _jumpingLeftFirstFrame;
    public int _jumpingLeftLastFrame;

    public Texture2D _jumpingRight;
    public int _jumpingRightFirstFrame;
    public int _jumpingRightLastFrame;

    public float _jumpingFPS = 30f;


    private float xAxis;
    private float yAxis;

    private Vector2 currentDirection = new Vector2 (1, 0);

    // Used for Animate coroutine.
    public Texture2D prevAnimation;
    public Texture2D currentAnimation;
    public bool currentlyRendering = false;
    public int currentFrame = -1;
	
    


	void Start () {
        prevAnimation = _facingLeft;
        currentAnimation = _facingLeft;
	}
	

	

	void Update () {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        prevAnimation = currentAnimation;		
		
        setAnimation(xAxis, yAxis);
        moveCharacter(xAxis, yAxis);
        

    }

        
    /* Moves the character based on the animation state. */
    private void moveCharacter(float xAxis, float yAxis) {

        if (currentAnimation == _walkingLeft) {
            this.transform.Translate( new Vector2(_walkingMovementSpeed * Time.deltaTime, 0) );
        }

        else if (currentAnimation == _walkingRight) {
            this.transform.Translate(new Vector2(_walkingMovementSpeed * Time.deltaTime * -1, 0));
        }
    }




    /*
     *  Determines and sets the correct character animation. Also sets currentAnimation.
     */
    private void setAnimation(float xAxis, float yAxis) {

        // If animator is still rendering the last frame, then skip determining any animation this update.
        if (currentlyRendering) {
            return;
        }


        // If character is not moving...
        if (xAxis == 0) {
            // If facing left...
            if (currentDirection.x < 0) {
                currentAnimation = _facingLeft;

                
                StartCoroutine(Animate(_facingLeft, _facingLeftFirstFrame, _facingLeftLastFrame, _facingFPS));
            }

            // If facing right...
            else if (currentDirection.x > 0) {
                currentAnimation = _facingRight;

                
                StartCoroutine( Animate(_facingRight, _facingRightFirstFrame, _facingRightLastFrame, _facingFPS) );
            }
        }
            
        // If pressing left...
        else if (xAxis < 0) {

            // ...but facing right, then turn left.
            if (_turningRightToLeft != null && currentDirection.x > 0) {
                currentAnimation = _turningRightToLeft;
                currentDirection = new Vector2(-1, 0);

                //for (int i = _turningRightToLeftFirstFrame; i <= _turningRightToLeftLastFrame; i++) {
                    StartCoroutine(Animate(_turningRightToLeft, _turningRightToLeftFirstFrame, _turningRightToLeftLastFrame, _turningFPS));
                //}

            }

            // Otherwise, walk left.
            else {
                currentDirection = new Vector2(-1, 0);
                currentAnimation = _walkingLeft;

                // Begins animation.
                StartCoroutine( Animate(_walkingLeft, _walkingLeftFirstFrame, _walkingLeftLastFrame, _walkingFPS) );
            }
        }

        // If pressing right...
        else if (xAxis > 0) {

            // ...but facing left, then turn right.
            if (_turningLeftToRight != null && currentDirection.x < 0) {
                currentAnimation = _turningLeftToRight;
                currentDirection = new Vector2(1, 0);

                //for (int i = _turningLeftToRightFirstFrame; i <= _turningLeftToRightLastFrame; i++) {
                    StartCoroutine(Animate(_turningLeftToRight, _turningLeftToRightFirstFrame, _turningLeftToRightLastFrame, _turningFPS));
                //}

            }

            // Otherwise, walk right.
            else { 
                currentDirection = new Vector2(1, 0);
                currentAnimation = _walkingRight;

                // Begins animation.
                StartCoroutine( Animate(_walkingRight, _walkingRightFirstFrame, _walkingRightLastFrame, _walkingFPS) );
            }
        }
    
    } // End method setAnimation();



    
    /* 
     *  This is implemented to be used as a coroutine.  Will animate the spritesheet once at the
     *  specified framerate.  firstFrame/lastFrame arguments give freedom to animate only part of the sheet.
     */ 
    private IEnumerator Animate (Texture2D spritesheet, int firstFrame, int lastFrame, float framerate) {

	        currentlyRendering = true;
	
	        // If starting new animation or repeating last animation...
	        if (currentFrame == -1 || prevAnimation != currentAnimation) {
	           
	            currentFrame = firstFrame;
	
	            renderer.material.mainTexture = spritesheet; // Set the spritesheet as the primary texture of the surface.
	            renderer.material.mainTextureScale = new Vector2( ( (float) spritesheet.height) / spritesheet.width, 1.0f); // Scale the spritesheet properly to the surface.
	            renderer.material.shader = Shader.Find("Unlit/Transparent"); // Apply shader that ignores light and retains alpha layer to spritesheet.
	            
	            renderer.material.mainTextureOffset =
	                new Vector2((float)(currentFrame * spritesheet.height) / spritesheet.width, 0f); // Move to appropriate frame on the spritesheet.

                yield return new WaitForSeconds(1.0f / framerate); // Wait until frame should be drawn.
	            currentFrame++;
	            
	        }
	
	        // If finishing with coroutine...
	        else if (currentFrame >= lastFrame) {
	
	            renderer.material.mainTextureOffset =
	                new Vector2((float)(currentFrame * spritesheet.height) / spritesheet.width, 0f); // Move to appropriate frame on the spritesheet.

                yield return new WaitForSeconds(1.0f / framerate); // Wait until frame should be drawn.    
	            currentFrame = -1; // Set currentFrame to -1 if done.         
	        }
	        
            // Else if continuing to animate the same spritesheet as the last coroutine call.
	        else {
	            
	            renderer.material.mainTextureOffset =
	                new Vector2((float)(currentFrame * spritesheet.height) / spritesheet.width, 0f); // Move to appropriate frame on the spritesheet.

                yield return new WaitForSeconds(1.0f / framerate); // Wait until frame should be drawn. 
	            currentFrame++;            
	        }
	
	        currentlyRendering = false;		

    } // End IEnumerator Animate().






}// end class PlatformerCharacterController_.....


