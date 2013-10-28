using UnityEngine;
using System.Collections;

public class Wheel5 : MonoBehaviour {

	public int totalLevels = 5;
	
	int level = 1;
	GameObject[] tiles;
	
	// Use this for initialization
	void Start () {
		tiles = new GameObject[totalLevels];
		for(int i = 1; i <= totalLevels; i++){
			tiles[i - 1] = transform.FindChild("Tile" + i).gameObject;
		}
		
		tiles[0].GetComponent<Tile>().fade = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ScrollLeft(){
		if(level < totalLevels){
			level++;
		}else{
			level = 1;
		}
		
		Scroll();
		
	}
	
	public void ScrollRight(){
		if(level > 1){
			level--;
		}else{
			level = totalLevels;
		}
		
		Scroll();
		
	}
	
	void Scroll(){
		switch (level){
			case 1:
        		Debug.Log("Case 1");
				iTween.MoveTo(tiles[0], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[1], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[3], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[4], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
        		break;
    		case 2:
        		Debug.Log("Case 2");
				iTween.MoveTo(tiles[1], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[2], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[3], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[4], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
        		break;
    		case 3:
        		Debug.Log("Case 3");
				iTween.MoveTo(tiles[2], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[3], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[4], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
        		break;
			case 4:
        		Debug.Log("Case 4");
				iTween.MoveTo(tiles[3], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[4], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[0], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
        		break;
			case 5:
        		Debug.Log("Case 5");
				iTween.MoveTo(tiles[4], new Vector3( 0.0f,0.0f, 0.0f), 2.00f);	//tile5
				tiles[4].GetComponent<Tile>().fade = false;
				iTween.MoveTo(tiles[0], new Vector3( 2.0f,0.0f, 2.0f), 2.00f);	//tile1
				tiles[0].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[1], new Vector3( 1.8f,0.0f, 4.0f), 2.00f);	//tile2
				tiles[1].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[2], new Vector3(-1.8f,0.0f, 4.0f), 2.00f);	//tile3
				tiles[2].GetComponent<Tile>().fade = true;
				iTween.MoveTo(tiles[3], new Vector3(-2.0f,0.0f, 2.0f), 2.00f);	//tile4
				tiles[3].GetComponent<Tile>().fade = true;
        		break;
		}
	}
}
