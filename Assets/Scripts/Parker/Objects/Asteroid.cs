using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour 
{
	public enum Type{ rock, iron, gold, iridium };
	public Type asteroidType = Type.rock;

	public float rotationSpeed = 10.0f;
	public float perlinValue = 1.0f;
	public float durability = 100.0f;
	public WorldCell parentCell;

	private Vector3 rotationVector = new Vector3();

	public void Change()
	{
		gameObject.transform.localScale = new Vector3(perlinValue / 10.0f ,perlinValue / 10.0f,1.0f);
		durability = 100f * perlinValue;
	}
	
	public void DestroySelf()
	{
		parentCell.RemoveAsteroid (this);
		Destroy (gameObject);
	}

	public void Damage(float amount)
	{
		durability -= amount;
		if(durability <= 0f)
		{
			DestroySelf();
		}
	}
}
