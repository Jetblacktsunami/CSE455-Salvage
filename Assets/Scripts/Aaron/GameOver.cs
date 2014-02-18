using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public AudioSource game_over;

	/**************************
	// Use this for initialization
	void Start () {
		game_over = (AudioSource)gameObject.AddComponent ("AudioSource");
		AudioClip dead;
		dead = (AudioClip)Assets.Load ("Sounds/Music/Game Over/Icy Game Over"); // dont know how to load from the Sounds folder
		game_over.clip = dead;
		game_over.loop = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		game_over.PlayOneShot ();
	
	}
	**************************/
}
