///*  
// *  SpriteAnimationBank Class
// * 
// *  Written By: Russell Jahn
// * 
// */
//
//using GameFramework.Animation;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using  System.Text;
//using UnityEngine;
//
//namespace GameFramework.Animation {
//	/* A storage bank containing SpriteAnimations. */
//	public class SpriteAnimationBank {
//	
//		private Dictionary <string, SpriteAnimation> animationBank;
//		
//		
//		/* Constructor. */
//		public SpriteAnimationBank() {
//			animationBank = new Dictionary<string, SpriteAnimation>();
//		}
//		
//		
//		/* Adds SpriteAnimation to the bank. SpriteAnimation must have a unique name field which cannot be null. */
//		public void AddSpriteAnimation(SpriteAnimation animationToAdd) {
//			/* Valid that argument is valid. */
//			System.Diagnostics.Debug.Assert(animationToAdd.name != null, 
//				"'animationToAdd' must not have a null 'name' field!");
//			System.Diagnostics.Debug.Assert(!animationBank.ContainsKey(animationToAdd.name), 
//				String.Format("'{0}' is already used in the SpriteAnimationBank!", animationToAdd.name));
//				
//			/* Add animation to the bank. */
//			animationBank.Add(animationToAdd.name, animationToAdd);
//		}
//		
//		
//		/* Retrieves a SpriteAnimation from the bank by name as a string. Returns null if SpriteAnimation 
//		 * isn't in the bank. */
//		public SpriteAnimation RetrieveSpriteAnimation(string spriteAnimationname) {
//			/* Ensure SpriteAnimation exists in bank, or return null. */
//			if (!animationBank.ContainsKey(spriteAnimationname)) {
//				return null;	
//			}
//			return animationBank[spriteAnimationname];
//		}
//
//	
//		/* Returns a string representation of the SpriteAnimationBank. */
//		public override string ToString() {
//			StringBuilder returnString = new StringBuilder();
//			returnString.Append('{');
//			foreach (KeyValuePair<string, SpriteAnimation> entry in animationBank) {
//				returnString.Append(entry.Key);
//				returnString.Append(',');
//				returnString.Append(' ');
//			}
//			returnString.Remove(returnString.Length - 2, 2);
//			returnString.Append('}');
//			return returnString.ToString();
//		}
//		
//	}
//}