using UnityEngine;
using System.Collections;
using System;

public class SubMenuAlpha : MonoBehaviour 
{
	public MenuAlpha.menuButtonType panelType;
	public GameObject panelGroup;
	private UIPanel panel;
		
	void Start () 
	{
		panel = panelGroup.GetComponent<UIPanel> ();
	}

	void OnEnable()
	{
		MenuAlpha.ButtonClicked += SwitchAlpha;
		MenuBack.BackClicked += SwitchAlphaBack;
	}
	
	void OnDisable()
	{
		MenuAlpha.ButtonClicked -= SwitchAlpha;
		MenuBack.BackClicked -= SwitchAlphaBack;
	}

	void SwitchAlpha(MenuAlpha.menuButtonType bType)
	{
		if (bType == panelType) 
		{
			Debug.Log(bType);
			panel.alpha = 1.0f;
		}
		else 
		{
			panel.alpha = 0;
		}
	}
	
	
	void SwitchAlphaBack()
	{
		panel.alpha = 0.0f;
	}
}