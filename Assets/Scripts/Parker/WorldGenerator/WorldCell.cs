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
	private enum CellStatus{ active, standby, init };
	private CellStatus status = CellStatus.standby;

	public GameObject parent;
	private string worldName; //name of the generated world
	private string cellName; //id of this cell
	private string fileName; //the exact file we are either saving or loading from
	private bool startRan = false; //has the start function ran this game for this cell
	public bool deactivateNow = false;
	public bool activateNow = false;
	public bool hasPlanet = false;
	#pragma warning disable 0414
	private float distanceFromCenter = 0.0f; //this is going to be used to add varience in the spawned asteriods
	#pragma warning restore
	private List<Asteroid> children = new List<Asteroid>();
	#endregion

	public void Update()
	{
		if(GameManager.Instance.playerObject)
		{
			float distance = Vector3.Distance(gameObject.transform.position, GameManager.Instance.playerObject.transform.position);
			float cellSize = gameObject.transform.localScale.x;
			if(distance <= (cellSize * 2) + 2.0f && status != CellStatus.active)
			{
				Activate();
				status = CellStatus.active;
			}
			else if(distance > (cellSize * 2)+ 2.0f && status != CellStatus.standby && status != CellStatus.init)
			{
				Deactivate();
				status = CellStatus.standby;
			}
		}
	}
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
		Start();

		if (Directory.Exists(directory) && File.Exists(fileName)) 
		{
			Debug.Log("Loading");
			Load ();
		}
		else
		{
			GenerateXMLData();
			Load();
		}
	}

	//turns the object off
	public void Deactivate()
	{
		AutoSave ();
		if(gameObject.transform.childCount > 0)
		{
			for(int i = 0;  i < gameObject.transform.childCount; i++)
			{
				gameObject.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public void GarbageCollect()
	{
		if(gameObject.transform.childCount > 0)
		{
			for(int i = 0; i < transform.childCount; i++)
			{
				if(gameObject.transform.GetChild(i).gameObject.name == "Asteroid")
				{
					Destroy(gameObject.transform.GetChild(i).gameObject);
				}
			}
		}
	}

	//Called when object is enabled
	public void OnEnable()
	{
		GameManager.gameManagerCalls += GameManagerEventHandler;
	}

	//Called when disabled
	public void OnDisable()
	{
		GameManager.gameManagerCalls -= GameManagerEventHandler;
	}

	public void GameManagerEventHandler(GameManager.FunctionCallType type)
	{
		if(type == GameManager.FunctionCallType.save)
		{
			Save();
		}
		else if(type == GameManager.FunctionCallType.load)
		{
			Load();
		}
	}
	//if this is the first time being activated the cell will call this to spawn the asteroids
	public void GenerateXMLData ()
	{
		Start ();
		Debug.Log ("Generating xml");
		if(!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}
		if(!hasPlanet)
		{
			WorldGenerator.WorldSpecs details = WorldGenerator.worldspec;

			Random.seed = details.seed;
			float maxDistance = details.mapLength / 2.0f;
			float halfCellLength = Mathf.Ceil(details.cellLength/2.0f);
			float[] perlinValue = new float[((int)halfCellLength * 2) * ((int)halfCellLength * 2)];

			Vector2 startingPos = new Vector2( -halfCellLength, -halfCellLength);
			Vector2 endPos = new Vector2( halfCellLength, halfCellLength);
			Vector2[,] asteroidPosition = new Vector2[(int)halfCellLength * 2,(int)halfCellLength * 2];

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

						if( details.invalidSpawnPoints == null || !details.invalidSpawnPoints.Contains(new Vector2(i,j)))
						{
							float xCoord = ((i + transform.position.x) + details.mapLength /2) / (float)details.mapLength * 25.6f;
							float yCoord = ((j + transform.position.y) + details.mapLength /2) / (float)details.mapLength * 25.6f;						
							float scale = Mathf.PerlinNoise(xCoord,yCoord);

							if(scale > 0.95f || scale < 0.1f || (scale > 0.45f && scale < 0.5f))
							{
	//							int x,y;
	//							x = i + ((int)halfCellLength);
	//							y = j + ((int)halfCellLength);
								asteroidPosition[i + ((int)halfCellLength),j + ((int)halfCellLength )].Set(i + transform.position.x, j + transform.position.y);
								perlinValue[(i + ((int)halfCellLength)) * (int)(halfCellLength * 2) + (j+ ((int)halfCellLength))] = scale;
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
						writer.WriteStartElement("AsteroidPosition");
						writer.WriteAttributeString("x ",asteroidPosition[i,j].x.ToString());
						writer.WriteAttributeString("y ",asteroidPosition[i,j].y.ToString());
						writer.WriteEndElement();
						writer.WriteWhitespace("\n\t\t");
						writer.WriteElementString("PerlinValue", perlinValue[c].ToString());
						writer.WriteWhitespace("\n");
					}
				}
			}

			writer.WriteEndDocument();
			writer.Close ();
		}
		else
		{
			XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);
			
			writer.WriteStartDocument();
			writer.WriteWhitespace("\n");
			writer.WriteStartElement("Root");
			writer.WriteWhitespace("\n");
			writer.WriteEndDocument();
			writer.Close ();
		}
	}

	//saves the current objects in the cell as well as their positions
	public void Save()
	{
		if(!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}
		if(File.Exists(fileName))
		{
			XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);

			writer.WriteStartDocument();
			writer.WriteWhitespace("\n");
			writer.WriteStartElement("Root");
			writer.WriteWhitespace("\n");

			foreach(Asteroid child in children)
			{
				writer.WriteWhitespace("\t");
				writer.WriteStartElement("AsteroidPosition");
				writer.WriteAttributeString("x ",child.transform.position.x.ToString());
				writer.WriteAttributeString("y ",child.transform.position.y.ToString());
				writer.WriteEndElement();
				writer.WriteWhitespace("\n\t\t");
				writer.WriteElementString("PerlinValue", child.perlinValue.ToString());
				writer.WriteWhitespace("\n");
			}

			writer.WriteEndDocument ();
			writer.Close ();
		}
		GameManager.Instance.AddToSavePercentage();
	}

	public void AutoSave()
	{
		if(!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}
		if(File.Exists(fileName))
		{
			XmlTextWriter writer = new XmlTextWriter (fileName, System.Text.Encoding.UTF8);
			
			writer.WriteStartDocument();
			writer.WriteWhitespace("\n");
			writer.WriteStartElement("Root");
			writer.WriteWhitespace("\n");
			
			foreach(Asteroid child in children)
			{
				writer.WriteWhitespace("\t");
				writer.WriteStartElement("AsteroidPosition");
				writer.WriteAttributeString("x ",child.transform.position.x.ToString());
				writer.WriteAttributeString("y ",child.transform.position.y.ToString());
				writer.WriteEndElement();
				writer.WriteWhitespace("\n\t\t");
				writer.WriteElementString("PerlinValue", child.perlinValue.ToString());
				writer.WriteWhitespace("\n");
			}
			
			writer.WriteEndDocument ();
			writer.Close ();
		}
	}

	//loads all the objects in the cell
	public void Load()
	{
		GameObject child = transform.GetChild (0).gameObject;
		if(gameObject.transform.childCount <= 1 && child && child.transform.childCount == 0)
		{
			List<Vector2> positions = new List<Vector2>();
			List<float> perlin = new List<float>();

			XmlTextReader reader = new XmlTextReader(fileName);

			while(reader.Read())
			{
				if(reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
				{
					switch(reader.Name)
					{
						case "AsteroidPosition" :
							positions.Add(new Vector2(float.Parse(reader.GetAttribute(0)), float.Parse(reader.GetAttribute(1))));
							break;

						case "PerlinValue":
							perlin.Add( float.Parse(reader.ReadElementString()));
							break;
					}
				}
			}
			reader.Close ();
		
			int associatedPerlinPosition = 0;

			foreach(Vector2 asteroidPosition in positions)
			{
				GameObject game = GameObject.Instantiate(Resources.Load("Asteroid/Asteroid")) as GameObject;
				game.transform.position = (Vector3)(asteroidPosition + new Vector2(Random.Range(-1.0f, 1.0f),Random.Range(-1.0f, 1.0f)));
				game.transform.parent = parent.transform;
				Asteroid temp =	game.AddComponent<Asteroid>();
				temp.perlinValue = perlin[associatedPerlinPosition];
				temp.Change();
				temp.parentCell = this;
				associatedPerlinPosition++;
				children.Add(temp);
			}
		}
		else
		{
			Debug.Log(gameObject.transform.childCount);
			for(int i = 0; i < gameObject.transform.childCount ; i++)
			{
				child.SetActive(true);
			}
		}
	}

	public void RemoveAsteroid(Asteroid self)
	{
		if(children.Contains(self))
		{
			children.Remove(self);
		}
	}
}
