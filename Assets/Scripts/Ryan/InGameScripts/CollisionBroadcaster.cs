using UnityEngine;
using System.Collections;
using System;

public class CollisionBroadcaster : MonoBehaviour 
{
	public static Action Collided;
	public float collisions = 0.0f;

	//1 damage = 1/totalHP

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Enemy")
		{
			collisions += 1;
			Debug.Log("Contact: " + collisions);
			Collided();
			//gameObject.GetComponent<UISlider>().value -= 1/hp;
		}
	}
}
