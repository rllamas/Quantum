using UnityEngine;
using System.Collections;

public class Wheel3 : MonoBehaviour {
	public int totalGroups = 3;
	
	int selectedGroup = 1;
	GameObject[] tiles;
	
	// Use this for initialization
	void Start () {
		tiles = new GameObject[totalGroups];
		for (int i = 1; i <= totalGroups; ++i) {
			tiles[i - 1] = transform.FindChild("Tile" + i).gameObject;
		}
		
		tiles[0].GetComponent<Tile>().fade = false;
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	public void ScrollLeft() {
		if (selectedGroup < totalGroups) {
			selectedGroup++;
		} else {
			selectedGroup = 1;
		}
		
		Scroll();
		
	}
	
	
	
	public void ScrollRight() {
		if (selectedGroup > 1) {
			selectedGroup--;
		} else {
			selectedGroup = totalGroups;
		}
		
		Scroll();
		
	}
	
	
	
	void Scroll() {
		switch (selectedGroup) {
			case 1:
        		Debug.Log("Case 1");
				iTween.MoveTo(tiles[0], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[1], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
        		break;
    		case 2:
        		Debug.Log("Case 2");
				iTween.MoveTo(tiles[1], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[2], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
        		break;
    		case 3:
        		Debug.Log("Case 3");
				iTween.MoveTo(tiles[2], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[0], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
        		break;	
		}
	}
	
	
}
