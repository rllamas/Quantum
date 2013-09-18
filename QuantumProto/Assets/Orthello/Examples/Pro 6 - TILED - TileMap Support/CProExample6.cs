// ------------------------------------------------------------------------
// Orthello Pro 2D Framework Example Source Code
// (C)opyright 2011 - WyrmTale Games - http://www.wyrmtale.com
// ------------------------------------------------------------------------
// More info http://www.wyrmtale.com/orthello
// ------------------------------------------------------------------------
// Pro Example 6
// TileMap (tiled.exe) support
// ------------------------------------------------------------------------
// Main example class
// ------------------------------------------------------------------------
// OTTilemap Usage
//
// You can use TILED.EXE to create your tilemaps | download @ http://www.mapeditor.org/
//
// > Using TILED.EXE - Map editor
//
// - Only normal XML tile layers are support for now - so no compression or Base64
//
//   Be sure to set saving tile layers to XML in preferences 
//   Menu-> Edit -> Preferences -> TAB:General/Saving And Loading/Store Tile Layer Data as : -> XML
// 
// - Save your tile layers to .XML extension instead of the .tmx
//   This so that Unity will detect the file as a TextAsset.
//
//   You can do this by simply typing this extension in the filename when you save it.
//   if you type *.xml in the filename when loading, the .xml files of a specific
//   floder are shown and you can select the correct file. .xml files are also put in
//   the recent files list.
//
// - Different layers will be created at a certain depth within the OTTilemap.
//   default will be 0, -1, -2, etc.
//
//   If you would like to override the layer depth of a specific TILED layer,
//   add a 'depth' property to the TILED layer by right clicking the layer and
//   selecting 'Layer properties'
//
// > Using the OTTilemap object
//
// - Drag an OTTilemap object to the scene.
//
// - Add the tileset images the the OTTilemap.tileSetImages list in the property 
//   inspector. These images will be mapped by name and used with OTTileMap
//   materials for the corresponding tiles.
//
// - Load a map by assigning a TextAsset from a TILED tilemap (xml) 
//   to the OTTilemap.tileMapXML.
//
// ** After a tilemap is loaded, you can access the tilemap's OTTileSet objects 
// ( tile textures ) through .. OTTilemap.tileSets
// ** After a tilemap is loaded, you can access the tilemap's OTTileMapLayer objects 
// ( tile textures ) through .. OTTilemap.layers
// ** if you change anything like a tileset's image or specific tiles within a
// layer, you can call OTTilemap.Rebuild() to apply these changes.
// ** you can always reload the original tilemap that was assigned by OTTilemap.Reload();
// ------------------------------------------------------------------------
using UnityEngine;
using System.Collections;


public class CProExample6 : MonoBehaviour {


    public TextAsset tiledLevel1;           // used for swapping between tilemap levels

    public TextAsset tiledLevel2;           // used for swapping between tilemap levels

    public Texture tileSetImage1;           // used for swapping between images

    public Texture tileSetImage2;           // used for swapping between images

    OTTileMap tilemap;                      // tilemap reference
    int sewerIdx = 0;                       // used to rotate sewers
	int appMode = 0;						// 0 = display tilemap, 1 = display tilesSprite (uses data from tilemap)
    OTTilesSprite tilesSprite;              // tilesSprite reference
	float pHor = 0;
	float pVer = 0;

	// Use this for initialization
	void Start () {
        // find the tilemap object from this scene
        tilemap = GameObject.Find("TileMap").GetComponent<OTTileMap>();
		tilesSprite = OT.Sprite("TilesSprite") as OTTilesSprite;
		tilesSprite.visible = false;
		OT.Sprite("cover-h").visible = tilesSprite.visible;
		OT.Sprite("cover-v").visible = tilesSprite.visible;
		
	}

	IVector2 lastOffset = new IVector2(-1,-1);
	void UpdateTiles()
	{		
		// determine offset point on tilemap where to retreive the tiles
		IVector2 tilemapOffset = new IVector2(Mathf.FloorToInt(pHor),Mathf.FloorToInt(pVer));
		// determine scolling position tilesprite to get a smooth scrolling effect
		Vector2 scrollPosition = new Vector2(
				-((pHor - tilemapOffset.x) * (float)tilesSprite.tileSize.x),
				(pVer - tilemapOffset.y) * (float)tilesSprite.tileSize.y);

		// check if offset has changed and we need to refill the sprite
		if (!tilemapOffset.Equals(lastOffset))
		{
			tilesSprite.FillFromTileMap(tilemap, tilemapOffset, new Rect(1,0,11,11));
			lastOffset = tilemapOffset;
		}
		// set position of sprite to achieve a smooth scrolling effect
		tilesSprite.position = scrollPosition;
				
	}
	
