using UnityEngine;
using FarseerPhysics.Collision;
using System.Collections;
using tk2dRuntime.TileMap;


public class GenerateTilemapColliders : MonoBehaviour {
	
	/* The tilemap of the GameObject that this script is attached to. */
	private tk2dTileMap tilemap;
	
	/* Sprite Definitions of the tilemap. */
	private tk2dSpriteDefinition [] spriteDefinitions;
	
	/* Size of a tile in Unity coordinates. */
	private float tileWidth;
	private float tileHeight;
	
	/* Maximum number of tiles in a chunk of the tilemap. */
	private int maxTilesY;
	private int maxTilesX;

	
	
	
	/* This is called first in the Unity execution order of events, adding new Farseer colliders
	 * to the tilemap before the Farseer physics engine begins. */
	void Awake () {
		
		tilemap = GetComponent<tk2dTileMap>();
		spriteDefinitions = tilemap.SpriteCollectionInst.spriteDefinitions;
		
		tileWidth = tilemap.data.tileSize.x;
		tileHeight = tilemap.data.tileSize.y;
		
		maxTilesY = Mathf.Min(tilemap.partitionSizeY, tilemap.height);
		maxTilesX = Mathf.Min(tilemap.partitionSizeX, tilemap.width);
		
		GenerateColliders();
	
	}
	
	
	
	
	/* Creates Farseer Box Colliders for all tiles in this tilemap with the User Defined collision types
	 * set for their sprites. */
	void GenerateColliders() {
		
		int numberOfLayers = tilemap.Layers.Length;

		/* Create an container gameObject to parent all layers of box colliders.
		 * This helps dramatically with scene organization. */
		GameObject boxCollidersContainer = new GameObject();
		boxCollidersContainer.name = "Box Colliders Container";
		boxCollidersContainer.transform.position = new Vector3(
			tilemap.transform.position.x,
			tilemap.transform.position.y,
			tilemap.transform.position.z
		);
	
		/* For every layer... */
		for (int layerID = 0; layerID < numberOfLayers; ++layerID) {
			
			
			Layer currentLayer = tilemap.Layers[layerID];
			
			
			/* Add a layer gameObject to parent all box colliders in this layer.
			 * This helps dramatically with scene organization. */
			GameObject currentLayerContainer = new GameObject();
			currentLayerContainer.name = "Layer " + layerID;
			currentLayerContainer.transform.position = boxCollidersContainer.transform.position;
			currentLayerContainer.transform.parent = boxCollidersContainer.transform;
		
			
			/* Ignore layer if it's empty or set to not generate colliders. */
			if (currentLayer.IsEmpty || !tilemap.data.Layers[layerID].generateCollider) {
				continue;
			}
			

			/* For every row of sprite chunks... */
			for (int chunkY = 0; chunkY < currentLayer.numRows; ++chunkY) {
			
				/* For every column of sprite chunks... */
				for (int chunkX = 0; chunkX < currentLayer.numColumns; ++chunkX) {
					
					SpriteChunk chunk = currentLayer.GetChunk(chunkX, chunkY);
					
					/* If there's nothing in this chunk, then disregard it. */
					if (chunk.IsEmpty) {
						continue;
					}
						
					/* Add a chunk gameObject to parent all box colliders in this chunk.
					 * This helps dramatically with scene organization. */
					GameObject currentChunkContainer = new GameObject();
					currentChunkContainer.name = "Chunk " + chunkX + " " + chunkY;
					currentChunkContainer.transform.position = boxCollidersContainer.transform.position;
					currentChunkContainer.transform.parent = currentLayerContainer.transform;
			
					GenerateCollidersForChunk(chunk, currentChunkContainer);		
				
				}
			
			} 
		
		}
	} // GenerateColliders
	
	
	
	
	/* Creates Farseer Box Colliders for tiles in chunk and sets them as children of container. */
	void GenerateCollidersForChunk(SpriteChunk chunk, GameObject container) {
		
		
		int spriteCount = spriteDefinitions.Length;
		int[] chunkData = chunk.spriteIds;
		
		
		/* For every row in the chunk... */
		for (int y = 0; y < maxTilesY; ++y) {
		
			
			/* For every column in the chunk... */
			for (int x = 0; x < maxTilesX; ++x) {
	
				
				int spriteId = chunkData[y * tilemap.partitionSizeX + x];
				int spriteIdx = BuilderUtil.GetTileFromRawTile(spriteId);
			
				
				/* Handle if tile has no valid sprite. */
				if (spriteIdx < 0 || spriteIdx >= spriteCount) {
					continue;
				}
				else if (tilemap.data.tilePrefabs[spriteIdx]) {
					continue;
				}
				
				
				tk2dSpriteDefinition spriteData = spriteDefinitions[spriteIdx];
			
				
				/* If the tile is marked as User Defined, then add a box collider. */
				if (spriteData.colliderType == tk2dSpriteDefinition.ColliderType.Unset) {
			
					Vector3 newBoxColliderPosition = new Vector3(
						chunk.gameObject.transform.position.x + x * tileWidth,
						chunk.gameObject.transform.position.y + y * tileHeight,
						chunk.gameObject.transform.position.z
					);
			

					GameObject newColliderObject = CreateBoxCollider(newBoxColliderPosition, "Box Collider " + x + " " + y);
				
					newColliderObject.transform.parent = container.transform;
				}
			}	
		}
		
		
	} // End method GenerateCollidersForChunk
	
	
	
	
	/* Creates a new GameObject with an attached Farseer Box Collider centered at position. */
	GameObject CreateBoxCollider(Vector3 position, string newGameObjectName) {
		
		
		/* Generate new GameObject with Box Collider to return. */
		GameObject newColliderGameObject = new GameObject();
		newColliderGameObject.name = newGameObjectName;
		newColliderGameObject.transform.position = position;

		newColliderGameObject.AddComponent<FSWorldComponent>();
		newColliderGameObject.AddComponent<FSBodyComponent>();
		newColliderGameObject.AddComponent<FSShapeComponent>();

	
		FSBodyComponent body = newColliderGameObject.GetComponent<FSBodyComponent>();
		body.Type = FarseerPhysics.Dynamics.BodyType.Static;
		
		FSShapeComponent boxCollider = newColliderGameObject.GetComponent<FSShapeComponent>();
		boxCollider.SType = FarseerPhysics.Collision.Shapes.ShapeType.Polygon;
		boxCollider.UseUnityCollider = false;
	

		/* Generate the positions of the points of the Box Collider. */
		Vector3 positionBottomLeft = new Vector3(
			position.x - 0.5f*tileWidth,
			position.y - 0.5f*tileHeight,
			position.z
		);

		Vector3 positionBottomRight = new Vector3(
			position.x + 0.5f*tileWidth,
			position.y - 0.5f*tileHeight,
			position.z
		);

		Vector3 positionTopRight = new Vector3(
			position.x + 0.5f*tileWidth,
			position.y + 0.5f*tileHeight,
			position.z
		);

		Vector3 positionTopLeft = new Vector3(
			position.x - 0.5f*tileWidth,
			position.y + 0.5f*tileHeight,
			position.z
		);
		
		
		/* Generate GameObjects to be used as the points of the new Box Collider. */
		GameObject pointBottomLeft = new GameObject();
		pointBottomLeft.name = newGameObjectName + " Bottom Left Point";
		pointBottomLeft.transform.position = positionBottomLeft;
		pointBottomLeft.transform.parent = newColliderGameObject.transform;
		
		GameObject pointBottomRight = new GameObject();
		pointBottomRight.name = newGameObjectName + " Bottom Right Point";
		pointBottomRight.transform.position = positionBottomRight;
		pointBottomRight.transform.parent = newColliderGameObject.transform;

		GameObject pointTopRight = new GameObject();
		pointTopRight.name = newGameObjectName + " Top Right Point";
		pointTopRight.transform.position = positionTopRight;
		pointTopRight.transform.parent = newColliderGameObject.transform;

		GameObject pointTopLeft = new GameObject();
		pointTopLeft.name = newGameObjectName + " Top Left Point";
		pointTopLeft.transform.position = positionTopLeft;
		pointTopLeft.transform.parent = newColliderGameObject.transform;

	
		boxCollider.PolygonPoints = new Transform [] {
			pointBottomLeft.transform,
			pointBottomRight.transform,
			pointTopRight.transform,
			pointTopLeft.transform,
		};
		
		
		return newColliderGameObject;
	
	} // End method CreateBoxCollider
	
	
	
	
} // End class GenerateTilemapColliders
