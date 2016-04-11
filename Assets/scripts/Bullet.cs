using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	//public static bool isBulletAlive = false;
	public float speed;
	public GameObject owner;
	
	// Use this for initialization
	void Start () {
		//isBulletAlive = true;
		GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnCollisionEnter(Collision col)
	{
		//isBulletAlive = false;
		col.gameObject.SendMessage("TakeDamage", this.owner, SendMessageOptions.DontRequireReceiver);
		
		DestroyObject(gameObject);
	}
}
