using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathManager : MonoBehaviour
{

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathManager instance;
    AstarPathFinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<AstarPathFinding>();
    }

    public static void RequestPath(Vector3 pathStartPos, Vector3 pathEndPos, Action<Vector3[], bool> callback)
    {
        PathRequest newPath = new PathRequest(pathStartPos, pathEndPos, callback);
        instance.pathRequestQueue.Enqueue(newPath);
        instance.TryOtherPath();
    }



    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryOtherPath();
    }

    void TryOtherPath()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStartPos, currentPathRequest.pathEndPos);
        }
    }

    struct PathRequest
    {
        public Vector3 pathStartPos;
        public Vector3 pathEndPos;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStartPos = _start;
            pathEndPos = _end;
            callback = _callback;
        }

    }
}
