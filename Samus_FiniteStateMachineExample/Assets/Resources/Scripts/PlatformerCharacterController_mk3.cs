/*
 *  Platformer Character Controller
 *  Written By Russell Jahn
 *  
 *  Mark 2: Rewriting Animation System, 
 *          Smooth switching between standing and walking
 *          
 *  Mark 3: Adding Jump Capabilities
 * 
 * 
 */


using UnityEngine;
using System.Collections;

public class PlatformerCharacterController_mk3 : MonoBehaviour {

    
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


    public Texture2D _jumpingUpFacingLeftBracing;
    public int _jumpingUpFacingLeftBracingFirstFrame;
    public int _jumpingUpFacingLeftBracingLastFrame;

    public Texture2D _jumpingUpFacingRightBracing;
    public int _jumpingUpFacingRightBracingFirstFrame;
    public int _jumpingUpFacingRightBracingLastFrame;

    public float _jumpingUpBracingFPS = 30f;

    public Texture2D _jumpingUpFacingLeftLifting;
    public int _jumpingUpFacingLeftLiftingFirstFrame;
    public int _jumpingUpFacingLeftLiftingLastFrame;

    public Texture2D _jumpingUpFacingRightLifting;
    public int _jumpingUpFacingRightLiftingFirstFrame;
    public int _jumpingUpFacingRightLiftingLastFrame;

    public float _jumpingUpLiftingFPS = 30f;

    public Texture2D _jumpingUpFacingLeftApex;
    public int _jumpingUpFacingLeftApexFirstFrame;
    public int _jumpingUpFacingLeftApexLastFrame;

    public Texture2D _jumpingUpFacingRightApex;
    public int _jumpingUpFacingRightApexFirstFrame;
    public int _jumpingUpFacingRightApexLastFrame;

    public float _jumpingUpApexFPS = 30f;

    public Texture2D _jumpingUpFacingLeftFalling;
    public int _jumpingUpFacingLeftFallingFirstFrame;
    public int _jumpingUpFacingLeftFallingLastFrame;

    public Texture2D _jumpingUpFacingRightFalling;
    public int _jumpingUpFacingRightFallingFirstFrame;
    public int _jumpingUpFacingRightFallingLastFrame;

    public float _jumpingUpFallingFPS = 30f;

    public Texture2D _jumpingUpFacingLeftRecoiling;
    public int _jumpingUpFacingLeftRecoilingFirstFrame;
    public int _jumpingUpFacingLeftRecoilingLastFrame;

    public Texture2D _jumpingUpFacingRightRecoiling;
    public int _jumpingUpFacingRightRecoilingFirstFrame;
    public int _jumpingUpFacingRightRecoilingLastFrame;

    public float _jumpingUpRecoilingFPS = 30f;

    public float jumpHeight = 5000.0f;

    private int currentJumpAnimationFrame = 0;
    private bool currentlyJumping_Bracing = false;
    private bool currentlyJumping_Lifting = false;
    private bool currentlyJumping_Apex = false;
    private bool currentlyJumping_Falling = false;
    private bool currentlyJumping_Recoiling = false;


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

      

    /*
     *   When the character's collider just hits another collider.
     *   Assumes character has a collider.
     */
    private void OnCollisionEnter() {

            // If currently in recoiling stage...
            if (currentlyJumping_Recoiling) {
                StartCoroutine(Animate(_jumpingUpFacingLeftRecoiling, _jumpingUpFacingLeftRecoilingFirstFrame,
                    _jumpingUpFacingLeftRecoilingLastFrame, _jumpingUpRecoilingFPS));


                currentlyJumping_Recoiling = false;
                currentJumpAnimationFrame = 0;

            }
        
    }



