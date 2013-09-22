/*  
 *  Sprite Animation Based Object Interface
 * 
 *  Written By: Russell Jahn
 * 
 */

using GameFramework;
using GameFramework.Animation;

namespace GameFramework.Interfaces
{
	/* Interface for a GameObject that is backed by a SpriteAnimation. */
	public interface SpriteAnimationBased
	{
		///* Animation that backs the GameObject. */
		//SpriteAnimation animation {get; set;}
		
		/* Animator that animates the GameObject. */
		SpriteAnimator animator {get; set;}
		
		/* When implementing this interface, the GameObject must provide
		 * a guarantee to somehow render animations to the screen. */
		void Render();

	}
}

