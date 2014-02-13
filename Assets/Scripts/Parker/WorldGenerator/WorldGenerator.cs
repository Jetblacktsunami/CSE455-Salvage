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

namespace ParkerSpaceSystem 
{
	public class WorldGenerator : MonoBehaviour 
	{
		private static WorldGenerator instance;
		public static WorldSpecs worldspec = new WorldSpecs();
		public enum ActionType{ load, saved, nothing };
		public static Action<ActionType> worldDoneLoading;
		public List<GameObject> Planets = new List<GameObject> ();

#if UNITY_EDITOR
		public static string mainDirectory = Application.dataPath + "/SaveData/";
		public static string directory = Application.dataPath + "/SaveData/";
#else
		public static string mainDirectory = Application.persistentDataPath + "/SaveData/";
		public static string directory = Application.persistentDataPath + "/SaveData/";
#endif

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
			public List<Vector2> invalidSpawnPoints;
		}

		/// <summary>
		/// Generates the space.
		/// </summary>
		/// <param name="details">Details.</param>
		public void GenerateSpace( WorldSpecs details)
		{
			worldspec = details;
			CreateCells (details);
		}

		/// <summary>
		/// Use to Procedurally generate a space world with a random seed
		/// length x width
		/// </summary>
		public void GenerateSpace( int mapLength , int numberOfSubdivisions, Vector2 startingVector, string SpaceName, int seed)
		{
			//worldspec = new WorldSpecs ();
			worldspec.spaceName = SpaceName;
			worldspec.spaceArea = mapLength * mapLength;
			worldspec.mapLength = mapLength;
			worldspec.cellLength = SquareMathCalculations.FindSmallestDivisionSideLength(worldspec.mapLength, numberOfSubdivisions);  
			worldspec.start = startingVector;
			worldspec.degreeJumpStep = 1.0f;
			worldspec.seed = seed;
			worldspec.subdivisions = numberOfSubdivisions;
			worldspec.invalidSpawnPoints = new List<Vector2>();

			for(float degree = 0; degree < 360; degree+= worldspec.degreeJumpStep)
			{
				float r0 = 5 * Mathf.Cos( 9 * degree) + (worldspec.mapLength * 0.20f); //degree / 4; //3*Mathf.Cos(6 * degree) + 15.0f;
				//float r1 =  Mathf.Cos(16 * degree ) + (degree / 5.0f);
				
				//sohcahtoa	
				worldspec.invalidSpawnPoints.Add(new Vector2( r0 * Mathf.Cos(degree), r0 * Mathf.Sin(degree)));
				//worldspec.invalidSpawnPoints.Add(new Vector2( r1 * Mathf.Cos(degree), r1 * Mathf.Sin(degree)));
			}

			for(int i = 0, j = 1 ; i < worldspec.invalidSpawnPoints.Count; i++)
			{
				Debug.DrawLine( worldspec.invalidSpawnPoints[i], worldspec.invalidSpawnPoints[j], Color.green,2000);
				j = (j + 1) % worldspec.invalidSpawnPoints.Count;
			}
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
			List<WorldSpecs> existingSpecs = new List<WorldSpecs>();
			XmlTextReader reader = new XmlTextReader(directory + "CreatedWorlds.xml");
			
			while(reader.Read())
			{
				if(reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
				{
					switch(reader.Name)
					{
						case "WorldSpec":
							if(reader.AttributeCount == 10)
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
					cell.AddComponent<WorldCell>();
					cell.AddComponent<BoxCollider2D>().isTrigger = true;
					worldspec.totalNumberOfCells++;
				}
			}
			if(worldDoneLoading != null)
			{
				worldDoneLoading(ActionType.load);
			}
		}
	}
}
