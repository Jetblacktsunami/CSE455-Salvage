using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour 
{
	public enum Type{ rock, iron, gold, iridium };
	public Type asteroidType = Type.rock;

	public float perlinValue = 1.0f;
	public float durability = 100.0f;
	public WorldCell parentCell;

	public void Change()
	{
		durability = 100f * perlinValue;
	}
	
	public void DestroySelf()
	{
		parentCell.RemoveAsteroid (this);
		ObjectPool.Pool.Unregister (this.gameObject);
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
