using UnityEngine;
using System.Collections;

public class EnemyInfo : MonoBehaviour 
{
	private float health = 100f;
	private float speed = 10;

	public void ApplyDamage (float amount) 
	{
		health -= amount;
		if(health <= 0)		
		{
			Destroy(gameObject);
		}
	}
}
