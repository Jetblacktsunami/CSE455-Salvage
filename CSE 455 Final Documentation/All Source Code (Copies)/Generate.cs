using UnityEngine;
using System.Collections;
using System;

public class Generate : MonoBehaviour 
{
	public static Action generateIsClicked;

	// Update is called once per frame
	void OnClick () 
	{
		generateIsClicked();
		LoadingBar.Instance.Acivate();
		Application.LoadLevel ("InGame");
	}
}
