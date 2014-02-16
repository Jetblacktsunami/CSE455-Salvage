using UnityEngine;
using System.Collections;

public class FlyingShip : MonoBehaviour {
	public AudioSource flying_ship;

	// Use this for initialization
	void Start () {
		flying_ship = (AudioSource)gameObject.AddComponent ("AudioSource");
		AudioClip hover;
		hover = (AudioClip)Assets.Load ("Sounds/Music/Game Over/Icy Game Over"); // dont know how to load from the Sounds folder
		flying_ship.clip = hover;
		flying_ship.loop = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		flying_ship.Play ();
	
	}
}
