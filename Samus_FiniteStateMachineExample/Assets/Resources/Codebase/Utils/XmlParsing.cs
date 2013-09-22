/*  
 *  XmlParser Class
 * 
 *  Written By: Russell Jahn
 * 
 */
using GameFramework.Animation;
using GameFramework.States;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;
using UnityEngine;






//public class AnimationParser : MonoBehaviour {
namespace GameFramework.Utils {
	
	
	
	class XmlParser {		
		
		
		/* 
		 *  Parses Xml animation file into SpriteAnimations & fills dictionary with 
		 *  <animationNameAsString, SpriteAnimation> pairs. 
		 *  PATH is location to Xml file to parse, relative to the '/Assets/Resources/' directory.
		 */
		public static void ParseSpriteAnimationDataXml(string path, Dictionary <string, SpriteAnimation> dictionary) {
			
			//Debug.Log("Path before assertions: " + path);
			
			/* Verify arguments. */
			System.Diagnostics.Debug.Assert(dictionary != null, "'dictionary' must not be null!");
			System.Diagnostics.Debug.Assert(path != null, "Xml 'path' must not be null!");
			
			//Debug.Log("Path after assertions: " + path);
			
			
			/* Used to check what animations have been added. */
			string debug_animationsAdded = "[";
			
			
		    /*  'directoryToProject'/Assets/Resources/'XmlFilePath'  */
		    string xmlFolderPath = Application.dataPath + "/Resources/" + path.Trim();
		
		    /*  'directoryToProject'/Assets/Resources/  */
		    string spritesheetFolderPath;
		
			/* Variables to hold data from the Xml file. */
		    string currentName;
		    string currentFilename;
		    int currentFirstFrame;
		    int currentLastFrame;
		    float currentFPS;
			string currentShaderName;
		
		    XmlReaderSettings settings = new XmlReaderSettings();
		    settings.IgnoreWhitespace = true;
		    settings.CloseInput = true; // Close FileStream when XmlReader is closed.
		
		    /* A 'using' block calls the Dispose method on the reader after the block finishes. */
		    using (XmlReader reader = XmlReader.Create( new FileStream(xmlFolderPath, FileMode.Open), settings ) ) {
		        try {
		            reader.MoveToContent(); /* skip header comments & whitespace */
		            reader.ReadStartElement("animation_data");
		         
		            reader.MoveToContent(); /* skip comments & whitespace */
		
		            /* Get spritesheet folder path */
		            reader.ReadStartElement("spritesheet_folder");
		            reader.MoveToContent();
		            //Debug.Log("reader.name: " + reader.name);
		            spritesheetFolderPath = reader.ReadElementContentAsString("location", "").Trim() + "/";
		            reader.ReadEndElement(); /* ...of "spritesheet_folder" */
		
		            if (spritesheetFolderPath == null) {
		                throw new XmlException("Ill-formed Xml file! No 'spritesheet_folder' defined.");
		            }
		
		            while (reader.Read() && reader.IsStartElement()) { /* Read Xml body content. */
		                reader.MoveToContent(); /* skip comments & whitespace */
		                //Debug.Log("name (Should be Animation): " + reader.name);
		                //Debug.Log("reader.IsStartElement? " + reader.IsStartElement());
		                reader.ReadStartElement();
					
		                //Debug.Log("name (Before Read name)): " + reader.Name);
		                currentName = reader.ReadElementContentAsString("name", "").Trim();
		                //Debug.Log("name (Before Read filename)): " + reader.Name);
		                currentFilename = reader.ReadElementContentAsString("filename", "").Trim();
		                //Debug.Log("name (Before Read first_frame)): " + reader.Name);
		                currentFirstFrame = reader.ReadElementContentAsInt("first_frame", "");
		                //Debug.Log("name (Before Read last_frame)): " + reader.Name);
		                currentLastFrame = reader.ReadElementContentAsInt("last_frame", "");
		                //Debug.Log("name (Before Read FPS)): " + reader.Name);
		                currentFPS = reader.ReadElementContentAsFloat("fps", "");
						//Debug.Log("name (Before Read shader_name): " + reader.Name);
						currentShaderName = reader.ReadElementContentAsString("shader_name", "").Trim();
		                
						reader.MoveToContent(); /* skip comments & whitespace */
						
		                /*** NO NEED TO CALL reader.ReadEndElement()!!!  ***
		                 * Already handled by reader.Read() in while loop! */
		
		                //Debug.Log("reader.IsStartElement (Should be false)? " + reader.IsStartElement());
		
		                /*  'directoryToProject'/Assets/Resources/'currentFilename'  */
		                Texture2D image = (Texture2D)Resources.Load(spritesheetFolderPath + currentFilename);
		
		                if (image == null) {
		                    throw new IOException("Image could not be loaded: " + spritesheetFolderPath + currentFilename);
		                }
		                else {
		                    //Debug.Log("Image loaded: " + spritesheetFolderPath + currentFilename);
		                }
						
						/* Add animation to dictionary. */
		                dictionary.Add(currentName, new SpriteAnimation(
								currentName, image, currentFirstFrame, currentLastFrame, currentFPS, currentShaderName
							)
						);
		
						
						/* For debugging. */
						debug_animationsAdded += currentName + ", ";
						
		            } // End while()
		        } // End try block
		
		        catch (XmlException e) {
		            Debug.Log("Xml ERROR: " + e.Message + " in file '" + xmlFolderPath + "'.");
		            Debug.Log("Stack trace: " + e.StackTrace);
		        }
		    }
			Debug.Log("Done parsing Xml. SpriteAnimation dictionary contains: " + debug_animationsAdded.TrimEnd(", ".ToCharArray()) + "]");
		} // end ParseSpriteAnimationDataXml()
		
		
		
		
		
		
		

