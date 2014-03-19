using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour 
{
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
	}

	void Update ()
	{
		if(Joystick.LeftStick.GetMagnitude() >= 0.1 && PlayerInformation.Instance.getCurrentFuel() > 0)
		{
			Move();
		}
	}

	void Move()
	{
		float mag = Joystick.LeftStick.GetMagnitude ();
		float angle = Joystick.LeftStick.GetAngle ();
		Vector3 newPosition = transform.position;
		newPosition.x += mag * PlayerInformation.Instance.getSpeed() * Time.deltaTime * Mathf.Cos(angle * Mathf.Deg2Rad);
		newPosition.y += mag * PlayerInformation.Instance.getSpeed() * Time.deltaTime * Mathf.Sin(angle * Mathf.Deg2Rad);
		transform.position = newPosition;
		transform.rotation = Quaternion.AngleAxis(angle + 180f, new Vector3(0f,0f,1.0f));
		PlayerInformation.Instance.ConsumeFuel();

	}

	public static void Wipe()
	{
		MovementManager.instance = null;
	}
}
