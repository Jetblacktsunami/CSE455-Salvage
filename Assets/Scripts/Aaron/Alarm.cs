using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour {
	public AudioSource lh_alarm; //low-health alarm

	// Use this for initialization
	void Start () {
		lh_alarm = (AudioSource)gameObject.AddComponent ("AudioSource");
		AudioClip alarm;
		alarm = (AudioClip)Assets.Load ("Sounds/SoundFX/Alarm/enemy spots-lose sound"); // dont know how to load from the Sounds folder
		lh_alarm.clip = alarm;
		lh_alarm.loop = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		lh_alarm.Play ();
	
	}
}