		/* 
		 *  Parses Xml animation file into ActionStates & fills dictionary with 
		 *  <actionStateNameAsString, ActionState> pairs. 
		 *  PATH is location to Xml file to parse, relative to the '/Assets/Resources/' directory.
		 * 
		 *  The entityState that these actionStates are associated to must also be provided.
		 */
		public static void ParseActionStateDataXml(string path, Dictionary <string, ActionState> dictionary, EntityState entityStateToAttachTo) {
			
			//Debug.Log("Path before assertions: " + path);
			
			/* Verify arguments. */
			System.Diagnostics.Debug.Assert(dictionary != null, "'dictionary' must not be null!");
			System.Diagnostics.Debug.Assert(path != null, "Xml 'path' must not be null!");
			System.Diagnostics.Debug.Assert(entityStateToAttachTo != null, "'entityStateToAttachTo' must not be null!");
			
			//Debug.Log("Path after assertions: " + path);
			
			
			/* Used to check what states have been added. */
			string debug_statesAdded = "[";
			
			/* Holds namespace of Action States once extracted from Xml file. */
			string actionStatesNamespace;
			
		    /*  'directoryToProject'/Assets/Resources/'XmlFilePath'  */
		    string xmlFolderPath = Application.dataPath + "/Resources/" + path.Trim();
		
			/* Variable to hold data from the Xml file. */
		    string currentActionStateName;
		
		    XmlReaderSettings settings = new XmlReaderSettings();
		    settings.IgnoreWhitespace = true;
		    settings.CloseInput = true; // Close FileStream when XmlReader is closed.
		
		    /* A 'using' block calls the Dispose method on the reader after the block finishes. */
		    using (XmlReader reader = XmlReader.Create( new FileStream(xmlFolderPath, FileMode.Open), settings ) ) {
		        try {
					
					//Debug.Log("Opened: " + xmlFolderPath);
					
		            reader.MoveToContent(); /* skip header comments & whitespace */
		            reader.ReadStartElement("action_state_data");      
		            reader.MoveToContent(); /* skip comments & whitespace */

		
		            /* Get the namespace for the action states. */
					actionStatesNamespace = reader.ReadElementContentAsString("namespace", "").Trim();
					
					//Debug.Log("actionStatesNamespace: '" + actionStatesNamespace + "'");
					//Debug.Log("reader.Value(): '" + reader.Value + "'");
		
		            if (actionStatesNamespace == null) {
		                throw new XmlException("Ill-formed Xml file! No 'namespace' defined.");
		            }			
					
					reader.MoveToContent(); /* skip comments & whitespace */
					
					
					//Debug.Log("name (Before first reader.read()): " + reader.Name);
		            while (reader.Read()) { /* Read Xml body content. */
						//Debug.Log("Made it into parsing loop!");
						
						//Debug.Log("name (Before reader.MoveToContent)): " + reader.Name);
						
		                reader.MoveToContent(); /* skip comments & whitespace */
		                //Debug.Log("reader.IsStartElement? " + reader.IsStartElement());
					
		                //Debug.Log("name (Before currentActionStateName = reader.Value)): " + reader.Name);
						//Debug.Log("value (Before currentActionStateName = reader.Value)): '" + reader.Value + "'");
		                currentActionStateName = reader.Value;//reader.ReadElementContentAsString("action_state", "").Trim();
		                
						//Debug.Log("currentActionStateName: " + currentActionStateName);
						
						reader.Read(); /* Advance past text segment of current element. */
						reader.ReadEndElement(); /* Advance past the end of the current element. */
						
						reader.MoveToContent(); /* skip comments & whitespace */
						
		                /*** NO NEED TO CALL reader.ReadEndElement()!!!  ***
		                 * Already handled by reader.Read() in while loop! */
		
		                //Debug.Log("reader.IsStartElement (Should be false)? " + reader.IsStartElement());
						
						//Debug.Log("Before creating new ActionState, currentActionStateName: '" + currentActionStateName + "'");
						//Debug.Log("Before creating new ActionState, typeof(currentActionStateName): '" + currentActionStateName + "'");
						
						//Debug.Log("Before creating new ActionState, Type.GetType(this).Assembly.GetType(currentActionStateName): '" 
						//	+ Type.GetType(this).Assembly.GetType(currentActionStateName) + "'");
						
						//Debug.Log("Before creating new ActionState, entityStateToAttachTo: '" + entityStateToAttachTo + "'");
						
		                /*  Create a new instance of the ActionState retrieved from the Xml file. */
		                ActionState newActionState = (ActionState)Activator.CreateInstance(
							//Type.GetType(currentActionStateName).Assembly.GetType(currentActionStateName),
							Type.GetType (actionStatesNamespace + "." + currentActionStateName),
							new System.Object [] {entityStateToAttachTo}
						);
						
						
		                if (newActionState == null) {
		                    throw new IOException("ActionState could not be created: " + currentActionStateName);
		                }
		                else {
		                    //Debug.Log("ActionState created: " + currentActionStateName);
		                }
						
						/* Add ActionState to dictionary. */
		                dictionary.Add(currentActionStateName, newActionState);
		
						/* Used for debugging. */
						debug_statesAdded += currentActionStateName + ", ";
						
		            } // End while()
		        } // End try block
		
		        catch (XmlException e) {
		            Debug.Log("Xml ERROR: " + e.Message + " in file '" + xmlFolderPath + "'.");
		            Debug.Log("Stack trace: " + e.StackTrace);
		        }
		    }
			Debug.Log("Done parsing Xml. ActionState dictionary contains: " + debug_statesAdded.TrimEnd(", ".ToCharArray()) + "]");
		} // end ParseXml()
		
		
		
		
		
		
		
