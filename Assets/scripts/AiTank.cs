using UnityEngine;
using System.Collections;

public class AiTank : MonoBehaviour {
	
	[HideInInspector]
	public TankController controller;
	
	public float directionSwitcherTime = 3;
	public float shootingTime = 1;
	
	public float _directionSwitcherTimer = 3;
	public float _shootingTimer = 1;
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<TankController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		_directionSwitcherTimer -= Time.deltaTime;
		_shootingTimer -= Time.deltaTime;
		
		if (_directionSwitcherTimer <= 0)
		{
			_directionSwitcherTimer = directionSwitcherTime;
			
			controller.direction = (TankController.goDirection) Random.Range(0, 4);
		}
		
		if (_shootingTimer <= 0 /*&& !Bullet.isBulletAlive*/)
		{
			_shootingTimer = shootingTime;
			controller.Shoot();
		}
	}
}
