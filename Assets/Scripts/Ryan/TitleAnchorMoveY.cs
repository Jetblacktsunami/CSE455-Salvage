using UnityEngine;
using System.Collections;

public class TitleAnchorMoveY : MonoBehaviour 
{
	public GameObject move;
	public float seconds;
	public float timeDelay;
	public float speed;
	private UIAnchor anchor;
	public bool click;
	
	
	void Start()
	{
		anchor = move.GetComponent<UIAnchor> ();
		click = false;
	}

	void OnClick ()
	{
		click = true; 
	}
	public bool BroadcastClick()
	{
		if(click == true)
		{
			return true;
		}
		return false;
	}

	void Update()
	{

		if (anchor.relativeOffset.y < 0.25f && click == true) 
		{
			anchor.relativeOffset.y += speed;
		}
	}
	
}

