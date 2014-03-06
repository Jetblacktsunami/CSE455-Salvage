using UnityEngine;
using System.Collections;
	
	[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour 
{

	public float speed;
	public Boundary boundary;

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		{
			Mathf.Clamp (Rigidbody.position.x, boundary.Min, boundary.Max),
		 	0.0f,
			Mathf.Clamp (Rigidbody.position.z, boundary.Min, boundary.Max),
		};
	}
}
