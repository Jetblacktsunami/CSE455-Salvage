using UnityEngine;
using System.Collections;

public class Explosions : MonoBehaviour 
{
	public AudioSource explosion;
	/******************
	// Use this for initialization
	void Start () {
		explosion = (AudioSource)gameObject.AddComponent ("AudioSource");
		AudioClip boom;
		boom = (AudioClip)Assets.Load ("Sounds/SoundFX/Explosions/explosion best"); // dont know how to load from the Sounds folder
		explosion.clip = boom;
		explosion.loop = false;
	
	}

	// Update is called once per frame
	void Update () {

//		explosion.PlayOneShot ();
	
	}
	*********************/
}
