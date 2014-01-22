using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class WorldCell : MonoBehaviour 
{

	public string worldName;
	public string cellName;
	public float distanceFromCenter;
	public string fileName;

#if UNITY_EDITOR
	public string directory = Application.dataPath + "/SaveData/";
#else
	public string directory = Application.persistentDataPath + "/SaveData/";
#endif


	public void Start()
	{
		cellName = gameObject.name;
		worldName = gameObject.transform.parent.name;
		distanceFromCenter = Vector2.Distance(Vector2.zero, gameObject.transform.position );
		directory += "/" + worldName + "/";
		fileName += directory + cellName + ".xml";
	}

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

	private IEnumerator Generate ()
	{
		yield return new WaitForSeconds (0);
		Save ();
	}

	public void Save()
	{
		if(!Directory.Exists(fileName))
		{
			Directory.CreateDirectory(directory);
		}
		XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);

		writer.WriteStartElement ("Asteroids");
		writer.WriteEndElement ();
	
		writer.Close ();

	}
	
	public void Load()
	{
		XmlTextReader reader = new XmlTextReader (fileName);

		Debug.Log ("found " + fileName);

		reader.Close ();
	}




}
