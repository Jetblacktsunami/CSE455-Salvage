using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour 
{
	public enum Type{ rock, iron, gold, iridium };
	public Type asteroidType = Type.rock;

	public float perlinValue = 1.0f;
	public float durability = 100.0f;
	public WorldCell parentCell;
	public Vector2 assignedPosition = new Vector2(0,0);

	public void Change()
	{
		gameObject.transform.position = (Vector3)(assignedPosition + new Vector2(Random.Range(-1.0f, 1.0f),Random.Range(-1.0f, 1.0f)));
		gameObject.transform.localScale = new Vector3(perlinValue/10.0f ,perlinValue/10.0f, 1.0f);
		durability = 100f * perlinValue;
	}
	
	public void DestroySelf()
	{
		parentCell.RemoveAsteroidPosition(assignedPosition);
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
