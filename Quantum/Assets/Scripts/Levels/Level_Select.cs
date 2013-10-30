using UnityEngine;
using System.Collections;

public class Level_Select : MonoBehaviour {
	public int levelCap = 5;
	
	private int level = 1;
	
	// Use this for initialization
	void Start () {
		

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
//        GUIContent[] items = new GUIContent[50];
//		int index = 1;
//		int xCount = 10;
//		
//		while (index < items.Length){
//			GUILayout.BeginHorizontal();
//			
//			for (int x = 0; x < xCount; x++){
//				// If index is greater than array size, end loop.
//				if (index >= items.Length)
//					break;
//					// Otherwise, create button for item at the current index, increase index by one.
//					GUILayout.Button(items[index++]);
//			}
//                // End a Row
//                GUILayout.EndHorizontal();
//		}
		
		int sideWidth = Screen.width / 8;
		int sideHeight = Screen.height / 3;
		
		if (GUI.Button (new Rect (Screen.width / 22, Screen.height/3, sideWidth, sideHeight), "Scroll Left")) {
			print ("Scrolling left!");
			if(level > 1)
				level--;
		}
		
		if (GUI.Button (new Rect (Screen.width - sideWidth, Screen.height/3, sideWidth, sideHeight), "Scroll Right")) {
			print ("Scrolling right!");
			if(level < levelCap)
				level++;
		}
		
		if (GUI.Button (new Rect (Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "Level Load")) {
			print ("Loading level " + level);
			//Application.LoadLevel("scene_prototype_level" + level);
		}
	}
}
