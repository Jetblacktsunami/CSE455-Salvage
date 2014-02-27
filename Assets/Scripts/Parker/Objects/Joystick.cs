using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour 
{
	public UIPanel parentPanel;
	public enum JoystickSide { right, left, unknown }
	public JoystickSide screenLocation = JoystickSide.unknown;
	private static Joystick leftStick;
	private static Joystick rightStick;

	public static Joystick LeftStick
	{
		get
		{
			return leftStick;
		}
		set
		{
			leftStick = value;	
		}
	}

	public static Joystick RightStick
	{
		get
		{
			return rightStick;
		}
		set
		{
			rightStick = value;
		}
	}

	private void Awake()
	{
		if(screenLocation == JoystickSide.left)
		{
			Joystick.LeftStick = this;
		}
		else if(screenLocation == JoystickSide.right)
		{
			Joystick.RightStick = this;
		}
	}

	public float GetAngle()
	{
		Vector2 position = gameObject.transform.position - gameObject.transform.parent.position;
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

	public float GetMagnitude()
	{
		Vector2 scale = new Vector2(gameObject.transform.localScale.x /2f, gameObject.transform.localScale.y/2f);
		Vector2 position = (Vector2)gameObject.transform.position - ((Vector2)gameObject.transform.parent.position) ;
		float mag = Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(position.x / ((parentPanel.transform.localScale.x - scale.x)/2f),2) + Mathf.Pow(position.y / ((parentPanel.transform.localScale.y - scale.y)/2f),2)), 0f, 1f);

		return mag;

	}
}
