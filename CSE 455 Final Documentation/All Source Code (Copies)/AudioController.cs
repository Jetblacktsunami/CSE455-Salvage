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
//	public AudioState state = AudioState.moving;
//	public AudioState state = AudioState.firing;
//	//end of enumeration section
//
//		//Will erase any useless code once everything is coded appropriately
//	//Current Audio Sounds that will be in the game. Thinking of creating a SFX array to clean up and shorten code, probably best idea.
//	public AudioClip [] LevelSounds; //menu song is index 0, levels go from index 1 to length-2, game over goes in length-1
//		LevelSounds[0] = "menu song"
//		LevelSounds[1] =  "level song 1"
//		LevelSounds[2] = "level song 2"
//		LevelSounds[3] = "level song 3"
//		LevelSounds[4] = "game over song"
//	//array of sound effects public AudioClip[] SoundFX;
//		SoundFX[0] = "weapon firing"
//		SoundFX[1] = "ship flying"
//		SoundFX[2] = "low health alert"
//		SoundFX[3] = "ship destroyed"
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
//		//Level Music Array
//		LevelSounds [0] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Menu Music/Loading Loop");
//		LevelSounds [1] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Level Music/chaser");
//		LevelSounds [2] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Level Music/Dark Future Loop");
//		LevelSounds [3] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Game Over/Icy Game Over");
//
//		//Sound Effects Array
//		SoundFX [0] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Shooting/laser_shooting_sfx");//		LevelSounds [0] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Menu Music/Loading Loop");
//		SoundFX [1] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Flying Ship/warp engine engage");
//		SoundFX [2] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Alarm/enemy spots-lose sound");
//		SoundFX [3] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Explosion/Loading Loop");
//
//		//Setting the loop values for each index. I doubt this is correct, its just an attempt
//		backgrounds.loop = true; //repeats the song for the level
//		SoundFX[0].loop = true; //If sfx array is created, would this work for looping: sfx[index].loop = true/false;
//		SoundFX[1].loop = true;
//		SoundFX[2].loop = false;
//		SoundFX[3].loop = false;
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
//		if (PlayerInformation.accelerate() && state != AudioState.moving) //whenever the ship is moving
//		{
//			shipflying.Play();
//			state = AudioState.moving;
//		}
//		if (PlayerInformation.getCurrentHealth() <= (0.1 * PlayerInformation.getMaxHealth())) //whenever health falls below 15%
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
//			if (PlayerInformation.getCurrentHealth() == 0) 
//			{
//				explosion.PlayOneShot();
//				backgrounds.Stop();
//				int gameover = LevelSounds.Length - 1;
//				backgrounds.clip = LevelSounds[gameover];
//				backgrounds.PlayOneShot();
//			}
//	}
}