    /* Moves the character based on the animation state. */
    private void moveCharacter(float xAxis, float yAxis) {

        //print("prevAnimation: " + prevAnimation);
        //print("currentAnimation: " + currentAnimation);

        if (currentAnimation == _walkingLeft) {
            this.transform.Translate( new Vector2(_walkingMovementSpeed * Time.deltaTime, 0) );
        }

        else if (currentAnimation == _walkingRight) {
            this.transform.Translate(new Vector2(_walkingMovementSpeed * Time.deltaTime * -1, 0));
        }

        else if (currentAnimation == _jumpingUpFacingLeftBracing && prevAnimation != _jumpingUpFacingLeftBracing)
        {
            // Pushes character straight up in the air.  Assumes character has a rigidbody
            // component.
            this.rigidbody.AddForce( new Vector2(0f, jumpHeight) );
        }
    }




    /*
     *  Determines and sets the correct character animation. Also sets currentAnimation.
     */
    private void setAnimation(float xAxis, float yAxis) {
        bool handledAnimation = false; // Whether the frame animation has been handled yet.

        // If animator is still rendering the last frame, then skip determining any animation this update.
        if (currentlyRendering) {
            return;
        }

        // Checks the user input and handles the appropriate animation case.  Only one
        // case may be handled.  Handling order is important, for example:  If the player is
        // trying to turn, that can also be interpreted as trying to walk. Turning has extra 
        // behavior, however. Thus, checking for turns must happen before checking for walks.

        if (!handledAnimation) {
            handledAnimation = handleJumping(xAxis, yAxis);
        }

        if (!handledAnimation) {
            handledAnimation = handleStanding();
        }

        if (!handledAnimation) {
            handledAnimation = handleTurning();
        }

        if (!handledAnimation) {
            handledAnimation = handleWalking();
        }

    } // End method setAnimation();




    /*
     *  Checks input for jumping and triggers animations appropriately.  Returns a boolean indicating
     *  whether any jump handling occurred.
     */

