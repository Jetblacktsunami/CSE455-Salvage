using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {
	private GameObject player;
	private static MovementManager instance;
	
	public static MovementManager Instance
	{
		get
		{
			if(!instance)
			{
				instance = PlayerInformation.Instance.gameObject.AddComponent<MovementManager>();
			}
			return instance;
		}
	}

	void Awake()
	{
		if(!instance || instance == this)
		{
			instance = this;
		}
		else 
		{
			Destroy(this);
		}
		player = transform.parent.gameObject;
	}

	void Update ()
	{
		if(Joystick.LeftStick.GetMagnitude() >= 0.1)
		{
			Move();
		}
	}

	void Move()
	{
		Vector3 newPosition = player.transform.position;
		newPosition.x += Joystick.LeftStick.GetMagnitude() * PlayerInformation.Instance.getSpeed() * (Mathf.Cos(Joystick.LeftStick.GetAngle() * Mathf.Rad2Deg) * Joystick.LeftStick.GetMagnitude()) * Time.deltaTime;
		newPosition.y += Joystick.LeftStick.GetMagnitude() * PlayerInformation.Instance.getSpeed() * (Mathf.Sin(Joystick.LeftStick.GetAngle() * Mathf.Rad2Deg) * Joystick.LeftStick.GetMagnitude()) * Time.deltaTime;
		player.transform.position = newPosition;
		player.transform.rotation = Quaternion.AngleAxis(Joystick.LeftStick.GetAngle(), new Vector3(0f,0f,1.0f));
	}
}
