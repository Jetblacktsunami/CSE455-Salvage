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
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace ParkerSpaceSystem 
{
	public class WorldGenerator : MonoBehaviour 
	{
		public static Vector2[,] NoSpawnAreas;
		private static WorldGenerator instance;
		public static WorldSpecs worldspec = new WorldSpecs();

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
			public Vector2 start;
			public float degreeJumpStep;
			public List<Vector2> invalidSpawnPoints;
		}

		/// <summary>
		/// Use to Procedurally generate a space world with a random seed
		/// length x width
		/// </summary>
		public void GenerateSpace( int mapLength , int numberOfSubdivisions, Vector2 startingVector, string SpaceName )
		{
			//worldspec = new WorldSpecs ();
			worldspec.spaceName = SpaceName;
			worldspec.spaceArea = mapLength * mapLength;
			worldspec.mapLength = mapLength;
			worldspec.cellLength = SquareMathCalculations.FindSmallestDivisionSideLength(worldspec.mapLength, numberOfSubdivisions);  
			worldspec.start = startingVector;
			worldspec.degreeJumpStep = 1.0f;
			worldspec.invalidSpawnPoints = new List<Vector2>();

			for(float degree = 0; degree < 360; degree+= worldspec.degreeJumpStep)
			{
				float r0 = 5 * Mathf.Cos( 9 * degree) + (worldspec.mapLength * 0.20f); //degree / 4; //3*Mathf.Cos(6 * degree) + 15.0f;
				//float r1 =  Mathf.Cos(16 * degree ) + (degree / 5.0f);
				
				//sohcahtoa	
				worldspec.invalidSpawnPoints.Add(new Vector2( r0 * Mathf.Cos(degree), r0 * Mathf.Sin(degree)));
				//invalidSpawnPoints.Add(new Vector2( r1 * Mathf.Cos(degree), r1 * Mathf.Sin(degree)));
			}

			for(int i = 0, j = 1 ; i < worldspec.invalidSpawnPoints.Count; i++)
			{
				Debug.DrawLine( worldspec.invalidSpawnPoints[i], worldspec.invalidSpawnPoints[j], Color.green,2000);
				j = (j + 1) % worldspec.invalidSpawnPoints.Count;
			}
			CreateCells (worldspec);

		}

		public void SaveSpace( string filename )
		{
			
		}

		public void LoadSpace( string filename )
		{

		}


		public void BeginJourney( string filename)
		{

		}

		public static void CreateCells(WorldSpecs details)
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
					cell.AddComponent<WorldCell>().Activate();
					cell.AddComponent<BoxCollider2D>().isTrigger = true;

				}
			}
		}
	}
}