    private bool handleJumping(float xAxis, float yAxis)
    {
        bool handledJumping = false;
        //print("xAxis: " + xAxis);


        // If currently at the height of a jump (Apex stage)...
        if (currentlyJumping_Lifting && this.rigidbody.velocity.y == 0) {

            currentlyJumping_Lifting = false;
            currentlyJumping_Apex = true;

            StartCoroutine(Animate(_jumpingUpFacingLeftApex, _jumpingUpFacingLeftApexFirstFrame,
                _jumpingUpFacingLeftApexLastFrame, _jumpingUpApexFPS));
            // If done with Apex stage, set to next stage...
            if (currentJumpAnimationFrame >= _jumpingUpFacingLeftApexLastFrame)
            {
                currentlyJumping_Apex = false;
                currentlyJumping_Falling = true;
                currentJumpAnimationFrame = _jumpingUpFacingLeftFallingLastFrame;
            }
            // Else, update to continue Apex stage...
            else
            {
                currentJumpAnimationFrame++;
            }
        }


        // If character is not moving...
        else if (xAxis == 0)
        {

            // If pressing jump button... 
            if (Input.GetButton("Jump")) {
                handledJumping = true;

                // bool indicating whether any jump flag is set.
                bool currentlyJumping = currentlyJumping_Bracing || currentlyJumping_Lifting ||
                                        currentlyJumping_Apex || currentlyJumping_Falling ||
                                        currentlyJumping_Recoiling;

                //print("currentlyJumping? " + currentlyJumping);
                //print("this.Rigidbody.velocity: " + this.rigidbody.velocity);
                //if (this.rigidbody.velocity.y == 0 && currentlyJumping) {
                //    print("Y Velocity is zero");
                //}

                // If facing left... 
                if (currentDirection.x < 0) {

                    // If the button is being pressed for the first time, set up into bracing stage...
                    if (!currentlyJumping) {
                        currentlyJumping_Bracing = true;
                        currentJumpAnimationFrame = _jumpingUpFacingLeftBracingFirstFrame;
                    }

                    // If currently in bracing stage...
                    else if (currentlyJumping_Bracing) {
                         
                        StartCoroutine( Animate( _jumpingUpFacingLeftBracing, _jumpingUpFacingLeftBracingFirstFrame,
                                                 _jumpingUpFacingLeftBracingLastFrame, _jumpingUpBracingFPS));
                        // If done with bracing stage, set to next stage...
                        if (currentJumpAnimationFrame >= _jumpingUpFacingLeftBracingLastFrame) {
                            currentlyJumping_Bracing = false;
                            currentlyJumping_Lifting = true;
                            currentJumpAnimationFrame = _jumpingUpFacingLeftLiftingLastFrame;
                        }
                        // Else, update to continue bracing stage...
                        else {
                            currentJumpAnimationFrame++;
                        }
                    }

                    // If currently in lifting stage...
                    else if (currentlyJumping_Lifting) {

                        StartCoroutine(Animate(_jumpingUpFacingLeftLifting, _jumpingUpFacingLeftLiftingFirstFrame,
                         _jumpingUpFacingLeftLiftingLastFrame, _jumpingUpLiftingFPS));
                        // If done with Lifting stage, set to next stage...
                        if (currentJumpAnimationFrame >= _jumpingUpFacingLeftLiftingLastFrame)
                        {
                            //currentlyJumping_Lifting = false;
                            //currentlyJumping_Apex = true;
                            currentJumpAnimationFrame = _jumpingUpFacingLeftLiftingFirstFrame;
                        }
                        // Else, update to continue Lifting stage...
                        else
                        {
                            currentJumpAnimationFrame++;
                        }
                    }

                    // If currently in Apex stage...
                    else if (currentlyJumping_Apex)
                    {

                        StartCoroutine(Animate(_jumpingUpFacingLeftApex, _jumpingUpFacingLeftApexFirstFrame,
                                                 _jumpingUpFacingLeftApexLastFrame, _jumpingUpApexFPS));
                        // If done with Apex stage, set to next stage...
                        if (currentJumpAnimationFrame >= _jumpingUpFacingLeftApexLastFrame)
                        {
                            currentlyJumping_Apex = false;
                            currentlyJumping_Falling = true;
                            currentJumpAnimationFrame = _jumpingUpFacingLeftFallingLastFrame;
                        }
                        // Else, update to continue Apex stage...
                        else
                        {
                            currentJumpAnimationFrame++;
                        }
                    }

                    // If currently in Falling stage...
                    else if (currentlyJumping_Falling)
                    {

                        StartCoroutine(Animate(_jumpingUpFacingLeftFalling, _jumpingUpFacingLeftFallingFirstFrame,
                         _jumpingUpFacingLeftFallingLastFrame, _jumpingUpFallingFPS));
                        // If done with Falling stage, set to next stage...
                        if (currentJumpAnimationFrame >= _jumpingUpFacingLeftFallingLastFrame)
                        {
                            currentlyJumping_Falling = false;
                            currentlyJumping_Recoiling = true;
                            currentJumpAnimationFrame = _jumpingUpFacingLeftRecoilingLastFrame;
                        }
                        // Else, update to continue Falling stage...
                        else
                        {
                            currentJumpAnimationFrame++;
                        }
                    }
                    
                    // Otherwise, resound error.  Parsing was messed up or some other bug.
                    else {
                        print("Warning: Unknown JumpingUp state!");
                    }
                }

                // If facing right...
                else if (currentDirection.x > 0) {
                }

            } // End (Input.GetButton("Jump")) branch.

            // If releasing jump button...
            else if (Input.GetButtonUp("Jump")) {
                handledJumping = true;

                if (currentlyJumping_Lifting) {

                    currentlyJumping_Falling = true;
                    currentlyJumping_Apex = true;
   
                    StartCoroutine(Animate(_jumpingUpFacingLeftApex, _jumpingUpFacingLeftApexFirstFrame,
                     _jumpingUpFacingLeftApexLastFrame, _jumpingUpApexFPS));
                    // If done with Apex stage, set to next stage...
                    if (currentJumpAnimationFrame >= _jumpingUpFacingLeftApexLastFrame)
                    {
                        currentlyJumping_Apex = false;
                        currentlyJumping_Falling = true;
                        currentJumpAnimationFrame = _jumpingUpFacingLeftFallingLastFrame;
                    }
                    // Else, update to continue Apex stage...
                    else
                    {
                        currentJumpAnimationFrame++;
                    }
                }
            }


        }
        return handledJumping;
    } // End method handleJumping();



/*
 *  Checks input for standing and triggers animations appropriately.  Returns a boolean indicating
 *  whether any standing handling occurred.
 */
    private bool handleStanding() {
        bool handledStanding = false;

        // If standing...
        if (xAxis == 0) {
            handledStanding = true;

            // If facing left...
            if (currentDirection.x < 0) {

                currentAnimation = _facingLeft;
                StartCoroutine(Animate(_facingLeft, _facingLeftFirstFrame, _facingLeftLastFrame, _facingFPS));
            }

            // If facing right...
            else if (currentDirection.x > 0) {

                currentAnimation = _facingRight;
                StartCoroutine(Animate(_facingRight, _facingRightFirstFrame, _facingRightLastFrame, _facingFPS));
            }
        }
        return handledStanding;
    } // End method handleStanding();



