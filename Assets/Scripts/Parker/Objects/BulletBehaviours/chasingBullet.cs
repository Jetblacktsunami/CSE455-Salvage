using UnityEngine;
using System.Collections;

public class chasingBullet : MonoBehaviour 
{
	private BulletInfo bulInfo; 
	private LayerMask mask = (1 << 16);

	// Use this for initialization
	void Start () 
	{
		bulInfo = gameObject.GetComponent<BulletInfo>();

		Collider2D[] hits = Physics2D.OverlapCircleAll (gameObject.transform.position, WorldGenerator.worldspec.cellLength, mask, -5, 5);

		if(hits.Length > 0)
		{
			bulInfo.target = hits[Random.Range(0, hits.Length - 1)].gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(bulInfo.target)
		{
			transform.position = Vector2.MoveTowards(transform.position, bulInfo.target.transform.position ,bulInfo.travelSpeed * Time.deltaTime);
		}
		else
		{
			transform.position += new Vector3(1.0f * Mathf.Cos(bulInfo.travelAngle * Mathf.Deg2Rad), 1.0f * Mathf.Sin(bulInfo.travelAngle * Mathf.Deg2Rad), 0) * Time.deltaTime * bulInfo.travelSpeed;
		}
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
