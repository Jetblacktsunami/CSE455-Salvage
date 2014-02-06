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
		}

		/// <summary>
		/// Use to Procedurally generate a space world with a random seed
		/// length x width
		/// </summary>
		public void GenerateSpace( int mapLength , int numberOfSubdivisions, Vector2 startingVector, string SpaceName )
		{
			WorldSpecs spec = new WorldSpecs ();
			spec.spaceName = SpaceName;
			spec.spaceArea = mapLength * mapLength;
			spec.mapLength = mapLength;
			spec.cellLength = SquareMathCalculations.FindSmallestDivisionSideLength(spec.mapLength, numberOfSubdivisions);  
			spec.start = startingVector;
			spec.degreeJumpStep = 1.0f;

			CreateCells (spec);
			CreateAsteroids(spec);

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

		public static void CreateAsteroids(WorldSpecs details)
		{
			int startingXpos, startingYpos;
			startingXpos = startingYpos = (details.mapLength / 2) * -1;
			float maxDistance = details.mapLength / 2.0f;

			Color[] transforms = new Color[details.mapLength * details.mapLength];
			GameObject parent = new GameObject ("Asteroids");
			GameObject[,] asteroids = new GameObject[details.mapLength,details.mapLength];
			List<Vector2> bowwow = new List<Vector2>();

			NoSpawnAreas = new Vector2[4,Mathf.CeilToInt(90f / details.degreeJumpStep)];
			for(float degree = 0; degree < 360; degree+= details.degreeJumpStep)
			{
				float r0 = degree / 4; //3*Mathf.Cos(6 * degree) + 15.0f;
				//float r1 = 3*Mathf.Cos(6 * (degree + details.degreeJumpStep)) + 50.0f;
				//sohcahtoa
				Vector2 position1 = new Vector2( r0 * Mathf.Cos(degree), r0 * Mathf.Sin(degree));
				//Vector2 position2 = new Vector2( r1 * Mathf.Cos(degree), r1 * Mathf.Sin(degree));

				bowwow.Add(position1);
				//Vector2 position1 = new Vector2(Mathf.Cos( 4 * degree) * (details.mapLength / 2.0f), Mathf.Sin( 4 * degree) * (details.mapLength / 2.0f));
				//Vector2 position2 = new Vector2(Mathf.Cos( 4 * (degree + details.degreeJumpStep)) * (details.mapLength / 2.0f), Mathf.Sin( 4 * (degree + details.degreeJumpStep)) * (details.mapLength / 2.0f));
			

			}

			for(int i = 0, j = 1 ; i < bowwow.Count; i++)
			{
				Debug.DrawRay( bowwow[i], bowwow[j], Color.green, 2000);
				j += 1 % bowwow.Count;
			}

			for(int i = startingXpos; i < -(startingXpos); i++)
			{
				for(int j = startingYpos; j < -(startingYpos); j++)
				{

					//float distance = Mathf.Sqrt( Mathf.Pow(i,2) * Mathf.Pow(j,2));
					float distance = Mathf.Sqrt( Mathf.Pow(i,2) + Mathf.Pow(j,2));
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

						if( !bowwow.Contains(new Vector2(i,j)))
						{
							Debug.Log(degree);
							float xCoord = (i + details.mapLength /2) / (float)details.mapLength * 25.6f;
							float yCoord = (j + details.mapLength /2) / (float)details.mapLength * 25.6f;
							float scale = Mathf.PerlinNoise(xCoord,yCoord);
							//float scale = Mathf.PerlinNoise(Mathf.Pow(xCoord,yCoord/xCoord),Mathf.Pow(yCoord,xCoord/yCoord));
							if(scale < 0.4f)
							{
								transforms[(i + (details.mapLength/2))* details.mapLength + (j+ (details.mapLength/2))] = new Color(scale,scale,scale);
								GameObject game =  GameObject.CreatePrimitive(PrimitiveType.Cube);
								game.transform.parent = parent.transform;
								game.transform.position = new Vector2(i , j);
								asteroids[i + (details.mapLength/2),j + (details.mapLength/2)] = game;
							}
						}
					}
				}
			}

			for(int i = 0,c = 0; i < details.mapLength ; i++)
			{
				for(int j = 0; j < details.mapLength; j++, c++)
				{
					if(asteroids[i,j])
					{
						asteroids[i,j].renderer.material.color = transforms[c];
					}
				}
			}

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
					cell.transform.localScale = new Vector2(details.cellLength ,details.cellLength);
					cell.transform.position = new Vector2(startPoint.x + i + (details.cellLength/2.0f) , startPoint.y + j + (details.cellLength / 2.0f) );
					cell.AddComponent<WorldCell>().Activate();
					cell.AddComponent<BoxCollider2D>();
				}
			}
		}
	}
}
