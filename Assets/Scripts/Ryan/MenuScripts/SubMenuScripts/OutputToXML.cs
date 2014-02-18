using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class OutputToXML : MonoBehaviour 
{
	public enum inputType {Name, Seed};
	public inputType inputTypeValue;
	private UIInput inputValue;

	void Start()
	{
		inputValue = gameObject.GetComponent<UIInput> ();
	}

	void OnEnable()
	{
		Generate.generateIsClicked += send;
	}
	void OnDisable()
	{
		Generate.generateIsClicked += send;
	}

	void send()
	{

		if(inputTypeValue.ToString() == "Name")
		{
			GameManager.WorldName = inputValue.value;
		}
		else
		{
			GameManager.seed = int.Parse(inputValue.value);
		}

		//List<WorldGenerator.WorldSpecs> worlds = WorldGenerator.GetCreatedWorlds();
		//GameManager.savedLevel = worlds [0];
		Debug.Log(inputTypeValue + " " + inputValue.value);
	}
	
}
