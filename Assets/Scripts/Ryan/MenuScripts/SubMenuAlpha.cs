using UnityEngine;
using System.Collections;
using System;

public class SubMenuAlpha : MonoBehaviour 
{
	public MenuAlpha.menuButtonType panelType;
	public GameObject panelGroup;
	private UIPanel panel;
	private UIPanel[] childPanels;
		
	void Start () 
	{
		panel = panelGroup.GetComponent<UIPanel> ();
		childPanels = gameObject.GetComponentsInChildren<UIPanel> ();
		if(panel.alpha < 1.0f)
		{
			if(childPanels.Length > 0)
			{
				foreach(UIPanel obj in childPanels)
				{
					obj.alpha = panel.alpha;
				}
			}
		}
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
//			Debug.Log(bType);
			panel.alpha = 1.0f;
			
			if(childPanels.Length > 0)
			{
				foreach(UIPanel obj in childPanels)
				{
					obj.alpha = panel.alpha;
				}
			}
		}
		else 
		{
			panel.alpha = 0;
			
			if(childPanels.Length > 0)
			{
				foreach(UIPanel obj in childPanels)
				{
					obj.alpha = panel.alpha;
				}
			}
		}
	}
	
	
	void SwitchAlphaBack()
	{
		panel.alpha = 0.0f;
		if(childPanels.Length > 0)
		{
			foreach(UIPanel obj in childPanels)
			{
				obj.alpha = panel.alpha;
			}
		}
	}
}