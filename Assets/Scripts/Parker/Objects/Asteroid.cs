using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour 
{
	public enum Type{ rock, iron, gold, iridium };
	public Type asteroidType = Type.rock;


	public float rotationSpeed = 10.0f;
	public float perlinValue = 1.0f;
	public float durability = 100.0f;
	public bool destroyYourself = false;
	public WorldCell parentCell;
	private Vector3 rotationVector = new Vector3();

	public void Change()
	{
		gameObject.transform.localScale = new Vector3(perlinValue / 10.0f ,perlinValue / 10.0f,1.0f);
	}

	public void Start()
	{
		rotationVector.Set (0.0f, 0.0f, rotationSpeed);
	}
	public void Update()
	{
		if(rotationSpeed != 0)
		{
			transform.localEulerAngles += new Vector3 (0.0f, 0.0f, rotationSpeed * Time.deltaTime);
		}

		if (destroyYourself) 
		{
			DestroySelf();
		}
	}

	private void OnEnable()
	{
		rotationSpeed = rotationSpeed * Random.Range (-1, 1);
		rotationVector.Set (0.0f, 0.0f, rotationSpeed);
	}

	public void DestroySelf()
	{
		parentCell.RemoveAsteroid (this);
		Destroy (gameObject);
	}
}
