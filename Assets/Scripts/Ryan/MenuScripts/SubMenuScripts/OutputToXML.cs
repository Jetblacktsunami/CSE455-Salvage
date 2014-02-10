using UnityEngine;
using System.Collections;

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
		Debug.Log(inputTypeValue + " " + inputValue.value);
	}
	
}
