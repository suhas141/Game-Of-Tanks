using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PCG : MonoBehaviour
{

    // Time Text object 
    public Text InTime;
    public Text Points;
    private float Initialtime;
    private float InitialPoints;

    //GameObject for generating random  destructible objects in the terrain
    public GameObject cube;
    public int numberOfCubes;
    public static int scube;
    public int min, max;


    //non-destructible objects in the terrain.

    public GameObject grass;
    public int numberOfGrass;
    public static int sGrass;

   //  tile sprites

    public int HTiles = 100;
    public int VTiles = 100;
    public int UTerrain = 3;
    public Transform TankP;
    public float TankPvalue = 5;
    public Vector2 OfstTerrain;
    public ProceduralTiles[] ProceduralTiless;


    private SpriteRenderer[,] CommitR;
    private IEnumerable<HighlitedM> _HighlitedMs;


    public Vector2 WorldToMapPosition(Vector3 worldPosition)
    {
        if (worldPosition.x < 0)
            worldPosition.x--;
        if (worldPosition.y < 0)
            worldPosition.y--;
        return new Vector2((int)(worldPosition.x + OfstTerrain.x), (int)(worldPosition.y + OfstTerrain.y));

    }
    public ProceduralTiles SelectTerrain(float x, float y)
    {
        return HighlitedM.near(_HighlitedMs, new Vector2(x, y), UTerrain).Terrain;
    }

    // for initialting grass
    void PlaceGrass()
    {
        sGrass = numberOfGrass;
        for (int i = 0; i < numberOfGrass; i++)
        {
            Instantiate(grass, GeneratedPosition(), Quaternion.identity);
        }
    }

    // for initiating destroyable cubes 

    void PlaceCubes()
    {
        scube = numberOfCubes;
        for (int i = 0; i < numberOfCubes; i++)
        {
            Instantiate(cube, GeneratedPosition(), Quaternion.identity);
        }
    }


    // Re-drawing the map taking TankP as the referance 

        void DrawTile()
    {

            transform.position = new Vector3((int)TankP.position.x, (int)TankP.position.y, TankP.position.z);
            _HighlitedMs = HighlitedM.GetHighlitedMs(transform.position.x, transform.position.y, UTerrain, ProceduralTiless, 1);
            var Tofset = new Vector3(
                transform.position.x - HTiles / 2,
                transform.position.y - VTiles / 2,
                0);
            for (int x = 0; x < HTiles; x++)
            {
                for (int y = 0; y < VTiles; y++)
                {
                    var spriteRenderer = CommitR[x, y];
                    var terrain = SelectTerrain(
                        Tofset.x + x,
                        Tofset.y + y);
                    spriteRenderer.sprite = terrain.TileFetch(Tofset.x + x,
                                                            Tofset.y + y,
                                                            UTerrain);

                }
            }

        }

    // To initializing the first tile,storing the new postion and  random objects as we load the the game for first time
    void Start()
    {


        Initialtime = Time.time;
        InitialPoints = 0;

            PlaceCubes();
            PlaceGrass();

        
        int sortIndex = 0;
            var Tofset = new Vector3(0 - HTiles / 2, 0, 0 - VTiles / 2);
            CommitR = new SpriteRenderer[HTiles, VTiles];
            for (int x = 0; x < HTiles; x++)
            {
                for (int y = 0; y < VTiles; y++)
                {
                    var tile = new GameObject();
                    tile.transform.position = new Vector3(x, 0, y) + Tofset;
                    var renderer = CommitR[x, y] = tile.AddComponent<SpriteRenderer>();
                    renderer.sortingOrder = sortIndex--;
                    tile.name = "Terrain " + tile.transform.position;
                    tile.transform.Rotate(90, 0, 0);
                    tile.transform.parent = transform;
                }
            }
        DrawTile();
        }


    Vector3 GeneratedPosition()
    {
        int x, y, z;
        x = UnityEngine.Random.Range(min, max);
        y = 0;
        z = UnityEngine.Random.Range(min, max);
        return new Vector3(x, y, z);
    }


    // generation  new tiles, grass and cubes once they are destroyed using the TankP position  

    void Update()
    {
            if (TankPvalue < Vector3.Distance(TankP.position, transform.position))
            {
            DrawTile();

            }


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

        // Time and Points increment

        float d = Time.time - Initialtime;
        string min = ((int)d / 60).ToString();
        string sec = (d % 60).ToString("f0");

        int p = int.Parse(sec);
        int m = int.Parse(min);

        int totalsec = 60 * m + p;


        InTime.text = "Time: " + totalsec + " sec";
       

        int tanksDestroyed = AddScoreOnDestroy.scrores;



        int total = tanksDestroyed * 1000 + m * 600 + p * 10;

        //  if (!destroyOnDamage.isGameOver)
        Points.text = "Score: " + total;



    }
    }

    