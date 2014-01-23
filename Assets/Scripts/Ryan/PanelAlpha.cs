using UnityEngine;
using System.Collections;

public class PanelAlpha : MonoBehaviour 
{
	public GameObject change;
	public GameObject wait;
	public float timeDelay;
	public float seconds;
	private UIPanel button;
	private TitleAnchorMoveY titleAnchorMoveY;
	
	void Start()
	{
		button = change.GetComponent<UIPanel> ();
		titleAnchorMoveY = wait.GetComponent<TitleAnchorMoveY> ();
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
		//Debug.Log (titleAnchorMoveY.OnClick);

	}
	
}
