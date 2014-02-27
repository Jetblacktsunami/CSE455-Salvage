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
}
