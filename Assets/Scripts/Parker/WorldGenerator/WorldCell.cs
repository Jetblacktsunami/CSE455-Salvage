using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class WorldCell : MonoBehaviour 
{

	public string worldName;
	public string cellName;
	public float distanceFromCenter;
	public bool hasBeenGenerated = false;
#if UNITY_EDITOR
	public string fileName = Application.dataPath + "/SaveData/";
#else
	public string fileName = Application.persistentDataPath;
#endif


	public void Start()
	{
		cellName = gameObject.name;
		worldName = gameObject.transform.parent.name;
		distanceFromCenter = Vector2.Distance(Vector2.zero, gameObject.transform.position );
		fileName +=  worldName + cellName;
	}

	public void Activate()
	{
		if (File.Exists(fileName)) 
		{
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
		XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);

		writer.WriteStartElement ("Asteroids");
		writer.WriteEndElement ();
	
		writer.Close ();

	}
	
	public void Load()
	{

	}




}
