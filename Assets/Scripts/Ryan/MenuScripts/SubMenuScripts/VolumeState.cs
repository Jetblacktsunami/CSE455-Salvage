using UnityEngine;
using System.Collections;
using System;

public class VolumeState : MonoBehaviour 
{
	public static Action<volumeSliderType> SliderMoved;
	public enum volumeSliderType{Master, FX, Music};
	public volumeSliderType sliderBackType;
	private UISlider volume;

	void Start()
	{
		volume = gameObject.GetComponent<UISlider> ();
	}

	void OnEnable()
	{
		VolumeThumb.thumbIsPressed += UpdateFromThumb;
	}

	void OnDisable()
	{
		VolumeThumb.thumbIsPressed -= UpdateFromThumb;
	}

	void UpdateFromThumb (volumeSliderType sliderType)
	{
		if(sliderBackType.CompareTo(sliderType) == 0)
		{
			Debug.Log (sliderType + " " + volume.value);
		}
	}

	void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
			Debug.Log(sliderBackType + " " + volume.value);
		}
	}

}
