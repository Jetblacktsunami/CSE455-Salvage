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
			inputValue.value = inputValue.value.Trim();
			GameManager.WorldName = inputValue.value;
		}
		else
		{
			int seedCheck = 0;
			int.TryParse(inputValue.value,out seedCheck);
			if(seedCheck == 0)
			{
				seedCheck = 46;
			}
			GameManager.seed = seedCheck;
		}
	}
	
}
