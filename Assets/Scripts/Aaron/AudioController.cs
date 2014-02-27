/**************************************************************************
 * Programmed by Aaron Flores
 * Project Garbage Sanatizer
 * Code is based off of code by Gerardo Parker and Thomas __________
 *************************************************************************/

using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
//	//enumeration to create different sound states
//	public enum AudioState{moving,firing, stationary}
//	public AudioState state = AudioState.stationary;
//	//end of enumeration section
//
//	//Current Audio Sounds that will be in the game. Thinking of creating a SFX array to clean up and shorten code, probably best idea.
//	public AudioClip [] LevelSounds; //menu song is index 0, levels go from index 1 to length-2, game over goes in length-1
//	//array of sound effects public AudioClip[] SoundFX;
//	public AudioClip WeaponFire; // anything from here on down would be considered SFX
//	public AudioClip FlyingShip;
//	public AudioClip HealthAlert;
//	public AudioClip ShipDestroyed;
//
//	public AudioSource backgrounds = new AudioSource();
//	//for the array of sound effects public AudioSource sfx = new AudioSource();
//	public AudioSource shipfiring = new AudioSource();
//	public AudioSource shipflying = new AudioSource();
//	public AudioSource lowhealthalert = new AudioSource();
//	public AudioSource explosion = new AudioSource();
//	public AudioListener PlayerListener;
//
//	// Use this for initialization
//	void Start () 
//	{
//		backgrounds.loop = true; //repeats the song for the level
//		shipfiring.loop = true; //If sfx array is created, would this work for looping: sfx[index].loop = true/false;
//		shipflying.loop = true;
//		lowhealthalert.loop = false;
//		explosion.loop = false;
//
//		LevelSounds [0] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Menu Music/Loading Loop");
//		LevelSounds [1] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Level Music/chaser");
//		LevelSounds [2] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Level Music/Dark Future Loop");
//		LevelSounds [3] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Game Over/Icy Game Over");
//
//		//Four SFX array, use same method to assign an SFX to an array index^^^^^^^
//	}
//
//	// Update is called once per frame
//	void Update () 
//	{
//		if (PlayerInformation.setWeapoFireRate && state != AudioState.firing) //whenever the player is firing at a target, loop is played
//		{
//			shipfiring.Play();
//			state = AudioState.firing;
//		}
//		if (PlayerInformation.isMoving () && state != AudioState.moving) //whenever the ship is moving
//		{
//			shipflying.Play();
//			state = AudioState.moving;
//		}
//		if (PlayerInformation.getCurrentHealth <= (0.1 * PlayerInformation.getMaxHealth)) //whenever health falls below 15%
//		{
//			lowhealthalert.PlayOneShot();
//		}
//	}
//
//	void OnLevelWasLoaded()
//	{
//		if()//world/level was loaded appropriately. how is this checked?
//		{
//			//for level music variation, a random seed generator will be used that goes from index 1 to index[length-2]
//			backgrounds.Stop();
//			backgrounds.clip = LevelSounds[1];
//			backgrounds.Play();
//		}
//
//	}
//
//	void OnApplicationPause()
//	{
//		if()//whenever the player goes into the pause menu
//		{	
//				backgrounds.Stop();
//				backgrounds.clip = PauseMusic();
//				backgrounds.Play ();
//		}
//
//		else
//		{
//				backgrounds.Stop ();
//				backgrounds.clip = LevelSounds[0];
//				backgrounds.Play ();
//		}
//	}
//
//	void GameOver() //whenever the game is over
//	{
//			if (PlayerInformation.getCurrentHealth == 0) 
//			{
//				explosion.PlayOneShot();
//				backgrounds.Stop();
//				int gameover = LevelSounds.Length - 1;
//				backgrounds.clip = LevelSounds[gameover];
//				backgrounds.PlayOneShot();
//			}
//	}
}
