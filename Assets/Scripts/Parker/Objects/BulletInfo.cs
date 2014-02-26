using UnityEngine;
using System.Collections;

public class BulletInfo : MonoBehaviour 
{
	public float fireRate = 1f;
	public float travelSpeed = 10f;

	public float travelAngle = 0f;

	void Start()
	{
		gameObject.transform.rotation = Quaternion.AngleAxis(travelAngle, new Vector3(0,0,1.0f));
	}

	void Update()
	{
		transform.position += new Vector3(1.0f * Mathf.Cos(travelAngle * Mathf.Deg2Rad), 1.0f * Mathf.Sin(travelAngle * Mathf.Deg2Rad), 0) * Time.deltaTime * travelSpeed;
	}
}
