using UnityEngine;
using System.Collections;

public class UIResetOnDepress : MonoBehaviour 
{
	void OnPress(bool isPressed)
	{
		if(!isPressed)
		{
			gameObject.transform.localPosition = Vector3.zero;
		}
	}
}
