using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour 
{
	public GameObject[] enemieTypes;
	public int maxEnemies = 6;

	private bool bCanSpawnEnemies = false;

	private static EnemyManager eManager;
	public static EnemyManager Instance
	{
		get
		{
			return eManager;
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
			Vector2 playerPos = (Vector2)PlayerInformation.Instance.gameObject.transform.position;
		}
	}

	private void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "MainMenu")
		{
			bCanSpawnEnemies = false;
		}
		else if(Application.loadedLevelName == "InGame")
		{
			bCanSpawnEnemies = true;
		}
	}
}
