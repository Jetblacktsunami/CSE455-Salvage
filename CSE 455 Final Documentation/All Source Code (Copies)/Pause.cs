using UnityEngine;
using System.Collections;
using System;

public class Pause : MonoBehaviour 
{

	public static Action PauseClicked;
	// Use this for initialization


	void OnClick()
	{
		if(PauseClicked != null)
		{
			PauseClicked();
			Time.timeScale = 0;
			//gameObject.transform.parent.GetComponent<UIPanel> ().alpha = 0;
		}
	}
}
