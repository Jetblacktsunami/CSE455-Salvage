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
		//Debug.Log (sliderBackType);
		if(sliderBackType == volumeSliderType.Master)
		{
			volume.value = PlayerPrefs.GetFloat("MasterVol");
		}
		else if(sliderBackType == volumeSliderType.FX)
		{
			volume.value = PlayerPrefs.GetFloat("FXVol");
		}
		else if(sliderBackType == volumeSliderType.Music)
		{
			volume.value = PlayerPrefs.GetFloat("MusicVol");
		}
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

		if(sliderBackType == (sliderType))
		{
			if(sliderBackType == volumeSliderType.Master)
			{
				PlayerPrefs.SetFloat("MasterVol", volume.value);
			}
			else if(sliderBackType == volumeSliderType.FX)
			{
				PlayerPrefs.SetFloat("FXVol", volume.value);
			}
			else if(sliderBackType == volumeSliderType.Music)
			{
				PlayerPrefs.SetFloat("MusicVol", volume.value);
			}
			//Debug.Log (sliderType + " " + volume.value);

		}
	}

	void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
		//	Debug.Log(sliderBackType + " " + volume.value);
			if(sliderBackType == volumeSliderType.Master)
			{
				PlayerPrefs.SetFloat("MasterVol", volume.value);
			}
			else if(sliderBackType == volumeSliderType.FX)
			{
				PlayerPrefs.SetFloat("FXVol", volume.value);
			}
			else if(sliderBackType == volumeSliderType.Music)
			{
				PlayerPrefs.SetFloat("MusicVol", volume.value);
			}
		}
	}

}
