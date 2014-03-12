using UnityEngine;
using System.Collections;

public class beamRifle : MonoBehaviour 
{
	private BulletInfo bulInfo;
	private LayerMask mask = ~(1 << 15);
	private enum ResizeEvent { standardSize, hitSize, standby } 
	ResizeEvent resize = ResizeEvent.standardSize;
	// Use this for initialization
	void Start () 
	{
		bulInfo = gameObject.GetComponent<BulletInfo>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit2D hit = Physics2D.Raycast (PlayerInformation.Instance.gameObject.transform.position, new Vector2 (Mathf.Cos (bulInfo.travelAngle * Mathf.Deg2Rad), Mathf.Sin (bulInfo.travelAngle * Mathf.Deg2Rad)), 74f , mask, -5, 5);
		if(hit)
		{
			if(hit.collider.tag == "Asteroid")
			{
				hit.collider.gameObject.GetComponent<Asteroid>().Damage(bulInfo.damageRate * Time.deltaTime);
				gameObject.transform.localScale = new Vector3( Vector2.Distance(PlayerInformation.Instance.gameObject.transform.position, hit.collider.gameObject.transform.position) , 2.0f ,1.0f);
				resize = ResizeEvent.hitSize;
			}
			else if(hit.collider.tag == "Enemy")
			{
				gameObject.GetComponent<EnemyInfo>().ApplyDamage(bulInfo.damageRate * Time.deltaTime);
				gameObject.transform.localScale = new Vector3( Vector2.Distance(PlayerInformation.Instance.gameObject.transform.position, hit.collider.gameObject.transform.position), 2.0f ,1.0f);
				resize = ResizeEvent.hitSize;
			}
		}

		if(resize != ResizeEvent.hitSize && resize != ResizeEvent.standby)
		{
			gameObject.transform.localScale = new Vector3( 75f , 2.0f ,1.0f);
			resize = ResizeEvent.standby;
		}
		else if(resize == ResizeEvent.hitSize)
		{
			resize = ResizeEvent.standardSize;
		}
	}
}
