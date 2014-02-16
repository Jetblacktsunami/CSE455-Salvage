using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public AudioSource shooting;
/******************
	// Use this for initialization
	void Start () {
		shooting = (AudioSource)gameObject.AddComponent ("AudioSource");
		AudioClip bang;
		bang = (AudioClip)Assets.Load ("Sounds/SoundFX/Shooting/laser_shooting_sfx"); // dont know how to load from the Sounds folder
		shooting.clip = bang;
		shooting.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		shooting.PlayOneShot ();
	
	}
	*********************/
}
