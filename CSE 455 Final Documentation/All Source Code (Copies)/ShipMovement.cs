using UnityEngine;
using System.Collections;
using System;

public class ShipMovement : MonoBehaviour 
{	
	public float speed;
	private float conversion;
	private float current = 0.0f;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float movex = (float)Math.Sin (transform.eulerAngles.z * Math.PI / 180);
		float movey = (float)Math.Cos (transform.eulerAngles.z * Math.PI / 180);

		Vector3 movement = new Vector3 (moveVertical * -movex, moveVertical * movey, 0);
		rigidbody.velocity = movement * speed;
		current += -moveHorizontal * 5;

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, current);

		//Debug.Log ("Angle: " + transform.eulerAngles.z + "   " + "Mathx: " + Math.Sin(transform.eulerAngles.z * Math.PI / 180));
	}
}
