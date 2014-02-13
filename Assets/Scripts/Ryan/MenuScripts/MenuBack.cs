using UnityEngine;
using System.Collections;
using System;

public class MenuBack : MonoBehaviour 
{
	public static Action BackClicked;
	//public GameObject panelGroup;
	/*
	private UIPanel panel;
	
	void Start () 
	{
		panel = panelGroup.GetComponent<UIPanel> ();
	}
	*/
	void OnClick()
	{
		BackClicked();
	}
}
