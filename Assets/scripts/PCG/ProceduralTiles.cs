using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ProceduralTiles{
	
	public Sprite[] Tiles;
	

	public Sprite TileFetch(float x, float y, int UTerrain)
	{
		return Tiles[RandomTile.Range (x, y, UTerrain, Tiles.Length)];
	}
}
