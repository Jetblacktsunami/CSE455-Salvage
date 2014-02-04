using UnityEngine;
using System.Collections;
using System;

public class VolumeState : MonoBehaviour 
{
	public static Action<volumeSliderType> SliderMoved;
	public enum volumeSliderType {Master, FX, Music};
	private UISlider volume;

	void Start()
	{
		volume = gameObject.GetComponent<UISlider> ();
	}

	void OnValueChange (float volume)
	{
		Debug.Log(volume);
	}
	/*
	void Update()
	{
		camera.audio.volume = volume.value;
	}
	*/
}
