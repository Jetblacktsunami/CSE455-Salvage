using UnityEngine;
using System.Collections;

public class basicBullet : MonoBehaviour 
{
	private BulletInfo bulInfo; 
	
	void Start () 
	{
		bulInfo = gameObject.GetComponent<BulletInfo>();	
	}

	void Update()
	{
		transform.position += new Vector3(1.0f * Mathf.Cos(bulInfo.travelAngle * Mathf.Deg2Rad), 1.0f * Mathf.Sin(bulInfo.travelAngle * Mathf.Deg2Rad), 0) * Time.deltaTime * bulInfo.travelSpeed;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Asteroid")
		{
			other.gameObject.GetComponent<Asteroid>().Damage(bulInfo.damageRate);
		}
		else if(other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyInfo>().ApplyDamage(bulInfo.damageRate);
		}

		Destroy (gameObject);
	}
}
