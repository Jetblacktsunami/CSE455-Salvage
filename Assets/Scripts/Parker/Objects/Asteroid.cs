using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour 
{
	public enum Type{ rock, iron, gold, iridium };
	public Type asteroidType = Type.rock;

	public float perlinValue = 1.0f;
	public float durability = 100.0f;



}
