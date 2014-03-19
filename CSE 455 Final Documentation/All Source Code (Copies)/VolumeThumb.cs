using UnityEngine;
using System.Collections;
using System;

public class VolumeThumb : MonoBehaviour 
{
	public static Action <VolumeState.volumeSliderType>thumbIsPressed;
	public VolumeState.volumeSliderType sliderType ;

	void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
			thumbIsPressed(sliderType);
		}
	}
}
