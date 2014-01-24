using UnityEngine;
using System.Collections;

public class PanelAlpha : MonoBehaviour 
{
	public GameObject change;
	public float timeDelay;
	public float seconds;
	private UIPanel button;
	private TitleAnchorMoveY titleAnchorMoveY;
	private MenuAnchorMoveX menuAnchorMoveX;
	private bool go;
	private bool hide;
	
	void Start()
	{
		button = change.GetComponent<UIPanel> ();
	}

	void OnEnable()
	{
		TitleAnchorMoveY.onTitleClick += onTitleClick;
		MenuAnchorMoveX.onNewGameClick += onNewGameClick;
	}
	//this is looking for a click on the title
	void onTitleClick (bool titleIsClicked)
	{
		go = true;
	}
	//this is looking for a click on the new game menu option
	void onNewGameClick (bool newGameIsClicked)
	{
		hide = true;
	}

	void Update()
	{
		//will wait for delay then increase alpha to 1 if title has been clicked and new game has not
		if(go == true && hide != true)
		{
			if(timeDelay > 0)
			{
				timeDelay -= Time.deltaTime;
			}
			if (timeDelay <= 0 && button.alpha < 1) 
			{
				button.alpha += Time.deltaTime * seconds;
			}
		}
		//will drop the alpha to 0 of all menu buttons but new game
		if(hide == true && gameObject.tag != "NewGame")
		{
			button.alpha -= Time.deltaTime * 5;
		}
		//Debug.Log (button.alpha);

	}
	
}
