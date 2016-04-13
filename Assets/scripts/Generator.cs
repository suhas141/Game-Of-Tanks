using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{

    public GameObject cube;
    public GameObject grass;

    public  int numberOfCubes;
    public static int scube;

    public int numberOfGrass;
    public static int sGrass;

    public int min, max;

    void Start()
    {
       
        PlaceCubes();
        PlaceGrass();
    }

    void PlaceGrass()
    {
        sGrass = numberOfGrass;
        for (int i = 0; i < numberOfGrass; i++)
        {
            Instantiate(grass, GeneratedPosition(), Quaternion.identity);
        }
    }

    void PlaceCubes()
    {
        scube = numberOfCubes;
        for (int i = 0; i < numberOfCubes; i++)
        {
            Instantiate(cube, GeneratedPosition(), Quaternion.identity);
        }
    }
    Vector3 GeneratedPosition()
    {
        int x, y, z;
        x = UnityEngine.Random.Range(min, max);
        y = 0;
        z = UnityEngine.Random.Range(min, max);
        return new Vector3(x, y, z);
    }
 void Update()
    {
      //  PlaceCubes();
        if (scube == 0)
        {
            numberOfCubes--;
            PlaceCubes();
           
        }

        if (sGrass == 0)
        {
            numberOfGrass--;
            PlaceGrass();

        }
    }
}
