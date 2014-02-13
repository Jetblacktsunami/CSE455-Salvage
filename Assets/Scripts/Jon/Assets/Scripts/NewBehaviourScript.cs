using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

public class MoveAround : MoveAround {
	public float speed = 3.0f;
	public float rotateSpeed = 10.0f;
	public Joystick moveJoystick;
	public Joystick rotateJoystick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
