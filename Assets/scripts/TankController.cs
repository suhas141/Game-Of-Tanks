using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour {
	
	public enum goDirection{ up, down, left, right, stay};
	public goDirection direction;
	
	public float speed;
	public GameObject mBullet;
	bool shouldGo = false;
	
	
	
	Quaternion initialRot;
	// Use this for initialization
	void Start () 
	{
		initialRot = transform.rotation;
	}
	
	void Update()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero; 
			
		if (direction == goDirection.right)
		{
			transform.rotation = initialRot;
			transform.Rotate(new Vector3(0, 90, 0));
			
			GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
		}else if (direction == goDirection.left)
		{
			transform.rotation = initialRot;
			transform.Rotate(new Vector3(0, -90, 0));
			
			
			GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
		}else if (direction == goDirection.up)
		{
			transform.rotation = initialRot;
			transform.Rotate(new Vector3(0, 0, 0));
			
			GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
		}else if (direction == goDirection.down)
		{
			transform.rotation = initialRot;
			transform.Rotate(new Vector3(0, 180, 0));
			
			GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
		}
			
		// SHOOT
		
		
		if (!GetComponent<Animation>().IsPlaying("shoot") && !GetComponent<Animation>().IsPlaying("idle"))// && animation["shoot"].time >= animation["shoot"].length)
		{
			GetComponent<Animation>().Play("idle");
		}
	}
	
	public void Shoot()
	{
		GetComponent<Animation>().Play("shoot");
		GameObject bullet = Instantiate(mBullet, transform.position + -transform.right * 7, this.transform.rotation) as GameObject;
		(bullet.GetComponent<Bullet>() as Bullet).owner = this.gameObject;
	}
}
