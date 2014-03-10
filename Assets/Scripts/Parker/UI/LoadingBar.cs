using UnityEngine;
using System.Collections;

public class LoadingBar : MonoBehaviour 
{
	private UISlider slider;
	private UILabel label;
	private UISprite[] sprites;
	public UISprite background;

	private static LoadingBar bar;

	public static LoadingBar Instance
	{
		get
		{
			return bar;
		}
	}

	// Use this for initialization
	void Start () 
	{
		bar = this;
		slider = gameObject.GetComponent<UISlider>();
		label = gameObject.GetComponentInChildren<UILabel>();
		sprites = gameObject.GetComponentsInChildren<UISprite>();

		background.alpha = 0;
		label.text = "";
		foreach(UISprite obj in sprites)
		{
			obj.alpha = 0f;
		}
	}

	public void Acivate()
	{
		background.alpha = 1f;

		if(Application.loadedLevelName == "InGame")
		{
			label.text = "Saving";
			slider.value = 0f;
			foreach(UISprite obj in sprites)
			{
				obj.alpha = 1f;
			}
		}
		else if(Application.loadedLevelName == "MainMenu")
		{
			label.text = "Loading";
			foreach(UISprite obj in sprites)
			{
				obj.alpha = 0f;
			}
		}

	}

	private void Deactive()
	{
		background.alpha = 0;
		label.text = "";
		foreach(UISprite obj in sprites)
		{
			obj.alpha = 0f;
		}
	}

	public void UpdateBar()
	{
		slider.value = GameManager.Instance.SavePercentage;
	}
	
	void OnLevelWasLoaded()
	{
		Deactive();
	}

}
