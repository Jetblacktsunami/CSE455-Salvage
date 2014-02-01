using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// World cell class used for each spawning area of asteroids
/// </summary>
public class WorldCell : MonoBehaviour 
{

	#region Saving Path ; where to save the game according to platform
	#if UNITY_EDITOR
	public string mainDirectory = Application.dataPath + "/SaveData/";
	public string directory = Application.dataPath + "/SaveData/";
	#else
	public string mainDirectory = Application.persistentDataPath + "/SaveData/";
	public string directory = Application.persistentDataPath + "/SaveData/";
	#endif
	#endregion

	#region data ; for this class
	private string worldName; //name of the generated world
	private string cellName; //id of this cell
	private string fileName; //the exact file we are either saving or loading from
	private bool startRan = false; //has the start function ran this game for this cell
	private float distanceFromCenter = 0.0f; //this is going to be used to add varience in the spawned asteriods
	private List<Asteroid> children = new List<Asteroid>();
	#endregion



	//first function that runs upon startup of object
	public void Start()
	{
		if(!startRan)
		{
			cellName = gameObject.name;
			worldName = gameObject.transform.parent.name;
			distanceFromCenter = Vector2.Distance(Vector2.zero, gameObject.transform.position );
			if(!Directory.Exists(mainDirectory))
			{
				Directory.CreateDirectory(mainDirectory);
			}
			directory += "/" + worldName + "/";
			fileName += directory + cellName + ".xml";
			startRan = true;


		}
	}

	//activate the cell so that it spawns all necessary objects.
	public void Activate()
	{
		Debug.Log ("Activating");
		Start();

		if (Directory.Exists(directory) && File.Exists(fileName)) 
		{
			Debug.Log("Loading");
			Load ();
		}
		else
			StartCoroutine(Generate());
	}

	//turns the object off
	public void Deactivate()
	{
		gameObject.SetActive(false);
		Save ();
	}

	//if this is the first time being activated the cell will call this to spawn the asteroids
	private IEnumerator Generate ()
	{
		Debug.Log("Generating");
		//GameObject asteroid = new GameObject("Asteroid");
		//asteroid.AddComponent<Asteroid>();

		yield return new WaitForSeconds (0);
		Save ();
	}

	//saves the current objects in the cell as well as their positions
	public void Save()
	{
		if(!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}
		XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);

		foreach(Asteroid child in children)
		{
		
		}
	
		writer.Close ();

	}

	//loads all the objects in the cell
	public void Load()
	{
		XmlTextReader reader = new XmlTextReader (fileName);


		reader.Close ();
	}
}
