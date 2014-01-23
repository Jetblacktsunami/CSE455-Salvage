using UnityEngine;
using System.Collections;

public class MenuAnchorMoveY : MonoBehaviour 
{
	public GameObject move;
	public float seconds;
	public float timeDelay;
	public float speed;
	private UIAnchor anchor;
	
	
	void Start()
	{
		anchor = move.GetComponent<UIAnchor> ();
	}
	void Update()
	{
		if(timeDelay > 0)
		{
			timeDelay -= Time.deltaTime;
		}
		if (timeDelay <= 0 && anchor.relativeOffset.y < 0.25) 
		{
			anchor.relativeOffset.y += speed;
		}
	}
	
}

