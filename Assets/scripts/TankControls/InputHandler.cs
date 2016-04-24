using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {
	
	TankController controller;
	TankController.goDirection direction = TankController.goDirection.stay;
	// Use this for initialization
	void Start () {
		controller = GetComponent<TankController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			direction = TankController.goDirection.right;
		}else if (Input.GetKey(KeyCode.LeftArrow))
		{
			direction = TankController.goDirection.left;
		}else if (Input.GetKey(KeyCode.UpArrow))
		{
			direction = TankController.goDirection.up;
		}else if (Input.GetKey(KeyCode.DownArrow))
		{
			direction = TankController.goDirection.down;
		}else
			direction = TankController.goDirection.stay;
		
		if (Input.GetKeyDown(KeyCode.Space) /*&& !Bullet.isBulletAlive*/)
		{
			controller.Shoot();
		}
	
		controller.direction = direction;
	}
}
