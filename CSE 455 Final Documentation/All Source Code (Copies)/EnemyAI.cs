using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
	private Transform target;
	private Vector2 tempMovement;
	private EnemyInfo eInfo;
	private float startRotation;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Debug.Log("Player");
			target = other.gameObject.transform;
		}
	}

	void Start()
	{
		tempMovement = PlayerInformation.Instance.gameObject.transform.position - transform.position;
		startRotation = GetAngle ();
		gameObject.transform.rotation = Quaternion.AngleAxis(startRotation + 180f, new Vector3(0,0,1.0f));
		eInfo = gameObject.GetComponent<EnemyInfo> ();
	}

	// Update is called once per frame
	void Update () 
	{

		if(target)
		{
			gameObject.transform.rotation = Quaternion.AngleAxis(GetAngle() + 180f, new Vector3(0,0,1.0f));

			if(Mathf.Abs(Vector2.Distance(PlayerInformation.Instance.transform.position,transform.position)) < eInfo.shootingRange)
			{

			}
			else
			{
				transform.position = Vector2.MoveTowards ((Vector2)transform.position, (Vector2)target.position, Time.deltaTime * eInfo.speed );
			}
		}
		else
		{
			Vector3 newPosition = transform.position;
			newPosition.x += eInfo.speed * Time.deltaTime * Mathf.Cos(startRotation * Mathf.Deg2Rad);
			newPosition.y += eInfo.speed * Time.deltaTime * Mathf.Sin(startRotation * Mathf.Deg2Rad);
			transform.position = newPosition;
		}
	}

	public float GetAngle()
	{
		Vector2 position =  PlayerInformation.Instance.gameObject.transform.position - gameObject.transform.position;
		float theta = Mathf.Atan (position.y / position.x) * Mathf.Rad2Deg;
		
		//return theta;
		if(position.x < 0)
		{
			if(position.y >= 0.0f)
			{
				theta += 180f;
			}
			else
			{
				theta += 180f;
			}
		}
		else if(position.x > 0)
		{
			if(position.y >= 0.0f)
			{
				//theta is equal to itself;
			}
			else
			{
				theta += 360f;
			}
		}
		else
		{
			theta = 0;
		}
		
		return theta;
	}

}