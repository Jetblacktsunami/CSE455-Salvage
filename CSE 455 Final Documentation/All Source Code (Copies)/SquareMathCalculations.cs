using UnityEngine;
using System.Collections;

public class SquareMathCalculations
{
	/// <summary>
	/// Finds the size of the smallest division.
	/// Feed In values squared in order to find a single size
	/// </summary>
	public static float FindSmallestDivisionSideLength( float length , int numberOfDivisions )
	{
		float currentLength = length;
		if(numberOfDivisions <= 0)
		{
			return currentLength; 
		}

		return currentLength / numberOfDivisions;
	}
	

	public static float FindSmallestDivisionSideLengthWithWeirdFX( float area , int numberOfDivisions )
	{
		float currentArea = area;
		
		if (numberOfDivisions <= 1) 
		{
			return Mathf.Sqrt(currentArea);
		} 
		else
		{
			currentArea /= 4.0f;
			return FindSmallestDivisionSideLength(currentArea, numberOfDivisions - 1);
		}
	}

	public static float Noise(int x, int y)
	{
		int n = x + y * 57;
		n = (n << 13) ^ n;
		return (1.0f - ((n * ( n * n * 15731 + 1376312589) + 1376312589) & 0x7ffffffff) / 1073741824.0f);
	}
}
