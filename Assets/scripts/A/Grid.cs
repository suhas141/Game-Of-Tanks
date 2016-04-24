using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public float radiusNode;
    float diameter;

    int gridSizeXCoord;
    int gridSizeYCoord;
    
    public Vector2 worldSize;

    public bool displayGizmosOnGrid;
   
    Node[,] grid;

    public LayerMask unwalkableAreaMask;
    void Awake()
    {
        diameter = radiusNode * 2;
        gridSizeXCoord = Mathf.RoundToInt(worldSize.x / diameter);
        gridSizeYCoord = Mathf.RoundToInt(worldSize.y / diameter);
        generateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeXCoord * gridSizeYCoord;
        }
    }

    void generateGrid()
    {
        grid = new Node[gridSizeXCoord, gridSizeYCoord];
        Vector3 worldBottomLeftCoord = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

        for (int x = 0; x < gridSizeXCoord; x++)
        {
            for (int y = 0; y < gridSizeYCoord; y++)
            {
                Vector3 worldPointCoord = worldBottomLeftCoord + Vector3.right * (x * diameter + radiusNode) + Vector3.forward * (y * diameter + radiusNode);
                bool walkableArea = !(Physics.CheckSphere(worldPointCoord, radiusNode, unwalkableAreaMask));
                grid[x, y] = new Node(walkableArea, worldPointCoord, x, y);
            }
        }
    }



    public Node NodeWorldPointCoord(Vector3 worldCoord)
    {
        float percentXCoord = (worldCoord.x + worldSize.x / 2) / worldSize.x;
        float percentYCoord = (worldCoord.z + worldSize.y / 2) / worldSize.y;
        percentXCoord = Mathf.Clamp01(percentXCoord);
        percentYCoord = Mathf.Clamp01(percentYCoord);
        int x = Mathf.RoundToInt((gridSizeXCoord - 1) * percentXCoord);
        int y = Mathf.RoundToInt((gridSizeYCoord - 1) * percentYCoord);

        return grid[x, y];
    }



    public List<Node> GetNeighbourNodes(Node node)
    {
        List<Node> neighbourNodeNodes = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;


                int checkXCoord = node.gridXCoord + x;
                int checkYCoord = node.gridYCoord + y;

                if (checkXCoord >= 0 && checkXCoord < gridSizeXCoord && checkYCoord >= 0 && checkYCoord < gridSizeYCoord)
                {
                    neighbourNodeNodes.Add(grid[checkXCoord, checkYCoord]);
                }
            }
        }

        return neighbourNodeNodes;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));
        if (grid != null && displayGizmosOnGrid)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkableArea) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldCoord, Vector3.one * (diameter - .1f));
            }
        }
    }
}