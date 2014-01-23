using UnityEngine;
using System.Collections;

public class MenuAnchorMove : MonoBehaviour 
{
	public GameObject move;
	public float seconds;
	public float timeDelay;
	public float speed;
	private UIAnchor anchor;
	//private Vector2 screen;

	
	void Start()
	{
		anchor = move.GetComponent<UIAnchor> ();
		//screen = new Vector2 (Screen.width, Screen.height);
		//anchor.relativeOffset.x = 0.3f;
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

		//anchor.relativeOffset.x += 0.01f;
		Debug.Log (anchor.relativeOffset.x);
	}
	
}

