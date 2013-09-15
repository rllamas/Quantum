using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExampleTilesSprite : MonoBehaviour {
	
	public OTTextSprite txt;
	public OTTextSprite txtHover;
	public int scrollPxPerSec = 50;
	
	// Use this for initialization
	void Start () {	
	}
		
	OTTilesSprite ts;

	Vector2 psStart;				// start 'snap' position of tiles sprite
	Vector2 dY;						// scroll vector
	IVector2[] tsBottomRow;			// tiles of bottom row
	IVector2[] tsTopRow;			// tiles of top row
	
	List<OTSprite> scrollingSprites = new List<OTSprite>(); // floating sprites that have to be scrolled
		
	bool initialized = false;
	void Initialize()
	{
		ts = OT.Sprite("tiles-sprite") as OTTilesSprite;
		// fill the matrix with random tiles
		ts.FillWithRandomTiles();
		// Create new  objects and link them to the tiles
		LinkNewObjects(ts.allTiles);		
		// hook up input event
		ts.onInput = TilesInput;
		// keep start position to be used to snap back after scrolling a row
		psStart = ts.position;
		// get bottom and top row
		tsBottomRow = ts.TilesY(0);
		tsTopRow = ts.TilesY(ts.tileMatrix.y-1);
		// bottom row starts transparent
		ts.Alpha(tsBottomRow,0f);	
		// pre-fabricate tile objects
		OT.PreFabricate("tile",25);
		// collect animation tiles for quicker lookup
		GetAnimationTiles();
	}
	
	void TilesInput(OTObject owner)
	{		
		if (Input.GetMouseButtonDown(0))
		{
			// clicked on the sheet;
			IVector2 tile = ts.hitTile;
			TileInfo tileInfo = (ts.objects[tile.x][tile.y] as TileInfo);
			if (tile!=null)
			{
				txt.text	= "clicked tile "+tile+"\n"+
							  tileInfo.description;
			}
		}
	}
	
	// create info objects and link them to the tile
	void LinkNewObjects(IVector2[] tiles)
	{
		for (int t=0; t<tiles.Length; t++)
		{
			IVector2 tile = tiles[t];
			string description = "tile has frame index "+ts.tiles[tile.x][tile.y]; 
			if (ts.tiles[tile.x][tile.y] == -1)
				description = "tile is transparent";					
			ts.objects[tile.x][tile.y] = new TileInfo(tile,description);
		}
	}

	
	float animFps = 3;
	float animTime = 0;
	List<IVector2> animationTiles = new List<IVector2>();
	void GetAnimationTiles()
	{
		animationTiles.Clear();
		// collect animation tiles for quicker lookup
		for (int x=0; x<ts.tileMatrix.x; x++)
			for (int y=0; y<ts.tileMatrix.y; y++)
		{
			if (ts.tiles[x][y]>=0 && ts.tiles[x][y]<4)
				animationTiles.Add(new IVector2(x,y));
		}
	}
		
	// Update is called once per frame
	void Update () {
		
		// make sure orthello is initialized and all containers are ready to go
		if (!OT.isValid || !OT.ContainersReady())
			return;
		
		if (!initialized)
		{			
			Initialize();
			initialized = true;
		}
						
		// check if we are hovering 
		if (OT.Over(ts))
		{
			IVector2 tile = ts.mouseTile;
			if (tile!=null)
			{
				TileInfo tileInfo = (ts.objects[tile.x][tile.y] as TileInfo);				
				txtHover.text	= "hovering over tile "+tile+"\n"+
							  "info : "+tileInfo.description;
				int idx = ts.tiles[tile.x][tile.y]; 
				if (idx!=-1 && tileInfo.sprite==null)
				{
					// lets create a hover sprite; ( get it from object pool )
					OTSprite sprite = OT.CreateSprite("tile");
					// add it as a child to the tiels sprite on the right position
					sprite.position = ts.WorldPosition(tile);
					sprite.frameIndex = idx;
					// set cross reference between sprite and tileInfo
					tileInfo.sprite = sprite;
					sprite.data = tileInfo;
					// tween alpha en size
					scrollingSprites.Add(sprite);
					new OTTween(sprite,1.5f,OTEasing.SineOut).
						Tween("size",new Vector2(100,100)).
						Tween("alpha",0).
						onTweenFinish = delegate (OTTween tween)
						{
							sprite = (tween.target as OTSprite);
							// remove cross reference
							(sprite.data as TileInfo).sprite = null;
							scrollingSprites.Remove(sprite);
							// put sprite back into the object pool
							OT.DestroyObject(sprite);						
						};
						
				}							
			}
		}	
		
		// increment position until we scrolled a row
		Vector2 dd = (Time.deltaTime * (float)scrollPxPerSec) * new Vector2(0,1);
		dY += dd;
				
		if (dY.y >= ts.tileSize.y)
		{
			// next row;
			dY.y -= ts.tileSize.y;
			// scroll tiles one row up and put the top row back at the bottom
			ts.Scroll(new IVector2(0,1),true);			
			// collect animation tiles for quicker lookup
			GetAnimationTiles();									
		}
		//adjust alpha of bottom and top row
		float dA = (dY.y/ts.tileSize.y);
		ts.Alpha(tsBottomRow,dA);
		ts.Alpha(tsTopRow,1f-dA);
		// set tiles sprite position
		ts.position = psStart + dY;
		foreach (OTSprite sprite in scrollingSprites)
			sprite.position += dd;
			
		// animate the tiles by changing the frameIndex when next frame should be shown
		animTime+=Time.deltaTime;
		if (animTime>=1/animFps)
		{
			animTime -= 1/animFps;
			// loop tiles to animate and afjust the tile frame index
			for (int i=0; i<animationTiles.Count; i++)
			{
				IVector2 tile = animationTiles[i];
				ts.tiles[tile.x][tile.y]++;
				if (ts.tiles[tile.x][tile.y]==4)
					ts.tiles[tile.x][tile.y] = 0;
			}
			ts.Repaint(animationTiles.ToArray());			
		}
		
		
						
	}
}

/// <summary>
/// Tile info class to accompany a tile so objects[x][y] belongs to tiles[x][y]
/// </summary>
class TileInfo
{
	public string description;
	public IVector2 tile;
	public OTSprite sprite;
	
	public TileInfo(IVector2 tile, string description)
	{
		this.description = description;
		this.tile = tile;
	}
}
	

