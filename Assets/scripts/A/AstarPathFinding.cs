using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class AstarPathFinding : MonoBehaviour
{
    Grid grid;
    PathManager pathManager;
    
    void Awake()
    {
        pathManager = GetComponent<PathManager>();
        grid = GetComponent<Grid>();
    }


    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        StartCoroutine(FindPath(startPosition, targetPosition));
    }

    IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
    {

        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypointsCoord = new Vector3[0];
        bool pathFoundSuccess = false;

        Node nStart = grid.NodeWorldPointCoord(startPosition);
        Node nTarget = grid.NodeWorldPointCoord(targetPosition);

        if (nStart.walkableArea && nTarget.walkableArea)
        {
            Heap<Node> openSetItems = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSetItems = new HashSet<Node>();
            openSetItems.Add(nStart);

            while (openSetItems.Count > 0)
            {
                Node nCurrent = openSetItems.RemoveFirst();
                closedSetItems.Add(nCurrent);

                if (nCurrent == nTarget)
                {
                    sw.Stop();
                  //  print("Path found in  " + sw.ElapsedMilliseconds + " ms");
                    pathFoundSuccess = true;
                    break;
                }

                foreach (Node neighbourNode in grid.GetNeighbourNodes(nCurrent))
                {
                    if (!neighbourNode.walkableArea || closedSetItems.Contains(neighbourNode))
                    {
                        continue;
                    }

                    int newCosttoNeighbor = nCurrent.gCostAstar + getDistance(nCurrent, neighbourNode);
                    if (newCosttoNeighbor < neighbourNode.gCostAstar || !openSetItems.Contains(neighbourNode))
                    {
                        neighbourNode.gCostAstar = newCosttoNeighbor;
                        neighbourNode.hCostAstar = getDistance(neighbourNode, nTarget);
                        neighbourNode.root = nCurrent;

                        if (!openSetItems.Contains(neighbourNode))
                            openSetItems.Add(neighbourNode);
                    }
                }
            }
        }
        yield return null;
        if (pathFoundSuccess)
        {
            waypointsCoord = RetracePath(nStart, nTarget);
        }
        pathManager.FinishedProcessingPath(waypointsCoord, pathFoundSuccess);

    }

    Vector3[] RetracePath(Node nStart, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node nCurrent = endNode;

        while (nCurrent != nStart)
        {
            path.Add(nCurrent);
            nCurrent = nCurrent.root;
        }
        Vector3[] waypointsCoord = SimplifyPath(path);
        Array.Reverse(waypointsCoord);
        return waypointsCoord;

    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypointsCoord = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridXCoord - path[i].gridXCoord, path[i - 1].gridYCoord - path[i].gridYCoord);
            if (directionNew != directionOld)
            {
                waypointsCoord.Add(path[i].worldCoord);
            }
            directionOld = directionNew;
        }
        return waypointsCoord.ToArray();
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridXCoord - nodeB.gridXCoord);
        int dstY = Mathf.Abs(nodeA.gridYCoord - nodeB.gridYCoord);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }


}
