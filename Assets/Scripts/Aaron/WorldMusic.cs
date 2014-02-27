using UnityEngine;
using System.Collections;



public class WorldMusic : MonoBehaviour {
	public AudioSource bckgrnd_music; //Used AudioSource instead of AudioClip becasue that was the name of the object component

//	// Use this for initialization
//	void Start () {
//
//		bckgrnd_music = (AudioSource)gameObject.AddComponent ("AudioSource");
//		AudioSource level_music;
//		level_music = (AudioSource)Assets.Load ("Sounds/Music/Level Music/chaser"); // dont know how to load from the Sounds folder
//		bckgrnd_music.clip = level_music;
//		bckgrnd_music.loop = true;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//		bckgrnd_music.Play ();
//	
//	}
}
