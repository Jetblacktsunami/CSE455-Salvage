using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	public float movespeed;
	private bool contact;
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerShip")
		{
			Debug.Log("player has entered");
		}
		contact = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if(contact == true)
		{
			transform.position = Vector2.MoveTowards ((Vector2)transform.position, (Vector2)target.position, movespeed * Time.deltaTime);
		}
	}
}