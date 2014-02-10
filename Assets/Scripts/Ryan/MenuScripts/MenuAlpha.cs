using UnityEngine;
using System;
using System.Collections;

public class MenuAlpha : MonoBehaviour 
{
	public static Action<menuButtonType> ButtonClicked;
	public enum menuButtonType {NewGame, LoadGame, Options, Credits, HowToPlay, Back};
	public menuButtonType buttonType;
	public GameObject panelGroup;

	private UIPanel panel;

	void Start () 
	{
		panel = panelGroup.GetComponent<UIPanel> ();
	}

	//listen for a click of a menu button or the back submenu button
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

	//broadcasting the click event of a button with this script attached
	void OnClick()
	{
		ButtonClicked (buttonType);
	}

	void SwitchAlpha(menuButtonType bType)
	{
		panel.alpha = 0.0f;
	}


	void SwitchAlphaBack()
	{
		panel.alpha = 1.0f;
	}
}
