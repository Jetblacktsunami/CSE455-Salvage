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
using System.Xml;

namespace ParkerSpaceSystem 
{
	public class WorldGenerator : MonoBehaviour 
	{
		public struct WorldSpecs
		{
			public int spaceArea;
			public int mapLength;
			public float cellLength;
			public Vector2 start;
		}

		/// <summary>
		/// Use to Procedurally generate a space world with a random seed
		/// length x width
		/// </summary>
		public static void GenerateSpace( int mapLength , int numberOfSubdivisions, Vector2 startingVector )
		{
			WorldSpecs spec = new WorldSpecs ();
			spec.spaceArea = mapLength * mapLength;
			spec.mapLength = mapLength;
			spec.cellLength = SquareMathCalculations.FindSmallestDivisionSideLength(spec.mapLength, numberOfSubdivisions);  
			spec.start = startingVector;

			CreateCells (spec);

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

			GameObject parent = new GameObject ("SpaceCells");
			parent.transform.position = Vector2.zero;


			int referenceNumber = 0;
			for(float i = 0, x = 0; i < details.mapLength ; i += details.cellLength, x++)
			{
				for(float j = 0, y = 0; j < details.mapLength; j += details.cellLength, y++)
				{
					GameObject cell = new GameObject ( x + "," +  y );
					cell.transform.parent = parent.transform;
					cell.AddComponent<WorldCell>();
					cell.transform.localScale = new Vector2(details.cellLength ,details.cellLength);
					cell.transform.position = new Vector2(startPoint.x + i + (details.cellLength/2.0f) , startPoint.y + j + (details.cellLength / 2.0f) );
					cell.AddComponent<BoxCollider2D>();
					referenceNumber++;
				}
			}
		}
	}
}
