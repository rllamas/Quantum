using UnityEngine;
using FarseerPhysics.Collision;
using System.Collections;
using tk2dRuntime.TileMap;
using Microsoft.Xna.Framework;

public class GenerateTilemapColliders : MonoBehaviour {
	
	private tk2dTileMap tilemap;
	private tk2dSpriteDefinition [] spriteDefinitions;
	
	/* Size of a tile in Unity coordinates. */
	private float tileWidth;
	private float tileHeight;
	
	
	// Use this for initialization
	void Start () {
		tilemap = GetComponent<tk2dTileMap>();
		spriteDefinitions = tilemap.SpriteCollectionInst.spriteDefinitions;
		
		tileWidth = tilemap.data.tileSize.x;
		tileHeight = tilemap.data.tileSize.y;
		
		GenerateColliders();
	}
	
	
	
	
	void GenerateColliders() {
		
			int numberOfLayers = tilemap.Layers.Length;
			int numberOfRows = tilemap.height;
			int numberOfColumns = tilemap.width;
		
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
			
				GameObject currentLayerContainer = new GameObject();
				currentLayerContainer.name = "layer" + layerID;
				currentLayerContainer.transform.position = boxCollidersContainer.transform.position;
				currentLayerContainer.transform.parent = boxCollidersContainer.transform;
			
				/* Ignore layer if it's empty or set to not generate colliders. */
				if (currentLayer.IsEmpty || !tilemap.data.Layers[layerID].generateCollider) {
					continue;
				}
	
				/* For every row... */
				for (int cellY = 0; cellY < numberOfRows; ++cellY) {
					
					/* For every column... */
					for (int cellX = 0; cellX < numberOfColumns; ++cellX) {
					
						Vector3 newBoxColliderPosition = new Vector3(
							boxCollidersContainer.transform.position.x + cellX * tileWidth,
							boxCollidersContainer.transform.position.y + cellY * tileHeight,
							boxCollidersContainer.transform.position.z
						);
					
						GameObject newColliderObject = 
							CreateBoxCollider(newBoxColliderPosition, "boxCollider" + "x" + cellX + "y" + cellY);
					
						newColliderObject.transform.parent = currentLayerContainer.transform;
					}

				}
			}
		
	}
	
	
	
	
	/* Creates a new GameObject with an attached Farseer Box Collider centered at position. */
	GameObject CreateBoxCollider(Vector3 position, string newGameObjectName) {
		
		
			/* Generate new GameObject with Box Collider to return. */
			GameObject newColliderGameObject = new GameObject();
			newColliderGameObject.name = newGameObjectName;
			newColliderGameObject.transform.position = position;
			newColliderGameObject.AddComponent<FSShapeComponent>();
		
			FSShapeComponent boxCollider = newColliderGameObject.GetComponent<FSShapeComponent>();
			boxCollider.SType = FarseerPhysics.Collision.Shapes.ShapeType.Polygon;
		
		
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
			pointBottomLeft.name = newGameObjectName + "_pointBottomLeft";
			pointBottomLeft.transform.position = positionBottomLeft;
			pointBottomLeft.transform.parent = newColliderGameObject.transform;
		
			GameObject pointBottomRight = new GameObject();
			pointBottomRight.name = newGameObjectName + "_pointBottomRight";
			pointBottomRight.transform.position = positionBottomRight;
			pointBottomRight.transform.parent = newColliderGameObject.transform;
		
			GameObject pointTopRight = new GameObject();
			pointTopRight.name = newGameObjectName + "_pointTopRight";
			pointTopRight.transform.position = positionTopRight;
			pointTopRight.transform.parent = newColliderGameObject.transform;
		
			GameObject pointTopLeft = new GameObject();
			pointTopLeft.name = newGameObjectName + "_pointTopLeft";
			pointTopLeft.transform.position = positionTopLeft;
			pointTopLeft.transform.parent = newColliderGameObject.transform;
		
		
			boxCollider.PolygonPoints = new Transform [] {
				pointBottomLeft.transform,
				pointBottomRight.transform,
				pointTopRight.transform,
				pointTopLeft.transform
			};
		
		
		return newColliderGameObject;
	
	}
	
}
