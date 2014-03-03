using UnityEngine;
using System.Collections;

public class random_rotator : MonoBehaviour 
{
	public float tumble;

	void start()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
