///**************************************************************************/
///** Programmed by Aaron Flores
///** Project Garbage Sanatizer
///** Coding is based off of code by Gerardo Parker and Thomas __________
///*************************************************************************/

using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
	//enumeration to create different sound states
	public enum AudioState{moving,firing, stationary}
	public AudioState state = AudioState.stationary;
	public AudioState state = AudioState.moving;
	public AudioState state = AudioState.firing;
	//end of enumeration section
	
	//Current Level Audio Sounds that will be in the game.
	public AudioClip [] LevelSounds; //menu song is index [0], levels go from index [1 to length-2], game over goes in index [length-1]
		LevelSounds[0] = "menu song"			//menu song
		LevelSounds[1] =  "level song 1"		//level song # 1
		LevelSounds[2] = "level song 2"			//level song # 2
		LevelSounds[3] = "game over song"		//level song # 3
	//Current SFX that will be in the game.
		public AudioClip [] Sound_FX;
		Sound_FX[0] = "weapon firing"			//weapon firing
		Sound_FX[1] = "ship flying"				//ship flying
		Sound_FX[2] = "low health alert"		//low health alert
		Sound_FX[3] = "ship destroyed"			//ship destroyed/explosion

	public AudioSource backgrounds = new AudioSource();
	public AudioSource sfx = new AudioSource();

	public AudioListener PlayerListener;

	// Use this for initialization
	void Start () 
	{
		//Level Music Array
		LevelSounds [0] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Menu Music/Loading Loop");
		LevelSounds [1] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Level Music/chaser");
		LevelSounds [2] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Level Music/Dark Future Loop");
		LevelSounds [3] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Game Over/Icy Game Over");

		//Sound Effects Array
		Sound_FX [0] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Shooting/laser_shooting_sfx");//		LevelSounds [0] = (AudioSource)Resources.Load ("Assets/Sounds/Music/Menu Music/Loading Loop");
		Sound_FX [1] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Flying Ship/warp engine engage");
		Sound_FX [2] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Alarm/enemy spots-lose sound");
		Sound_FX [3] = (AudioSource)Resources.Load ("Assets/Sounds/SoundFX/Explosion/Loading Loop");

		backgrounds.loop = true; //repeats the song for the level

		//setting appropriate loop value for certain sfx. Weapon Fire and Ship Flying only true loop values, others are false
		sfx.clip = Sound_FX[0];
		if(sfx.clip == Sound_FX[0])
		{
			sfx.loop = true;
		}

		sfx.clip = Sound_FX[1];
		if(sfx.clip == Sound_FX[1])
		{
			sfx.loop = true;
		}

		sfx.clip = Sound_FX[2];
		if(sfx.clip == Sound_FX[2])
		{
			sfx.loop = false;
		}

		sfx.clip = Sound_FX[3];
		if(sfx.clip == Sound_FX[3])
		{
			sfx.loop = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		while (PlayerInformation.getWeaponFireRate() && state != AudioState.firing) 			//whenever the player is firing at a target, loop is played
		{
			sfx.Stop();
			sfx.clip = Sound_FX[0];				//sets sfx = weapon firing sound
			sfx.Play();
			state = AudioState.firing;
		}

		while (PlayerInformation.accelerate() && state != AudioState.moving) 					//whenever the ship is moving
		{
			sfx.Stop();
			sfx.clip = Sound_FX[1];				//sets sfx = flying ship sound
			sfx.Play();
			state = AudioState.moving;
		}

		if (PlayerInformation.getCurrentHealth() <= (0.1 * PlayerInformation.getMaxHealth())) 	//whenever health falls to 10% or lower
		{
			sfx.Stop();
			sfx.clip = Sound_FX[2];				//sets sfx = low health alarm
			sfx.Play();
		}
	}

	void OnLevelWasLoaded()
	{
		//for level music variation, a random seed generator will be used that goes from index 1 to index[length-2]
		backgrounds.Stop();
		backgrounds.clip = LevelSounds[1];
		backgrounds.Play();
	}

	void OnApplicationPause()
	{
		if()//whenever the player goes into the pause menu
		{	
				backgrounds.Stop();
				backgrounds.clip = LevelSounds[0];			//sets backgrounds = menu music
				backgrounds.Play ();
		}

		else
		{
				backgrounds.Stop ();
				backgrounds.clip = LevelSounds[1];			//sets backgrounds = level music
				backgrounds.Play ();
		}
	}

	void OnGameOver() //whenever the game is over
	{
			if (PlayerInformation.getCurrentHealth() == 0) 
			{
				sfx.Stop();
				sfx.clip = Sound_FX[3];   					//sets sfx = explosion clip
				sfx.PlayOneShot();

				backgrounds.Stop();
				int gameover = LevelSounds.Length - 1;
				backgrounds.clip = LevelSounds[gameover];	//sets backgrounds = game over music
				backgrounds.PlayOneShot();
			}
	}
}