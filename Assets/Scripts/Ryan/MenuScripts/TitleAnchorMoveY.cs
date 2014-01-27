using UnityEngine;
using System.Collections;

public class TitleAnchorMoveY : MonoBehaviour 
{
	public delegate void MenuStart( bool titleIsClicked );
	public static event MenuStart onTitleClick;

	public GameObject move;
	//public float seconds;
	//public float timeDelay;
	public float speed;
	private UIAnchor anchor;
	private UIStretch stretch;
	private bool go;
	
	
	void Start()
	{
		anchor = move.GetComponent<UIAnchor> ();
		stretch = move.GetComponent<UIStretch> ();
	}

	void OnClick ()
	{
		if(onTitleClick != null)
		{
			onTitleClick (true);
			go = true;
		}
	}

	void Update()
	{

		if (anchor.relativeOffset.y < 0.25f && go == true) 
		{
			anchor.relativeOffset.y += speed;
		}
	}
	
}

