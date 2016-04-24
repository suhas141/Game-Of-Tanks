using UnityEngine;
using System.Collections;

public class destroyOnDamage : MonoBehaviour {
	
	public GameObject ExplosionFX;
	
	public GUIText mText;
	public bool isGameOver = false;
	
	void Start () {
	
	}
	

	void Update () {
	    
	}
	
	public void TakeDamage(GameObject shooter)
	{	
		if ((shooter.GetComponent<Unit>() != null) && (GetComponent<Unit>() != null))
			return;
		
		if (ExplosionFX != null)
			Instantiate(ExplosionFX, transform.position, Quaternion.identity);
		
		if (isGameOver)
		{
			mText.gameObject.active = true;
			
		}
		
		DestroyObject(this.gameObject);
        //Generator.scube--;
	}
}
