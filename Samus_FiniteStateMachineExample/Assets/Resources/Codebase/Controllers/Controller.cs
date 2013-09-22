//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//namespace GameFramework.Controller {
//	
//	/* Maps a virtual button or axis on the controller to a Unity input button/key. */
//	abstract class VirtualController {
//		
//		/* Mapping of virtual button names to input objects. */
//		private Dictionary <string, ControllerInput> inputMap;
//		
//		/* Maps a virtual button on the controller to a Unity input button/key. */
//		public abstract void ChangeButtonMapping(string virtualButtonname, string keyToMapTo);
//		
//		public abstract void HeldDown(string virtualButtonname);
//		
//		public abstract void PressedDown(string virtualButtonname);
//		
//		public abstract void Released(string virtualButtonname);
//		
//		public abstract void GetVirtualButtonArray();
//		 
//		public abstract void GetVirtualAxisArray();
//		
//		
//		
//		/* Maps a virtual button name 'virtualname' to Unity mouse button name 'click'. */
//		//public void MapMouseClick(string virtualname, string clickname);
//		
//		/* Maps a virtual button name 'virtualname' to Unity button name 'button'. */
//		//public void MapButton(string virtualname, string buttonname);
//		
//		//public void MapKey(string virtualname, string keyname);
//		
//		/* Jump  -> button1
//		 * Run   -> button2
//		 * Shoot -> button3*/
//		
//		/*
//		public bool HeldDown(string name) {
//			InputBase i = GetMapping(name);	
//			if (i == instanceof ClickInput) {
//				i.HeldDown();	
//			}
//			else throw argument exception;
//		}
//		*/	
//		
//	}
//	
//	
//	abstract class ControllerInput {
//		private string mappedTo {get; set;}	
//	}
//	
//	
//	abstract class ClickBasedInput : ControllerInput {
//		private string mappedTo;
//		public abstract bool HeldDown();
//		public abstract bool PressedDown();
//		public abstract bool Released();	
//	}
//	
//	
//	class Button : ClickBasedInput {
//		private string mappedTo;
//		
//		/* Returns true while the button identified by 'mappedTo' is held down. */
//		public override bool HeldDown() {
//			return Input.GetButton(mappedTo);	
//		}
//		
//		/* Returns true during the frame the user pressed down the button identified by 'mappedTo'.*/
//		public override bool PressedDown() {
//			return Input.GetButtonDown(mappedTo);		
//		}
//		
//		/* Returns true the first frame the user releases the button identified by 'mappedTo'. */
//		public override bool Released() {
//			return Input.GetButtonUp(mappedTo);	
//		}
//	}
//	
//	
//	class MouseClick : ClickBasedInput {
//		private string mappedTo;
//		
//		/* Returns true while the mouse button identified by 'mappedTo' is held down. */
//		public override bool HeldDown() {
//			return Input.GetMouseButton(mappedTo);	
//		}
//		
//		/* Returns true during the frame the user pressed down the mouse button identified by 'mappedTo'.*/
//		public override bool PressedDown() {
//			return Input.GetMouseButtonDown(mappedTo);		
//		}
//		
//		/* Returns true the first frame the user releases the mouse button identified by 'mappedTo'. */
//		public override bool Released() {
//			return Input.GetMouseButtonUp(mappedTo);	
//		}
//	}
//	
//	
//	class Key : ClickBasedInput {
//		private string mappedTo;
//		
//		/* Returns true while the key identified by 'mappedTo' is held down. */
//		public override bool HeldDown() {
//			return Input.GetKey(mappedTo);	
//		}
//		
//		/* Returns true during the frame the user pressed down the key identified by 'mappedTo'.*/
//		public override bool PressedDown() {
//			return Input.GetKeyDown(mappedTo);		
//		}
//		
//		/* Returns true the first frame the user releases the key identified by 'mappedTo'. */
//		public override bool Released() {
//			return Input.GetKeyUp(mappedTo);	
//		}
//	}
//	
//	
//	class Axis : ControllerInput {
//		private string mappedTo;
//		
//		/*  Returns the value of the axis identified by 'mappedTo'. */
//		public float GetAxis() {
//			return Input.GetAxis(mappedTo);	
//		}
//			
//		/* Returns the value of the axis identified by 'mappedTo' with no smoothing filtering applied. */
//		public float GetAxisRaw() {
//			return Input.GetAxisRaw(mappedTo);	
//		}
//	}
//	
//}
//
///*
//GetJoysticknames	 Returns an array of strings describing the connected joysticks.
//
//ResetInputAxes	 Resets all input. After ResetInputAxes all axes return to 0 and all buttons return to 0 for one frame.
//GetAccelerationEvent	 Returns specific acceleration measurement which occurred during last frame. (Does not allocate temporary variables)
//
//GetTouch	 Returns object representing status of a specific touch. (Does not allocate temporary variables)
//*/
//
//
