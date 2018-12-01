using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator2 : MonoBehaviour {

	public int mapSizeX = 6;
	public int mapSizeY = 8;
	
	public List<MapTile2> tiles = new List<MapTile2>();
	public Transform tilePrefab;


	private void Start() {
		GenerateMap();
	}

	public void ResetMap() {
		for (int i = 0; i < tiles.Count; i++) {
			tiles[i].Reset();
		}
	}

	private int TilePosition(int x, int y) {
		return x + y * mapSizeX;
	}

	public MapTile2 GetTile(int x, int y) {
		if (x < 0 || y < 0 || x >= mapSizeX || y >= mapSizeY)
			return null;

		return tiles[TilePosition(x, y)];
	}

	public void GenerateMap() {
		for (int j = 0; j < mapSizeY; j++) {
			for (int i = 0; i < mapSizeX; i++) {
				Transform tile = Instantiate(tilePrefab);
				tile.position = new Vector3(i,j,0);
				tile.parent = transform;

				MapTile2 tempTile = tile.GetComponent<MapTile2>();
				tempTile.posx = i;
				tempTile.posy = j;

				tiles.Add(tempTile);
			}
		}
	}

}
