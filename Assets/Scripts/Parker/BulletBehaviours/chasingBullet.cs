using UnityEngine;
using System.Collections;

public class chasingBullet : MonoBehaviour 
{
	private BulletInfo bulInfo; 

	// Use this for initialization
	void Start () 
	{
		bulInfo = gameObject.GetComponent<BulletInfo>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector2.MoveTowards(transform.position, bulInfo.target.transform.position ,bulInfo.travelSpeed * Time.deltaTime);
	}
}
