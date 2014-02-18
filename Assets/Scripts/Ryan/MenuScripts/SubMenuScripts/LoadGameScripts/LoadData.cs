using UnityEngine;
using System.Collections;
using System;

public class LoadData : MonoBehaviour 
{
	public UILabel LoadLabel;
	public string LoadName;
	public WorldGenerator.WorldSpecs LoadedLevel;

	void OnClick()
	{
		GameManager.savedLevel = LoadedLevel;
		Application.LoadLevel ("InGame");
	}

}

