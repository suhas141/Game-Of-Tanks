using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

    public int min, max;
 //   public Transform target;
    public int count = 2;
	public float time = 8;
	float timer;
	public bool onStart = true;
	public GameObject prefab;
	public GameObject FX;
    public int oldpos;
	// Use this for initialization
	void Start () {
       
        timer = time;
		if (onStart)
			Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		
		if (timer <= 0)
		{
			timer = time;
			if (count <= 0)
			{
				this.enabled = false;
				return;
			}
			Spawn();
		}
	}

    Vector3 GeneratedPosition()
    {
        print("target"+Unit.targetPos.transform.position);
        float x, y, z;
        x = UnityEngine.Random.Range(min, max);
        y = 0;
        z = UnityEngine.Random.Range(min, max);
        return new Vector3(x, y, z);
    }

    void Spawn()
	{
		count--;

        Vector3 temp = GeneratedPosition();
        Instantiate(prefab, temp, Quaternion.identity);
      
        Instantiate(FX, temp, Quaternion.identity);
	}
}
