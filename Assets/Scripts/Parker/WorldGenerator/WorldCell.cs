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
	public GameObject parent;
	private string worldName; //name of the generated world
	private string cellName; //id of this cell
	private string fileName; //the exact file we are either saving or loading from
	private bool startRan = false; //has the start function ran this game for this cell
#pragma warning disable 0414
	private float distanceFromCenter = 0.0f; //this is going to be used to add varience in the spawned asteriods
#pragma warning restore
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

			if(!parent)
			{
				parent = new GameObject("Asteroids");
			}
			parent.transform.position = transform.position;
			parent.transform.parent = transform;
			parent.transform.localScale = Vector3.one;
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
		//Generate();
		//StartCoroutine(Generate());
		GenerateXMLData();
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
		ParkerSpaceSystem.WorldGenerator.WorldSpecs details = ParkerSpaceSystem.WorldGenerator.worldspec;

		float maxDistance = details.mapLength / 2.0f;
		GameObject parent = new GameObject ("Asteroids");
		parent.transform.position = transform.position;
		parent.transform.parent = transform;
		parent.transform.localScale = Vector3.one;

		Vector2 startingPos = new Vector2( 0 - (details.cellLength /2.0f), 0 - (details.cellLength /2.0f));
		Vector2 endPos = new Vector2( 0 + (details.cellLength /2.0f), 0 + (details.cellLength /2.0f));

		Color[] colorValues = new Color[(int)details.cellLength * (int)details.cellLength];
		GameObject[,] asteroids = new GameObject[(int)details.cellLength,(int)details.cellLength];
		
		for(int i = (int)startingPos.x; i < endPos.x ; i++)
		{
			for(int j = (int)startingPos.y; j < endPos.y ; j++)
			{				
				float distance = Mathf.Sqrt( Mathf.Pow(i + transform.position.x,2) + Mathf.Pow(j + transform.position.y ,2));
				if(distance < maxDistance && distance > maxDistance / 10.0)
				{
					float degree;
					if( j != 0)
					{
						degree = Mathf.Rad2Deg * Mathf.Atan( i / j);
						if( i >= 0)
						{
							if(j < 0)
							{
								degree += 360.0f;
							}
						}
						else if(i < 0)
						{
							if(j > 0)
							{
								degree += 180.0f;
							}
							else if(j < 0)
							{
								degree += 270.0f;
							}
						}
					}
					else
					{
						if(i >= 0)
						{
							degree = 0;
						}
						else
						{
							degree = 180;
						}
					}
					
					if( !details.invalidSpawnPoints.Contains(new Vector2(i,j)))
					{
						float xCoord = ((i + transform.position.x) + details.mapLength /2) / (float)details.mapLength * 25.6f;
						float yCoord = ((j + transform.position.y) + details.mapLength /2) / (float)details.mapLength * 25.6f;

						float scale = Mathf.PerlinNoise(xCoord,yCoord);

						//float scale = Mathf.PerlinNoise(Mathf.Pow(xCoord,yCoord/xCoord),Mathf.Pow(yCoord,xCoord/yCoord));
						if(scale < 0.4f)
						{
							colorValues[(i + ((int)details.cellLength/2))* (int)details.cellLength + (j+ ((int)details.cellLength/2))] = new Color(scale,scale,scale);
							GameObject game =  GameObject.CreatePrimitive(PrimitiveType.Cube);
							game.transform.position = transform.position + new Vector3(i,j);
							game.transform.parent = parent.transform;
							asteroids[i  + ((int)details.cellLength/2),j + ((int)details.cellLength/2)] = game;

						}
					}
				}
			}
		}

		for(int i = 0,c = 0; i < (int)details.cellLength ; i++)
		{
			for(int j = 0; j < (int)details.cellLength; j++, c++)
			{
				if(asteroids[i,j])
				{
					asteroids[i,j].renderer.material.color = colorValues[c];
				}
			}
		}

		yield return new WaitForSeconds (0);
		//Save ();
	}
			
	//if this is the first time being activated the cell will call this to spawn the asteroids
	public void GenerateXMLData ()
	{
		if(!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		ParkerSpaceSystem.WorldGenerator.WorldSpecs details = ParkerSpaceSystem.WorldGenerator.worldspec;
		
		float maxDistance = details.mapLength / 2.0f;
		float[] perlinValue = new float[(int)details.cellLength * (int)details.cellLength];

		Vector2 startingPos = new Vector2( 0 - (details.cellLength /2.0f), 0 - (details.cellLength /2.0f));
		Vector2 endPos = new Vector2( 0 + (details.cellLength /2.0f), 0 + (details.cellLength /2.0f));
		Vector2[,] asteroidPosition = new Vector2[(int)details.cellLength,(int)details.cellLength];


		for(int i = (int)startingPos.x; i < endPos.x ; i++)
		{
			for(int j = (int)startingPos.y; j < endPos.y ; j++)
			{				
				float distance = Mathf.Sqrt( Mathf.Pow(i + transform.position.x,2) + Mathf.Pow(j + transform.position.y ,2));
				if(distance < maxDistance && distance > maxDistance / 10.0)
				{
					float degree;
					if( j != 0)
					{
						degree = Mathf.Rad2Deg * Mathf.Atan( i / j);
						if( i >= 0)
						{
							if(j < 0)
							{
								degree += 360.0f;
							}
						}
						else if(i < 0)
						{
							if(j > 0)
							{
								degree += 180.0f;
							}
							else if(j < 0)
							{
								degree += 270.0f;
							}
						}
					}
					else
					{
						if(i >= 0)
						{
							degree = 0;
						}
						else
						{
							degree = 180;
						}
					}
					
					if( !details.invalidSpawnPoints.Contains(new Vector2(i,j)))
					{
						float xCoord = ((i + transform.position.x) + details.mapLength /2) / (float)details.mapLength * 25.6f;
						float yCoord = ((j + transform.position.y) + details.mapLength /2) / (float)details.mapLength * 25.6f;						
						float scale = Mathf.PerlinNoise(xCoord,yCoord);

						if(scale > 0.4f)
						{
							asteroidPosition[i + ((int)details.cellLength/2 ),j + ((int)details.cellLength/2 )].Set(i + transform.position.x, j + transform.position.y);
							perlinValue[(i + ((int)details.cellLength/2))* (int)details.cellLength + (j+ ((int)details.cellLength/2))] = scale;
						}
					}
				}
			}
		}
		


		XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);

		writer.WriteStartDocument();
		writer.WriteWhitespace("\n");
		writer.WriteStartElement("Root");
		writer.WriteWhitespace("\n");

		for(int i = 0,c = 0; i < (int)details.cellLength ; i++)
		{
			for(int j = 0; j < (int)details.cellLength; j++, c++)
			{
				if(!Vector2.Equals(asteroidPosition[i,j],Vector2.zero))
				{
					writer.WriteWhitespace("\t");
					writer.WriteStartAttribute("AsteroidPosition");
					writer.WriteAttributeString("AsteroidPosition",asteroidPosition[i,j].x.ToString());
					writer.WriteAttributeString("AstroidPosition", asteroidPosition[i,j].y.ToString());
					writer.WriteWhitespace("\n\t\t");
					writer.WriteElementString("PerlinValue", perlinValue[c].ToString());
					writer.WriteWhitespace("\n");
				}
			}
		}

		writer.WriteEndDocument();
		writer.Close ();
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
			writer.WriteWhitespace("\t");
			writer.WriteElementString("AsteroidPosition", child.transform.position.x.ToString() + "," + child.transform.position.y.ToString());
			writer.WriteWhitespace("\n\t\t");
			writer.WriteElementString("PerlinValue", child.perlinValue.ToString());
			writer.WriteWhitespace("\n");
		}
		
		writer.Close ();
	}

	//loads all the objects in the cell
	public void Load()
	{
		List<Vector2> positions = new List<Vector2>();
		List<float> perlin = new List<float>();

		XmlTextReader reader = new XmlTextReader(fileName);

		while(reader.Read())
		{
			Debug.Log("Reading..");
			if(reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
			{
				switch(reader.Name)
				{
					case "AsteroidPosition" :
						Debug.Log(reader.ReadElementString());
						string element = reader.ReadElementString();
						string[] seperatedString = element.Split(new char[]{','}, 2);
						Debug.Log(seperatedString[0]);
						positions.Add(new Vector2(float.Parse(seperatedString[0]), float.Parse(seperatedString[1])));
						break;

					case "PerlinValue":
						perlin.Add( float.Parse(reader.ReadElementString()));
						break;
				}
			}
		}
		Debug.Log("Done reading");
		reader.Close ();

		Debug.Log("Generating stuff");

		int associatedPerlinPosition = 0;
		foreach(Vector2 asteroidPosition in positions)
		{
			GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
			game.transform.position = (transform.position + (Vector3)asteroidPosition);
			game.transform.parent = parent.transform;
			game.AddComponent<Asteroid>().perlinValue = perlin[associatedPerlinPosition];
			associatedPerlinPosition++;
		}
	}
}
