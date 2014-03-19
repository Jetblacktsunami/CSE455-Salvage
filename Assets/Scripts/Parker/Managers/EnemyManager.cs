using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour 
{
	public GameObject[] enemyTypes;

	public float spawnTimer = 10f;
	private float countDownTimer = 0f;

	public int maxEnemiesCount = 6;
	private int currentEnemyCount = 0;
	private bool bCanSpawnEnemies = false;

	private static EnemyManager eManager;
	public static EnemyManager Instance
	{
		get
		{
			return eManager;
		}
	}

	public void DecreaseEnemyCount()
	{
		currentEnemyCount--;
		if(currentEnemyCount < 0)
		{
			currentEnemyCount = 0;
		}
	}

	private void Start()
	{
		if(!eManager || eManager == this)
		{
			eManager = this;
		}
		else
		{
			Destroy(this);
		}
	}

	private void Update()
	{
		if(bCanSpawnEnemies)
		{
			if(currentEnemyCount < maxEnemiesCount)
			{
				if(countDownTimer <= 0f)
				{
					Vector2 playerPos = (Vector2)PlayerInformation.Instance.gameObject.transform.position;
					GameObject enemy = GameObject.Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length - 1)], playerPos + RandomEdgePosition(), Quaternion.identity) as GameObject ; 
					enemy.transform.localScale = new Vector3(0.5f,0.5f,1.0f);
					currentEnemyCount += 1;
					countDownTimer = spawnTimer;
				}

				countDownTimer -= Time.deltaTime;
			}
			else
			{
				countDownTimer = spawnTimer;
			}
		}
	}

	private void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "MainMenu")
		{
			bCanSpawnEnemies = false;
			countDownTimer = spawnTimer;
		}
		else if(Application.loadedLevelName == "InGame")
		{
			bCanSpawnEnemies = true;
			countDownTimer = spawnTimer;
		}
	}

	private Vector2 RandomEdgePosition()
	{
		Vector2 vect = new Vector2(WorldGenerator.worldspec.cellLength, WorldGenerator.worldspec.cellLength);

		Vector2 signs = new Vector2 (RandomSign (), RandomSign ());
		signs [Random.Range (0, 1)] *= Random.Range (0f,1f);

		return  MultiplyVector2((vect * 2) , signs);
	}

	private Vector2 MultiplyVector2(Vector2 v1, Vector2 v2)
	{
		v1.x *= v2.x;
		v1.y *= v2.y;
		return v1;
	}

	private float RandomSign()
	{
		float value = Random.Range (-1f, 1f);

		if(value < 0)
		{
			return -1f;
		}

		return 1f;
	}
}
