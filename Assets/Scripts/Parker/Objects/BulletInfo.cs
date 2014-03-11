using UnityEngine;
using System.Collections;

public class BulletInfo : MonoBehaviour 
{
	public float fireRate = 1f;
	public float travelSpeed = 10f;
	public float travelAngle = 0f;
	public GameObject target;
	public float damageRate = 5.0f;

	void Start()
	{
		gameObject.transform.rotation = Quaternion.AngleAxis(travelAngle, new Vector3(0,0,1.0f));
	}
}
