using UnityEngine;
using System.Collections;

public class MenuAnchorMoveX : MonoBehaviour 
{
	public delegate void OpenNewGame( bool newGameIsClicked );
	public static event OpenNewGame onNewGameClick;

	public GameObject move;
	public float seconds;
	public float timeDelay;
	public float speed;
	private UIAnchor anchor;
	private bool go;
	private bool clicked1;
	private bool clicked2;
	private bool shift1;
	private bool shift2;
	
	void Start()
	{
		anchor = move.GetComponent<UIAnchor> ();
	}

	void OnEnable()
	{
		TitleAnchorMoveY.onTitleClick += onTitleClick;
	}

	void onTitleClick(bool titleIsClicked)
	{
		go = true;
	}
	void OnClick()
	{
		if(shift1 == true && shift2 != true)
		{
			if(onNewGameClick != null)
			{
				onNewGameClick(true);
				clicked2 = true;
			}
			clicked1 = true;
		}
		if(shift2 == true)
		{
			clicked2 = true;
		}
	}

	void Update()
	{
		if(go == true && shift1 != true)
		{
			if(timeDelay > 0)
			{
				timeDelay -= Time.deltaTime;
			}
			if (timeDelay <= 0 && anchor.relativeOffset.x < 0.3f) 
			{
				anchor.relativeOffset.x += speed;
			}
			if(anchor.relativeOffset.x > 0.3f)
			{
				shift1 = true;
			}
		}
		if(anchor.relativeOffset.x > 0 && clicked1 == true)
		{
			anchor.relativeOffset.x -= speed * 0.3f;
		}
		if(anchor.relativeOffset.x <= 0 && shift1 == true)
		{
			shift2 = true;
		}

	}
	
}

