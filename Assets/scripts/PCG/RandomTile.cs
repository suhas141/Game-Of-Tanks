
// Taking x, y and UTerrain and randomizing the cordinate number to generate the random tiles using binary operator 
public class RandomTile
{
   
    public static int Range(float x, float y, int UTerrain, int range)
	{
		return Range ((int)x, (int)y, UTerrain, range);
	}

	public static int Range(int x, int y, int UTerrain, int range)
	{
		uint hash = (uint)UTerrain;
		hash ^= (uint)x;
		hash *= 0x51d7348d;
		hash ^= 0x85dbdda2;
		hash = (hash << 16) ^ (hash >> 16);
		hash *= 0x7588f287;
		hash ^= (uint)y;
		hash *= 0x487a5559;
		hash ^= 0x64887219;
		hash = (hash << 16) ^ (hash >> 16);
		hash *= 0x63288691;
		return (int)(hash % range);
    }
    public static float Percent(float x, float y, int UTerrain)
    {
        return Percent((int)x, (int)y, UTerrain);
    }
    public static float Percent(int x, int y, int UTerrain)
    {
        return (float)Range(x, y, UTerrain, int.MaxValue) / (float)int.MaxValue;
    }
}
