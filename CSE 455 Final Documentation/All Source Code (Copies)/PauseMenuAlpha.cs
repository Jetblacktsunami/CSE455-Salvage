using UnityEngine;
using System.Collections;

public class PauseMenuAlpha : MonoBehaviour {

	UIPanel parentPanel;

	void Start()
	{
		parentPanel = gameObject.transform.parent.parent.GetComponent<UIPanel> ();
	}

	void OnEnable()
	{
		Pause.PauseClicked += alphaup;
	}
	void OnDisable()
	{
		Pause.PauseClicked -= alphaup;
	}

	void alphaup()
	{
		parentPanel.alpha = 1;
	}


	//to work this script has to be on the resume button
	void OnClick()
	{
		gameObject.transform.parent.parent.GetComponent<UIPanel> ().alpha = 0;
		Time.timeScale = 1;
	}
}
