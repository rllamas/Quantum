/*  
 *  Sprite Animator Machine
 * 
 *  Written By: Russell Jahn
 * 
 */

using GameFramework.Animation;
using System.Collections;
using System;
using UnityEngine;


namespace GameFramework.Animation {

	/* 
	 * A SpriteAnimator takes an object to draw to and draws a SpriteAnimation to its rendering
	 * component.
	 *  
	 * NO METHODS ARE SYNCHRONIZED FOR THREAD-SAFETY! END-USER MUST DO OWN EXTERNAL SYNCHRONIZATION!!! 
	 * 
	 * */
	public class SpriteAnimator : MonoBehaviour {
		
		
		#region Internal variables.
		private GameObject objectToAnimate;
		private int currentFrame;
		private bool loop;
	    private new SpriteAnimation currentAnimation;
		private SpriteAnimation previousAnimation;
		
	    //private volatile float timeSinceLastDraw;
		private volatile float lastDrawTime;
		
		/* bool indicating an that a new animation is loading. */
		private volatile bool loadingAnimation;
		
		/* bool indicating that an animation is currently playing. */
		private volatile bool currentlyPlaying;
		
		/* bool indicating that an animation has finished playing. */
		private volatile bool finishedPlaying;
		#endregion

			
		#region Public properties.
		/* Is the animation set to loop? */
		public bool Loop {
			get {
				return loop;	
			} 
			
			set {
				loop = value;
			}
		}
		
		
		/* The SpriteAnimation loaded in the SpriteAnimator. */
		public SpriteAnimation CurrentAnimation {
			get {
				return currentAnimation;	
			} 
		}
		
		
		/* The SpriteAnimation loaded in the SpriteAnimator. */
		public SpriteAnimation PreviousAnimation {
			get {
				return previousAnimation;	
			} 
		}
		
		
		/* The object that the SpriteAnimator is set to animate. */
		public GameObject ObjectToAnimate {
			get {
				return objectToAnimate;	
			}
			
			set {
				objectToAnimate = value;	
			}
		}
		
		
		/* The current frame of the loaded SpriteAnimation. Must be a valid frame number. */
	    public int CurrentFrame {
			get {
				return currentFrame;	
			}

			set {
				Debug.Log("Calling setCurrentFrame!");
				if (value < currentAnimation.StartFrame) {
					throw new ArgumentException("Attemping to set current frame to smaller value than animation's beginning frame!");	
				}
				else if (value > currentAnimation.EndFrame) {
					throw new ArgumentException("Attemping to set current frame to greater value than animation's last frame!");	
				}
				currentFrame = value;
			}
		}
		#endregion

		
		#region Constructors.
		public SpriteAnimator () {
			finishedPlaying = false;
		}
		
		
		public SpriteAnimator (GameObject objectToAnimate) {
			this.objectToAnimate = objectToAnimate;
			finishedPlaying = false;
		}


		public SpriteAnimator (GameObject objectToAnimate, SpriteAnimation animation) {
			this.objectToAnimate = objectToAnimate;
			finishedPlaying = false;
			Debug.Log("Loading animation: " + animation.Name);
			LoadAnimation(animation);
		}
		#endregion
		
		
		#region SpriteAnimation Interface.
		
		
		/* Plays animation from beginning frame until the end.  Will loop if looping is set. */
		public void Play () {
			/* Ensure that necessary fields are set correctly before rendering. */
			validateConditions();
			
			//Debug.Log("In SpriteAnimator.Play(); 'loadingAnimation': " + loadingAnimation);
			currentFrame = currentAnimation.StartFrame;
			
			StartCoroutine( "PlayFrames", (currentAnimation.EndFrame - currentFrame + 1) );
		}

		
		/* IEnumerator that renders the next frame for NUMBER_OF_FRAMES_TO_PLAY times. Will keep looping
		 * if 'loop' is set. */
		private IEnumerator PlayFrames(int numberOfFramesToPlay) {
			currentlyPlaying = true;
			//Debug.Log(string.Format ("In PlayFrames(): numberOfFramesToPlay is '{0}'.", numberOfFramesToPlay));
			do {
				/* Play entire animation sequence from current frame. */
				for (int i = 0; i < numberOfFramesToPlay; i++) {
					
					/* If another frame is being loaded, break out of the current rendering loop. */
					if (loadingAnimation) {
						currentlyPlaying = false;
						yield break;	
					}
					
					//Debug.Log(string.Format ("In PlayFrames(): Playing frame {0} out of {1} for animation '{2}'.", i+1,
					//	numberOfFramesToPlay, currentAnimation.Name));
					
					PlayNextFrame();
					
					/* Wait enough time between frames to maintain the animation's framerate. */
					yield return new WaitForSeconds(1.0f/currentAnimation.Fps); 
					
				}
			}
			while (loop);
			
			currentlyPlaying = false;
			finishedPlaying = true;
			yield break;	
		}
		
		
		/* Will play the next frame of the loaded animation. Sets current frame to the next
		 * frame of the animation sequence. If the last frame was played and looping was set, 
		 * the current frame will be set to the animation's beginning frame. */
		private void PlayNextFrame () {

			RenderCurrentFrame();
			
			/* If not at the last frame yet, advance the current frame by one. */
			if (currentFrame < currentAnimation.EndFrame) {
				currentFrame++;	
			}
			
			/* Else, if at last frame and set to loop, set the current frame to animation's beginning frame. */
			else if (loop) {
				currentFrame = currentAnimation.StartFrame;
			}
			
		}


