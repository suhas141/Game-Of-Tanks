using UnityEngine;
using System.Collections.Generic;

public class HighlitedM {
	public Vector2 Location { get; set; }
	public ProceduralTiles Terrain { get; set; }
	

	public static HighlitedM near(IEnumerable<HighlitedM> HighlitedMs, Vector2 location, int UTerrain)
	{
		HighlitedM HighTileSel = null;
		float near = float.MaxValue;
		foreach (var HighlitedM in HighlitedMs) {
			float rand = RandomTile.Percent(
				(int)(HighlitedM.Location.x + location.x),
				(int)(HighlitedM.Location.y + location.y),
				UTerrain) * 8;
			float Away = Vector2.Distance(location, HighlitedM.Location);
			Away -= rand;
			if (Away < near)
			{
				near = Away;
				HighTileSel = HighlitedM;
			}
		}
		return HighTileSel;
	}

    public static IEnumerable<HighlitedM> GetHighlitedMs(float x, float y, int UTerrain, ProceduralTiles[] terrains, float RockFound)
    {
        var HighlitedMs = new HighlitedM[9];
        x = (int)x >> 4;
        y = (int)y >> 4;
        int HighlitedMIndex = 0;
        for (int iX = -1; iX < 2; iX++)
        {
            for (int iY = -1; iY < 2; iY++)
            {
                var terrain = terrains[RandomTile.Range(x + iX, y + iY, UTerrain, terrains.Length)];

                float mass = RandomTile.Percent(x + iX, y + iY, UTerrain) * 8 + 2;
                HighlitedMs[HighlitedMIndex++] = new HighlitedM()
                {
                    Terrain = terrain,

                    Location = new Vector2((int)(x + iX) << 4, (int)(y + iY) << 4)
                };
            }
        }
        return HighlitedMs;
    }







}