    /*
     *  Checks input for turning and triggers animations appropriately.  Returns a boolean indicating
     *  whether any turning handling occurred.
     */
    public bool handleTurning() {

        bool handledTurning = false;
        // If pressing left...
        if (xAxis < 0) {

            // ...but facing right, then turn left.
            if (_turningRightToLeft != null && currentDirection.x > 0) {
                
                handledTurning = true;
                currentAnimation = _turningRightToLeft;
                currentDirection = new Vector2(-1, 0);

                //for (int i = _turningRightToLeftFirstFrame; i <= _turningRightToLeftLastFrame; i++) {
                StartCoroutine(Animate(_turningRightToLeft, _turningRightToLeftFirstFrame, _turningRightToLeftLastFrame, _turningFPS));
                //}
            }
        }

        // If pressing right...
        else if (xAxis > 0) {

            // ...but facing left, then turn right.
            if (_turningLeftToRight != null && currentDirection.x < 0) {
                
                handledTurning = true;
                currentAnimation = _turningLeftToRight;
                currentDirection = new Vector2(1, 0);

                //for (int i = _turningLeftToRightFirstFrame; i <= _turningLeftToRightLastFrame; i++) {
                StartCoroutine(Animate(_turningLeftToRight, _turningLeftToRightFirstFrame, _turningLeftToRightLastFrame, _turningFPS));
                //}
            }
        }
        return handledTurning;
    } // End method handleTurning();




/*
 *  Checks input for walking and triggers animations appropriately.  Returns a boolean indicating
 *  whether any walking handling occurred.
 */
    public bool handleWalking() {

        bool handledWalking = false;
        // If pressing left...
        if (xAxis < 0) {

            handledWalking = true;
            currentDirection = new Vector2(-1, 0);
            currentAnimation = _walkingLeft;

            // Begins animation.
            StartCoroutine(Animate(_walkingLeft, _walkingLeftFirstFrame, _walkingLeftLastFrame, _walkingFPS));        
        }

        // If pressing right...
        else if (xAxis > 0) {

            handledWalking = true;
            currentDirection = new Vector2(1, 0);
            currentAnimation = _walkingRight;

            // Begins animation.
            StartCoroutine(Animate(_walkingRight, _walkingRightFirstFrame, _walkingRightLastFrame, _walkingFPS));
        }
        return handledWalking;
    } // End method handleWalking();



    
    /* 
     *  This is implemented to be used as a coroutine.  Will animate the spritesheet once at the
     *  specified framerate.  firstFrame/lastFrame arguments give freedom to animate only part of the sheet.
     */ 
    private IEnumerator Animate (Texture2D spritesheet, int firstFrame, int lastFrame, float framerate) {

	        currentlyRendering = true;
            currentAnimation = spritesheet;
	
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






}// end class PlatformerCharacterController.....