		/* Renders current frame to 'objectToAnimate'. */
		private void RenderCurrentFrame () {
			if (!loadingAnimation) {
				// Display correct frame on objectToAnimate.
		        objectToAnimate.renderer.material.mainTextureOffset =
		            new Vector2((float)(currentFrame * currentAnimation.Spritesheet.height) / currentAnimation.Spritesheet.width, 0f);
			}
		}
		
		
		/* Returns a boolean indicating whether the animation finished playing. */
		public bool IsFinished () {
			Debug.Log("In IsFinished() : " + finishedPlaying);
			return finishedPlaying;
		}
		

		/* Sets current frame to beginning frame. */
		public void SetCurrentFrameToBeginning () {
			currentFrame = currentAnimation.StartFrame;
		}
		
		
		/* Loads a SpriteAnimation to draw to. Sets current frame to the beginning and looping to false. */
		public void LoadAnimation (SpriteAnimation animationToLoad) {
			StartCoroutine("LoadAnimationHelper", animationToLoad);
		}
		
		
		/* Handles the actual loading of a new animation. */
		private IEnumerator LoadAnimationHelper(SpriteAnimation animationToLoad) {
		
			/* Set variables to stop animation currently playing. */
			loop = false;
			loadingAnimation = true;
			finishedPlaying = false;		
			
			
			/* Force animating coroutine to stop. */
			StopCoroutine("PlayFrames");
			
			
			/* Set the animation to load as the current animation and update settings. */
			previousAnimation = currentAnimation;
			currentAnimation = animationToLoad;
			currentFrame = animationToLoad.StartFrame;
			
			
			/* Ensure that necessary fields are set correctly. */
			validateConditions();
			
			/* Set the spritesheet as the primary texture of 'objectToAnimate'. */
	        objectToAnimate.renderer.material.mainTexture = currentAnimation.Spritesheet;
			
	        /* Scale the spritesheet properly. */
	        objectToAnimate.renderer.material.mainTextureScale =
	            new Vector2(((float)currentAnimation.Spritesheet.height) / currentAnimation.Spritesheet.width, 1.0f);
			
	        /* Apply shader. */
	        objectToAnimate.renderer.material.shader = Shader.Find(currentAnimation.ShaderName);
			
			/* Move spritesheet to appropriate frame. */
			objectToAnimate.renderer.material.mainTextureOffset =
		            new Vector2((float)(currentFrame * currentAnimation.Spritesheet.height) / currentAnimation.Spritesheet.width, 0f);
			
			Debug.Log ("Successfully loaded animation: '" + CurrentAnimation.Name + "'!");
			
			/* Indicate that animation is finished loading. */
			loadingAnimation = false;
			
			yield break;
		}

		/* Ensures that user has set necessary fields for animator to function. */
		private void validateConditions() {
			System.Diagnostics.Debug.Assert(objectToAnimate != null, "'objectToAnimate' must not be null!");
			System.Diagnostics.Debug.Assert(currentAnimation != null, "'currentAnimation' must not be null!");
		}

		
		public override string ToString() {
			return "{SpriteAnimator}";
		}
		#endregion
		
		
	}
}