		/* 
		 *  Parses Xml animation file into PropertyStates & fills dictionary with 
		 *  <actionStateNameAsString, PropertyState> pairs. 
		 *  PATH is location to Xml file to parse, relative to the '/Assets/Resources/' directory.
		 * 
		 *  The entityState that these actionStates are associated to must also be provided.
		 */
		public static void ParsePropertyStateDataXml(string path, Dictionary <string, PropertyState> dictionary, EntityState entityStateToAttachTo) {
			
			//Debug.Log("Path before assertions: " + path);
			
			/* Verify arguments. */
			System.Diagnostics.Debug.Assert(dictionary != null, "'dictionary' must not be null!");
			System.Diagnostics.Debug.Assert(path != null, "Xml 'path' must not be null!");
			System.Diagnostics.Debug.Assert(entityStateToAttachTo != null, "'entityStateToAttachTo' must not be null!");
			
			//Debug.Log("Path after assertions: " + path);
			
			
			/* Used to check what states have been added. */
			string debug_statesAdded = "[";
			
			/* Holds namespace of Property States once extracted from Xml file. */
			string propertyStatesNamespace;
			
		    /*  'directoryToProject'/Assets/Resources/'XmlFilePath'  */
		    string xmlFolderPath = Application.dataPath + "/Resources/" + path.Trim();
		
			/* Variable to hold data from the Xml file. */
		    string currentPropertyStateName;
		
		    XmlReaderSettings settings = new XmlReaderSettings();
		    settings.IgnoreWhitespace = true;
		    settings.CloseInput = true; // Close FileStream when XmlReader is closed.
		
		    /* A 'using' block calls the Dispose method on the reader after the block finishes. */
		    using (XmlReader reader = XmlReader.Create( new FileStream(xmlFolderPath, FileMode.Open), settings ) ) {
		        try {
					
					//Debug.Log("Opened: " + xmlFolderPath);
					
		            reader.MoveToContent(); /* skip header comments & whitespace */
		            reader.ReadStartElement("property_state_data");
		            reader.MoveToContent(); /* skip comments & whitespace */
		
					
					/* Get the namespace for the property states. */
					propertyStatesNamespace = reader.ReadElementContentAsString("namespace", "").Trim();
					
					//Debug.Log("propertyStatesNamespace: '" + propertyStatesNamespace + "'");
					//Debug.Log("reader.Value(): '" + reader.Value + "'");
		
		            if (propertyStatesNamespace == null) {
		                throw new XmlException("Ill-formed Xml file! No 'namespace' defined.");
		            }			
					
					reader.MoveToContent(); /* skip comments & whitespace */
					
					
					//Debug.Log("name (Before first reader.read()): " + reader.Name);
		            while (reader.Read()) { /* Read Xml body content. */
						//Debug.Log("Made it into parsing loop!");
						
						//Debug.Log("name (Before reader.MoveToContent)): " + reader.Name);
						
		                //reader.MoveToContent(); /* skip comments & whitespace */
		                //Debug.Log("reader.IsStartElement? " + reader.IsStartElement());
		                //reader.ReadStartElement();
					
		                //Debug.Log("name (Before currentPropertyStateName = reader.Value)): " + reader.Name);
		                currentPropertyStateName = reader.Value;//reader.ReadElementContentAsString("action_state", "").Trim();
		                
						//Debug.Log("currentPropertyStateName: " + currentPropertyStateName);
						
						reader.Read(); /* Advance past text segment of current element. */
						reader.ReadEndElement(); /* Advance past the end of the current element. */
						
						reader.MoveToContent(); /* skip comments & whitespace */
						
		                /*** NO NEED TO CALL reader.ReadEndElement()!!!  ***
		                 * Already handled by reader.Read() in while loop! */
		
		                //Debug.Log("reader.IsStartElement (Should be false)? " + reader.IsStartElement());
		
		                /*  Create a new instance of the PropertyState retrieved from the Xml file. */
		                PropertyState newPropertyState = (PropertyState)Activator.CreateInstance(
							Type.GetType(propertyStatesNamespace + "." + currentPropertyStateName),
							new System.Object [] {entityStateToAttachTo});
						//PropertyState newPropertyState = (PropertyState)Activator.CreateInstance(Type.GetType(currentPropertyStateName));
						
						/* Set the attached entity of the new Property State. */
						//newPropertyState.attachedEntityState = entityStateToAttachTo;
						
						
		                if (newPropertyState == null) {
		                    throw new IOException("PropertyState could not be created: " + currentPropertyStateName);
		                }
		                else {
		                    //Debug.Log("PropertyState created: " + currentPropertyStateName);
		                }
						
						/* Add PropertyState to dictionary. */
		                dictionary.Add(currentPropertyStateName, newPropertyState);
		
						/* Used for debugging. */
						debug_statesAdded += currentPropertyStateName + ", ";
						
		            } // End while()
		        } // End try block
		
		        catch (XmlException e) {
		            Debug.Log("Xml ERROR: " + e.Message + " in file '" + xmlFolderPath + "'.");
		            Debug.Log("Stack trace: " + e.StackTrace);
		        }
		    }
			Debug.Log("Done parsing Xml. PropertyState dictionary contains: " + debug_statesAdded.TrimEnd(", ".ToCharArray()) + "]");
		} // end ParseXml()
		
		
		
		
	}
}