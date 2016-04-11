using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    [HideInInspector]
    public TankController controller;

    public Transform target;
    public int speed = 20;
    Vector3[] path;
    int targetIndex;
   

    public float shootingTime = 3;
    public float _shootingTimer = 1;

    Quaternion initialRot;

    Vector3 oldPos;

    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        controller = GetComponent<TankController>();
        initialRot = transform.rotation;
    }

    void Update()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        _shootingTimer -= Time.deltaTime;

        if (_shootingTimer <= 0 /*&& !Bullet.isBulletAlive*/)
        {
            _shootingTimer = shootingTime;
            controller.Shoot();
        }

        var delta = (target.position - transform.position) / Time.deltaTime;
    //    print("x="+delta.x);
      //  print(delta.y);
     //  print("z="+delta.z);
        oldPos = transform.position;


        //up
        if (delta.z > 0 && Mathf.Abs(delta.z) > Mathf.Abs(delta.x))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, 180, 0));
        } 
        //down
        if (delta.z < 0 &&Mathf.Abs(delta.z) > Mathf.Abs(delta.x))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, 0, 0));
        }
        //left
        if (delta.x < 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, 90, 0));
        }
        //right
        if (delta.x > 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, -90, 0));
        }

       
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
