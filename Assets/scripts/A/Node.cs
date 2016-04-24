using UnityEngine;


public class Node : IHeapItem<Node>
{

   //world coordinates
    public int gridXCoord;
    public int gridYCoord;
    //for a star algorithm
    public int gCostAstar;
    public int hCostAstar;
    //root node
    public Node root;
    int heapIndex;
    public bool walkableArea;
    public Vector3 worldCoord;

    public Node(bool _walkableArea, Vector3 _worldPos, int _gridXCoord, int _gridYCoord)
    {
        walkableArea = _walkableArea;
        worldCoord = _worldPos;
        gridXCoord = _gridXCoord;
        gridYCoord = _gridYCoord;
    }

    public int fCostAstar
    {
        get
        {
            return gCostAstar + hCostAstar;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node n)
    {
        int cmp = fCostAstar.CompareTo(n.fCostAstar);
   
        if (cmp == 0)
        {
            cmp = hCostAstar.CompareTo(n.hCostAstar);
        }
        return -cmp;
    }
}