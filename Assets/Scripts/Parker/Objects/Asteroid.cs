using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour 
{
	public enum Type{ rock, iron, gold, iridium };
	public Type asteroidType = Type.rock;
	
	public float perlinValue = 1.0f;
	public float durability = 100.0f;

	public void Change()
	{
		gameObject.transform.localScale = new Vector3(perlinValue / 10.0f ,perlinValue / 10.0f,1.0f);
	}

}
