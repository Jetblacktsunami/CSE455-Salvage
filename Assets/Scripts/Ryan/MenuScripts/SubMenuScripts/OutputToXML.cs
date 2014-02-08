using UnityEngine;
using System.Collections;

public class OutputToXML : MonoBehaviour 
{


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

	}

	// Update is called once per frame
	void Update () {
	
	}
}
