// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Pro Example 2
// Actions
// ------------------------------------------------------------------------
// Main example class
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;


public class CProExample2 : MonoBehaviour
{

    private bool dragging = false;                          // dragging indicator
    private GameObject current = null;                      // current active object
    List<OTObject> endingTiles = new List<OTObject>();      // all tiles that are in ending state

    // Create 50 random sized tiles
    void CreateTiles()
    {
        // Lookup the asteroid container
        GameObject tiles = GameObject.Find("-Tiles");
        // Iterate and create 50 tile sprites
        for (int t = 0; t < 50; t++)
        {
            // Create a new tile from its prototype
            OTSprite tile = OT.CreateObject("tile").GetComponent<OTSprite>();
            // Set the parent of this tile to the container
            tile.transform.parent = tiles.transform;
            // Determine a random x size
            float sx = 30 + Random.value * 120;
            // Set size of the tile
            tile.size = new Vector2(sx, sx );
            // Put the tile on a random screen position
            tile.position = new Vector2((Screen.width * -1 / 2) + Random.value * Screen.width, (Screen.height / 2) - Random.value * Screen.height);
            // Set the tile image to a random index
            tile.frameIndex = (int)Mathf.Floor(Random.value * 15);
            // Make the tile draggable
            tile.draggable = true;

            // Add the custom tile action controller to this tile
            tile.AddController(new TileActions(this));

            // hookup delegates
            tile.onMouseEnterOT = OnMouseEnter;     
            tile.onMouseExitOT = OnMouseExit;       
            tile.onDragStart = DragStart;           
            tile.onDragEnd = DragEnd;
            tile.onInput = OnInput;
        }
    }

    // 'short' helper function to get custom tile action controller
    TileActions Actions(OTObject owner)
    {
        return owner.Controller(typeof(TileActions)) as TileActions;
    }

    // public function that will be called from the action controller
    // to move all tiles one level to the back (increasing depth by one )
    
    public void AllTilesDepthPlusOne()
    {
        // Lookup the asteroid container
        GameObject tiles = GameObject.Find("-Tiles");
        // loop all tiles
        for (int t = 0; t < tiles.transform.childCount; t++)
        {
            // only change depth if it is not the dragging object
            if (tiles.transform.GetChild(t).gameObject!=current)
                tiles.transform.GetChild(t).GetComponent<OTObject>().depth++;
        }
    }

    // input handler to capture the right mouse button click
    void OnInput(OTObject owner)
    {
        if (Input.GetMouseButtonDown(1))
        {
            // right mousebutton was clicked
            for (int e = 0; e < endingTiles.Count; e++)
            {
                // move all 'valid' ending tiles one level to the front
                if (endingTiles[e]!=null)
                    endingTiles[e].depth--;
            }
            // add this tile to the ending tiles
            endingTiles.Add(owner);
            // activate the tile ending action tree
            Actions(owner).Run("TileEnd");
        }
    }

    // This function will be called when a tile is started beeing dragged.
    void DragStart(OTObject owner)
    {
        // if this tile is ending stop here
        if (Actions(owner).IsRunning("TileEnd"))
            return;
        // only one tile can be dragged so we need to wait for an enddrag
        if (dragging) return;
        // start the DragStart effect
        dragging = true;
        Actions(owner).Run("DragStart");
    }

    void DragEnd(OTObject owner)
    {
        dragging = false;
        // if this tile is ending stop here
        if (Actions(owner).IsRunning("TileEnd"))
            return;
        else
            Actions(owner).Run("DragEnd");
    }

    void OnMouseEnter(OTObject owner)
    {
        // if this tile is ending or we are dragging stop here
        if (Actions(owner).IsRunning("TileEnd") || dragging)
            return;

        // if the DragEnd actions are playing we need to set the 'Hover' tree in the action queueu
        if (Actions(owner).IsRunning("DragEnd"))
            Actions(owner).Next("Hover");
        else
            Actions(owner).Run("Hover");
		
		System.GC.Collect();
		
        current = owner.gameObject;
    }

    void OnMouseExit(OTObject owner)
    {
        // if this tile is ending or we are dragging stop here
        if (Actions(owner).IsRunning("TileEnd") || dragging)
            return;

        // if the DragEnd actions are playing we need to set the 'HoverOut' tree in the action queueu
        if (Actions(owner).IsRunning("DragEnd"))
           Actions(owner).Next("HoverOut");
       else
           Actions(owner).Run("HoverOut");
    }

	// Use this for initialization
	void Start () {
		OT.Passify();
        // Create 50 random tiles
        CreateTiles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}