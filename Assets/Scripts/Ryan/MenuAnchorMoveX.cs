using UnityEngine;
using System.Collections;

public class MenuAnchorMoveX : MonoBehaviour 
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
		if (timeDelay <= 0 && anchor.relativeOffset.x < 0.3) 
		{
			anchor.relativeOffset.x += speed;
		}
	}
	
}

