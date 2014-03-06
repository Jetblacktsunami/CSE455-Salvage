/******************************
*  Created By : Gerardo Parker
* 
* 
* 
* 
* 
* 
* 
* 
* ***************************/
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class WorldGenerator : MonoBehaviour 
{
	private static WorldGenerator instance;
	public static WorldSpecs worldspec = new WorldSpecs();
	public enum ActionType{ load, saved, nothing };
	public static Action<ActionType> worldDoneLoading;
	public static string directory;// = Application.dataPath + "/SaveData/";

	public List<GameObject> planets = new List<GameObject> ();
	public GameObject sun;

	/// <summary>
	/// Gets or sets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static WorldGenerator Instance
	{
		get
		{
			if(!instance)
			{
				GameObject WorldGen = new GameObject("WorldGenerator");
				instance = WorldGen.AddComponent<WorldGenerator>();
			}

			return instance;
		}
		set
		{
			if(!instance || instance == value)
			{
				instance = value;
			}
			else
			{
				Destroy(value);
			}
		}
	}

	void Awake()
	{
		#if UNITY_EDITOR
		directory = Application.dataPath + "/SaveData/";
		#else
		directory = Application.persistentDataPath + "/SaveData/";
		#endif
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		Instance = this;
	}


	public struct WorldSpecs
	{
		public string spaceName;
		public int spaceArea;
		public int mapLength;
		public float cellLength;
		public int totalNumberOfCells;
		public Vector2 start;
		public float degreeJumpStep;
		public int seed;
		public int subdivisions;
		public Vector2[] planetPositions;
		public List<Vector2> invalidSpawnPoints;
	}

	/// <summary>
	/// Generates the space.
	/// </summary>
	/// <param name="details">Details.</param>
	public void GenerateSpace( WorldSpecs details)
	{
		worldspec = details;
		CreateCells (worldspec);
		SpawnPlanets ();

	}

	/// <summary>
	/// Use to Procedurally generate a space world with a random seed
	/// length x width
	/// </summary>
	public void GenerateSpace( int mapLength , int numberOfSubdivisions, Vector2 startingVector, string SpaceName, int seed)
	{
		worldspec = default(WorldSpecs);
		worldspec.spaceName = SpaceName;
		worldspec.spaceArea = mapLength * mapLength;
		worldspec.mapLength = mapLength;
		worldspec.cellLength = SquareMathCalculations.FindSmallestDivisionSideLength(worldspec.mapLength, numberOfSubdivisions);  
		worldspec.start = startingVector;
		worldspec.degreeJumpStep = 1.0f;
		worldspec.seed = seed;
		worldspec.subdivisions = numberOfSubdivisions;
		worldspec.invalidSpawnPoints = new List<Vector2>();
		worldspec.totalNumberOfCells = numberOfSubdivisions * numberOfSubdivisions;

		if(planets.Count > 0)
		{
			worldspec.planetPositions = new Vector2[planets.Count];
			float jumpStep = ((float)worldspec.mapLength) / (float)planets.Count;
			float jump = 0f;
			for(int i = 0 ; i < planets.Count; i++, jump += jumpStep)
			{
				// r =  distance + previous distance
reroll:			float r = jump;
				float theta = UnityEngine.Random.Range( 1f, 89f);
				Vector2 tempPos = new Vector2(-(float)mapLength/2.0f, -(float)worldspec.mapLength/2.0f);
				tempPos.x = tempPos.x + (Mathf.Abs(Mathf.Cos(theta) * r));
				tempPos.y = tempPos.x + (Mathf.Abs(Mathf.Sin(theta) * r));

				float offsetFromCenter = (tempPos.x % (worldspec.cellLength));
				if(offsetFromCenter != (worldspec.cellLength/2.0f))
				{
					tempPos.x = (tempPos.x - (offsetFromCenter)) + (worldspec.cellLength/2.0f);
				}

				offsetFromCenter = (tempPos.y % (worldspec.cellLength));
				if(offsetFromCenter != (worldspec.cellLength/2.0f))
				{
					tempPos.y = (tempPos.y - (offsetFromCenter)) + (worldspec.cellLength/2.0f);
				}
				if(isPlanetInRange(tempPos) || Vector2.Distance(Vector2.zero,tempPos) <= worldspec.cellLength)
				{
					Debug.Log("had to reroll");
					goto reroll;
				}
				worldspec.planetPositions[i] = tempPos;
			}

		}

		for(float degree = 0; degree < 360; degree+= worldspec.degreeJumpStep)
		{
			float r0 = 5 * Mathf.Cos( 9 * degree) + (worldspec.mapLength * 0.20f); //degree / 4; //3*Mathf.Cos(6 * degree) + 15.0f;
			worldspec.invalidSpawnPoints.Add(new Vector2( r0 * Mathf.Cos(degree), r0 * Mathf.Sin(degree)));
		}

		SpawnPlanets ();
		CreateCells (worldspec);

		SaveSpace();
	}

	/// <summary>
	/// Saves the space.
	/// </summary>
	public static void SaveSpace()
	{
		bool directoryExists = Directory.Exists(directory);
		bool fileExists = File.Exists(directory + "CreatedWorlds.xml");

		if(directoryExists && fileExists)
		{
			List<WorldSpecs> existingSpecs = GetCreatedWorlds();
			bool skipSaving = false;
			foreach(WorldSpecs obj in existingSpecs)
			{
				if(obj.spaceName == worldspec.spaceName)
				{
					skipSaving = true;
				}
			}
			if(!skipSaving)
			{
				existingSpecs.Add(worldspec);
				XmlTextWriter writer = new XmlTextWriter( directory + "CreatedWorlds.xml" , System.Text.Encoding.UTF8);
				writer.WriteStartDocument();
				writer.WriteStartElement("Root");
				writer.WriteWhitespace("\n");
				foreach(WorldSpecs obj in existingSpecs)
				{
					writer.WriteWhitespace("\t");
					writer.WriteStartElement("WorldSpec");
					writer.WriteAttributeString("name", obj.spaceName);
					writer.WriteAttributeString("area", obj.spaceArea.ToString());
					writer.WriteAttributeString("spaceLength", obj.mapLength.ToString());
					writer.WriteAttributeString("cellLength", obj.cellLength.ToString());
					writer.WriteAttributeString("start-x", obj.start.x.ToString());
					writer.WriteAttributeString("start-y",obj.start.y.ToString());
					writer.WriteAttributeString("degreeJump", obj.degreeJumpStep.ToString());
					writer.WriteAttributeString("subdivisions", obj.subdivisions.ToString());
					writer.WriteAttributeString("numOfCells", obj.totalNumberOfCells.ToString());
					writer.WriteAttributeString("Seed", obj.seed.ToString());
					if(obj.planetPositions.Length > 0)
					{
						for(int i = 0; i < obj.planetPositions.Length; i++)
						{
							writer.WriteAttributeString("pPos-x" + i,obj.planetPositions[i].x.ToString());
							writer.WriteAttributeString("pPos-y" + i,obj.planetPositions[i].y.ToString());
						}
					}
					writer.WriteEndElement();
					writer.WriteWhitespace("\n");
				}
				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Close();

			}
			//read the entire xml data populate it into a list and several data fields to resave
		}
		else if(directoryExists && !fileExists)
		{
			XmlTextWriter writer = new XmlTextWriter( directory + "CreatedWorlds.xml" , System.Text.Encoding.UTF8);

			writer.WriteStartDocument();
			writer.WriteStartElement("Root");
			writer.WriteWhitespace("\n\t");
			writer.WriteStartElement("WorldSpec");
			writer.WriteAttributeString("name", worldspec.spaceName.ToString());
			writer.WriteAttributeString("area", worldspec.spaceArea.ToString());
			writer.WriteAttributeString("spaceLength", worldspec.mapLength.ToString());
			writer.WriteAttributeString("cellLength", worldspec.cellLength.ToString());
			writer.WriteAttributeString("start-x", worldspec.start.x.ToString());
			writer.WriteAttributeString("start-y",worldspec.start.y.ToString());
			writer.WriteAttributeString("degreeJump", worldspec.degreeJumpStep.ToString());
			writer.WriteAttributeString("subdivisions", worldspec.subdivisions.ToString());
			writer.WriteAttributeString("numOfCells", worldspec.totalNumberOfCells.ToString());
			writer.WriteAttributeString("Seed", worldspec.seed.ToString());
			if(worldspec.planetPositions.Length > 0)
			{
				for(int i = 0; i < worldspec.planetPositions.Length; i++)
				{
					writer.WriteAttributeString("pPos-x" + i,worldspec.planetPositions[i].x.ToString());
					writer.WriteAttributeString("pPos-y" + i,worldspec.planetPositions[i].y.ToString());
				}
			}
			writer.WriteEndElement();
			writer.WriteWhitespace("\n");
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Close();
			//create just the new file with the new world
		}
		else if(!directoryExists && !fileExists) 
		{
			Directory.CreateDirectory(directory);

			XmlTextWriter writer = new XmlTextWriter( directory + "CreatedWorlds.xml" , System.Text.Encoding.UTF8);
			
			writer.WriteStartDocument();

			writer.WriteStartElement("Root");
			writer.WriteWhitespace("\n\t");
			writer.WriteStartElement("WorldSpec");
			writer.WriteAttributeString("name", worldspec.spaceName);
			writer.WriteAttributeString("area", worldspec.spaceArea.ToString());
			writer.WriteAttributeString("spaceLength", worldspec.mapLength.ToString());
			writer.WriteAttributeString("cellLength", worldspec.cellLength.ToString());
			writer.WriteAttributeString("start-x", worldspec.start.x.ToString());
			writer.WriteAttributeString("start-y",worldspec.start.y.ToString());
			writer.WriteAttributeString("degreeJump", worldspec.degreeJumpStep.ToString());
			writer.WriteAttributeString("subdivisions", worldspec.subdivisions.ToString());
			writer.WriteAttributeString("numOfCells", worldspec.totalNumberOfCells.ToString());
			writer.WriteAttributeString("Seed", worldspec.seed.ToString());
			if(worldspec.planetPositions.Length > 0)
			{
				for(int i = 0; i < worldspec.planetPositions.Length; i++)
				{
					writer.WriteAttributeString("pPos-x" + i,worldspec.planetPositions[i].x.ToString());
					writer.WriteAttributeString("pPos-y" + i,worldspec.planetPositions[i].y.ToString());
				}
			}
			writer.WriteEndElement();
			writer.WriteWhitespace("\n");
			writer.WriteEndElement();

			writer.WriteEndDocument();
			writer.Close();
			//create both the director and the file
		}
	}

	/// <summary>
	/// Gets the created worlds.
	/// </summary>
	/// <returns>The created worlds.</returns>
	public static List<WorldSpecs> GetCreatedWorlds()
	{
		if(File.Exists(directory + "CreatedWorlds.xml"))
		{
			List<WorldSpecs> existingSpecs = new List<WorldSpecs> ();
			XmlTextReader reader = new XmlTextReader(directory + "CreatedWorlds.xml");
			
			while(reader.Read())
			{
				if(reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
				{
					switch(reader.Name)
					{
						case "WorldSpec":
							if(reader.AttributeCount >= 10)
							{
								WorldSpecs tempSpec = new WorldSpecs();
								tempSpec.spaceName = reader.GetAttribute(0);	
								tempSpec.spaceArea = int.Parse(reader.GetAttribute(1));
								tempSpec.mapLength = int.Parse(reader.GetAttribute(2));
								tempSpec.cellLength = float.Parse(reader.GetAttribute(3));
								tempSpec.start = new Vector2(float.Parse(reader.GetAttribute(4)),float.Parse(reader.GetAttribute(5)));
								tempSpec.degreeJumpStep = float.Parse(reader.GetAttribute(6));
								tempSpec.subdivisions = int.Parse(reader.GetAttribute(7));
								tempSpec.totalNumberOfCells = int.Parse(reader.GetAttribute(8));
								tempSpec.seed = int.Parse(reader.GetAttribute(9));
								tempSpec.planetPositions = new Vector2[(reader.AttributeCount - 10) / 2];
								Debug.Log("Planet Positions: " + tempSpec.planetPositions.Length);
								if(reader.AttributeCount > 11)
								{
									float maxPosition = (reader.AttributeCount - 10)/2.0f;
									int maxP = Mathf.CeilToInt(maxPosition);
									for(int i = 0, j = 0; i < maxP; j++) 
									{
										tempSpec.planetPositions[j].Set(float.Parse(reader.GetAttribute(i+10)), float.Parse(reader.GetAttribute(i+11)));
									 	i+=2;
									}
								}
								existingSpecs.Add(tempSpec);
							}
							else
							{
								Debug.Log("Data is missing from 1 of the worlds. Not Saving it anymore");
							}
							break;
						case "Root":
							Debug.Log("Root found");
							break;
						default:
							Debug.Log(reader.Name + " : possible invalid data in save file ignoring, please review file");
							break;
					}
				}
			}

			reader.Close();
			return existingSpecs;
		}
		else 
		{
			return new List<WorldSpecs>(1);
		}
	}


	private static void SpawnPlanets()
	{
		int count = WorldGenerator.Instance.planets.Count;
		GameObject parent = new GameObject ("Planets");
		parent.transform.position = Vector3.zero;

		for(int i = 0; i < count; i++)
		{
			GameObject planet = GameObject.Instantiate(WorldGenerator.Instance.planets[i]) as GameObject;
			planet.transform.position = worldspec.planetPositions[i];
			planet.transform.parent = parent.transform;
			planet.transform.localScale = new Vector3(worldspec.cellLength * 0.1f,worldspec.cellLength * 0.1f,1.0f);
		}

		GameObject sun = GameObject.Instantiate (WorldGenerator.Instance.sun) as GameObject;
		sun.transform.position = Vector3.zero;
		sun.transform.parent = parent.transform;
		sun.transform.localScale = new Vector3(worldspec.cellLength * 0.1f,worldspec.cellLength * 0.1f,1.0f);
	}

	/// <summary>
	/// Creates the cells.
	/// </summary>
	/// <param name="details">Details.</param>
	private static void CreateCells(WorldSpecs details)
	{
		Vector2 startPoint = new Vector2(details.start.x - (details.mapLength * 0.5f), details.start.y - (details.mapLength * 0.5f));

		GameObject parent = new GameObject (details.spaceName);
		parent.transform.position = Vector2.zero;


		for(float i = 0, x = 0; i < details.mapLength ; i += details.cellLength, x++)
		{
			for(float j = 0, y = 0; j < details.mapLength; j += details.cellLength, y++)
			{
				GameObject cell = new GameObject ( x + "," +  y );
				cell.transform.parent = parent.transform;
				cell.transform.localScale = new Vector3(details.cellLength ,details.cellLength,1.0f);
				cell.transform.position = new Vector2(startPoint.x + i + (details.cellLength/2.0f) , startPoint.y + j + (details.cellLength / 2.0f) );
				if(isPlanetInRange(cell.transform.position))
				{
					cell.AddComponent<WorldCell>().hasPlanet = true;
				}
				else
				{
					cell.AddComponent<WorldCell>();
				}
				cell.AddComponent<BoxCollider2D>().isTrigger = true;
			}
		}
		if(worldDoneLoading != null)
		{
			worldDoneLoading(ActionType.load);
		}
	}

	private static bool isPlanetInRange(Vector2 currentPosition)
	{
		foreach(Vector2 position in worldspec.planetPositions)
		{
			if(Vector2.Distance(currentPosition,position) < (worldspec.cellLength /2.0f))
			{
				return true;
			}
		}

		return false;
	}
}

