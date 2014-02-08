using UnityEngine;
using System.Collections;

public class AudioSourceController : MonoBehaviour 
{
	//private AudioSource volume;
	public Vector3 volumes;
	private float defaultVol = 1.0f;

	// Use this for initialization
	void Start () 
	{
		//volume = gameObject.GetComponent<AudioSource> ();
		if (!PlayerPrefs.HasKey ("MasterVol")) 
		{
			PlayerPrefs.SetFloat("MasterVol", defaultVol);
			PlayerPrefs.Save();
		}
		if(!PlayerPrefs.HasKey ("FXVol"))
		{
			PlayerPrefs.SetFloat("FXVol", defaultVol);
			PlayerPrefs.Save();
		}
		if(!PlayerPrefs.HasKey ("MusicVol"))
		{
			PlayerPrefs.SetFloat("MusicVol", defaultVol);
			PlayerPrefs.Save();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		volumes.Set(PlayerPrefs.GetFloat ("MasterVol"), PlayerPrefs.GetFloat ("MusicVol"), PlayerPrefs.GetFloat ("FXVol"));
		audio.volume = PlayerPrefs.GetFloat ("MasterVol") * PlayerPrefs.GetFloat ("MusicVol");
	}
}
