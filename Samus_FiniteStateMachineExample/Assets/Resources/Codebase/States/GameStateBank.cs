///*  
// *  GameStateBank Class
// * 
// *  Written By: Russell Jahn
// * 
// */
//
//using GameFramework.States;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using UnityEngine;
//
//namespace GameFramework.States {
//	/* A storage bank containing GameStates. It provides an abstraction to store GameStates. You give the bank
//	 * an array of names of states to create as strings, and the bank will populate itself with these states. You can
//	 * then retrieve a state through using the name as a string. */
//	public class GameStateBank {
//	
//		private Dictionary <string, GameState> stateBank;
//		
//		
//		/* Constructor. */
//		public GameStateBank() {
//			stateBank = new Dictionary<string, GameState>();
//		}
//		
//		
//		/* Adds GameState to the bank. GameState must have a unique name field which cannot be null. */
//		public void AddGameStates(string [] gameStateNames) {
//			/* Valid that argument is valid. */
//			System.Diagnostics.Debug.Assert(gameStateNames != null, 
//				"'gameStateNames' must not be null");
//			
//			foreach (string stateName in gameStateNames) {
//				/* Ensure that current state to add isn't already in the bank. */
//				System.Diagnostics.Debug.Assert(!stateBank.ContainsKey(stateName), 
//					String.Format("'{0}' is already used in the GameStateBank!", stateName));
//				
//				/* Create a new state to add to the bank, and verify that it was created correctly. */
//				GameState newState = (GameState)Activator.CreateInstance(Type.GetType(stateName));
//				System.Diagnostics.Debug.Assert(newState != null, 
//					String.Format("Failed creating state '{0}'! Is this the exact name of a valid GameState class?", stateName));
//				
//				/* Add animation to the bank. */
//				stateBank.Add(stateName, newState);
//			}
//		}
//		
//		
//		/* Retrieves a GameState from the bank by name as a string. Returns null if GameState 
//		 * isn't in the bank. */
//		public GameState RetrieveGameState(string gameStateName) {
//			/* Ensure GameState exists in bank, or return null. */
//			if (!stateBank.ContainsKey(gameStateName)) {
//				return null;	
//			}
//			return stateBank[gameStateName];
//		}
//
//	
//		/* Returns a string representation of the GameStateBank. */
//		public override string ToString() {
//			StringBuilder returnString = new StringBuilder();
//			returnString.Append('{');
//			foreach (KeyValuePair<string, GameState> entry in stateBank) {
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