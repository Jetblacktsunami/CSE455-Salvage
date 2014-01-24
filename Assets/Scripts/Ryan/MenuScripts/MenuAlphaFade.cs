using UnityEngine;
using System.Collections;

public class MenuAlphaFade : MonoBehaviour 
{
	public GameObject change;
	public float timeDelay;
	public float seconds;
	private UISprite button;

	void Start()
	{
		button = change.GetComponent<UISprite> ();
	}
	void Update()
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
 
}