    void OnGUI()
    {
        // only create a UI if we got a tilemap
        if (tilemap == null) return;
        // create a box and a zoom slider
        GUI.Box(new Rect(4, 30, 200, 200), "");
        GUI.Label(new Rect(10, 45, 40, 30), "Zoom");
        OT.view.zoom = GUI.HorizontalSlider(new Rect(50, 50, 150, 30), OT.view.zoom, -2, 2);

        // swap tilemap level button
        if (GUI.Button(new Rect(20, 70, 170, 27), "Swap Tilemap Level"))
        {
            // toggle the tilemap level by compare the tilemap name and assigning the TILED xml
            if (tilemap.tileMapXML.name == tiledLevel1.name)
                tilemap.tileMapXML = tiledLevel2;
            else
                tilemap.tileMapXML = tiledLevel1;
            // We need to manually rebuild the tilemap
            tilemap.Rebuild();
        }

        // swap image button
        if (GUI.Button(new Rect(20, 100, 170, 27), "Swap Image"))
        {
            // first lets look for the right tileset
            for (int i=0; i<tilemap.tileSets.Length; i++)
            {
                OTTileSet tileset = tilemap.tileSets[i];
                // when we found the right tileset, toggle the image
                // by assinging it.
                if (tileset.image.name == tileSetImage1.name)
                {
                    tileset.image = tileSetImage2;
                    break;
                }
                else
                    if (tileset.image.name == tileSetImage2.name)
                    {
                        tileset.image = tileSetImage1;
                        break;
                    }
            }
            // We need to manually rebuild the tilemap
            tilemap.Rebuild();
        }

        // Change sewer tiles button
        if (GUI.Button(new Rect(20, 130, 170, 27), "Change Sewer Tiles"))
        {
            // first lets find the serwer layer
            for (int i = 0; i < tilemap.layers.Length; i++)
            {
                OTTileMapLayer layer = tilemap.layers[i];
                if (layer.name == "Sewers")
                {
                    // now change all tiles to the same sewer ( sewerIdx )
                    for (int t = 0; t < layer.tiles.Length; t++)
                    {
                        // we hard coded the firstGid (17) of the tileset that
                        // contains the sewer tiles but we could have done a lookup
                        // just call me lazy!
                        if (layer.tiles[t] > 0)
                            layer.tiles[t] = 17 + sewerIdx;
                    }
                    break;
                }
            }
            // adjust the sewerIdx so that will will be rotating the
            // sewer tiles when this button is pressed repeatingly
            sewerIdx++;
            if (sewerIdx == 4) sewerIdx = 0;
            // We need to manually rebuild the tilemap
            tilemap.Rebuild();
        }
        if (GUI.Button(new Rect(20, 160, 170, 27), "Reload Tilemap"))
        {
            // just reload the assign XML to set the tilemap to its initial state.
            tilemap.Reload();
        }		
        if (GUI.Button(new Rect(20, 190, 170, 27), (appMode==0)?"OTTilesSprite Mode":"OTTileMap mode"))
        {
			appMode = 1-appMode;
			
			tilemap.visible = (appMode == 0);
			tilesSprite.visible = !tilemap.visible;	
			OT.Sprite("cover-h").visible = tilesSprite.visible;
			OT.Sprite("cover-v").visible = tilesSprite.visible;
        }
		
		if (appMode==1)
		{
			float dimX = tilemap.LayerByName("ground").layerSize.x - (tilesSprite.tileMatrix.x-2) - 0.1f;
			float dimY = tilemap.LayerByName("ground").layerSize.y - (tilesSprite.tileMatrix.y-2) - 0.1f;
			
			
        	pHor = GUI.HorizontalSlider(new Rect(Screen.width/3, Screen.height-30 , Screen.width/3, 30), pHor, 0, dimX);
			pVer = GUI.VerticalSlider(new Rect(Screen.width-30, Screen.height/3 , 30, Screen.height/3), pVer, 0, dimY);
			
			UpdateTiles();
		}		
		
    }
}
