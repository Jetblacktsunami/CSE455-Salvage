using UnityEngine;
using System.Collections;

public class beamRifle : MonoBehaviour 
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
		RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(Mathf.Cos(bulInfo.travelAngle * Mathf.Deg2Rad), Mathf.Sin(bulInfo.travelAngle * Mathf.Deg2Rad)),Screen.width);
		if(hit)
		{
			gameObject.transform.localScale = new Vector3( Vector2.Distance(gameObject.transform.position, hit.collider.gameObject.transform.position) , 1.0f ,1.0f);
		}
	}
}
