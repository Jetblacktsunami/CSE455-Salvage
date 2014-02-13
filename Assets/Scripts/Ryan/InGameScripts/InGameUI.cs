using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour 
{
	private UISlider vitalbar;
	private float decrement = 1.0f / 3.0f; 

	void Start()
	{
		vitalbar = gameObject.GetComponent<UISlider> ();
	}
	void OnEnable()
	{
		CollisionBroadcaster.Collided += Damage;
	}
	void OnDissable()
	{
		CollisionBroadcaster.Collided -= Damage;
	}

	void Damage()
	{
		Debug.Log ("Current value: " + vitalbar.value);
		vitalbar.value -= decrement;
		if (vitalbar.value < 0.01) 
		{
			Debug.Log("You are dead!");
		}

	}
}
