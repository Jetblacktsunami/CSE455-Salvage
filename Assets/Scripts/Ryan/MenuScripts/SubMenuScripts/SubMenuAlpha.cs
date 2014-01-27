using UnityEngine;
using System.Collections;

public class SubMenuAlpha : MonoBehaviour 
{
	private MenuAnchorMoveX menuAnchorMoveX;
	private UIPanel button;
	public GameObject change;
	private bool hide;
	public float delay;

	void Start()
	{
		button = change.GetComponent<UIPanel> ();
	}

	void OnEnable()
	{
		MenuAnchorMoveX.onNewGameClick += onNewGameClick;
	}

	void onNewGameClick (bool newGameIsClicked)
	{
		hide = true;
	}

	void Update()
	{
		if(gameObject.tag == "NewGameOptions" && hide == true)
		{
			delay -= Time.deltaTime;
			if(delay <= 0)
			{
				button.alpha += Time.deltaTime * 5;
			}
		}
	}
}
