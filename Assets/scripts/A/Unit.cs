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

    int counter = 0;
    public float shootingTime = 3;
    public float _shootingTimer = 1;

    public GameObject cube;
    public int numberOfCubes;
    public int min, max;
    public int once = 0;

    public static Transform targetPos;


    Quaternion initialRot;

    Vector3 oldPos;

    void Start()
    {
        print("start called");
        PlaceCubes();
        PathManager.RequestPath(transform.position, target.position, OnPathFound);
        controller = GetComponent<TankController>();
      //  transform.Rotate(new Vector3(0, 0, 0));
     //   initialRot = transform.rotation;
        print(transform);
    }

    void Update()
    {
        if (once == 0)
        {
         //   transform.Rotate(new Vector3(0, 0, 0));
         //   initialRot = transform.rotation;
         //   once = 1;
         //   print("called once");
        }
      
        //PlaceCubes();
        if (counter == 100)
        {
           // initialRot = transform.rotation;
            PathManager.RequestPath(transform.position, target.position, OnPathFound);
            PlaceCubes();
            counter = 0;
        }
        counter++;
      //  print("counter="+counter);

       
        _shootingTimer -= Time.deltaTime;

        if (_shootingTimer <= 0)
        {
            _shootingTimer = shootingTime;
            controller.Shoot();
        }

        var delta = (target.position - transform.position) / Time.deltaTime;
    //    print("x="+delta.x);
      //  print(delta.y);
     //  print("z="+delta.z);
        oldPos = transform.position;
        targetPos = transform;


        //up
        if (delta.z > 0 && Mathf.Abs(delta.z) > Mathf.Abs(delta.x))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, 90, 0));
        } 
        //down
        if (delta.z < 0 &&Mathf.Abs(delta.z) > Mathf.Abs(delta.x))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, -90, 0));
        }
        //left
        if (delta.x < 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, 0, 0));
        }
        //right
        if (delta.x > 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            transform.rotation = initialRot;
            transform.Rotate(new Vector3(0, 180, 0));
        }

       
    }


    void PlaceCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Instantiate(cube, GeneratedPosition(), Quaternion.identity);
        }
    }
    Vector3 GeneratedPosition()
    {
        float x, y, z;
        x = target.position.x + 20;
        y = 0;
        z = UnityEngine.Random.Range(min, max);
        return new Vector3(x, y, z);
    }

    public void OnPathFound(Vector3[] newPath, bool pathFoundSuccessful)
    {
        if (pathFoundSuccessful)
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
