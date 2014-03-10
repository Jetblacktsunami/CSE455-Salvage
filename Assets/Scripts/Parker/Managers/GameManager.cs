using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static Action<FunctionCallType> gameManagerCalls;
	public static WorldGenerator.WorldSpecs savedLevel;

	public static string WorldName;
	public static int seed;
	public static bool saving;

	public GameObject playerPrefab;
	public float SavePercentage = 0f;
	public enum FunctionCallType{ load, save, exit };
	[HideInInspector]public GameObject playerObject;

	
	private static GameManager instance;


	public static GameManager Instance
	{
		get
		{
			if(instance)
			{
				return instance;
			}
			else 
			{
				instance = new GameObject().AddComponent<GameManager>();
				return instance;
			}
		}
	}
	
	public void Save()
	{
		gameManagerCalls(FunctionCallType.save);
	}
	
	public void Load()
	{
		gameManagerCalls(FunctionCallType.load);
	}

	void Start()
	{
		if(!instance)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}

		DontDestroyOnLoad(this.gameObject);
	}

	void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "InGame")
		{
		 	if(savedLevel.spaceArea > 0)  
			{
				WorldGenerator.Instance.GenerateSpace(savedLevel);
			}
			else
			{
				WorldGenerator.Instance.GenerateSpace(1024 , 40 ,Vector2.zero, WorldName ,seed );
			}

			SavePercentage = 0.0f;
		}
		else if(Application.loadedLevelName == "MainMenu")
		{
			PlayerInformation.Wipe();
			ShootingManager.Wipe();
			MovementManager.Wipe();
			savedLevel = default(WorldGenerator.WorldSpecs);
			saving = false;
			WorldName = "";
			seed = 0;
			Time.timeScale = 1f;
		}
	}

	private void OnEnable()
	{
		WorldGenerator.worldDoneLoading += OnWorldLoadDone;
	}

	private void OnDisable()
	{
		WorldGenerator.worldDoneLoading -= OnWorldLoadDone;
	}


	/// <summary>
	/// TODO: Add player to the counter
	/// </summary>
	public void AddToSavePercentage()
	{
		SavePercentage += (100f / (ObjectPool.Pool.ActiveCells.Count));
		LoadingBar.Instance.UpdateBar();
		if(SavePercentage >= 100f)
		{
			ReturnToMenu.OnSaveDone();
		}
	}

	private void OnWorldLoadDone(WorldGenerator.ActionType action)
	{
		playerObject = GameObject.Instantiate(playerPrefab) as GameObject;
		playerObject.SendMessage("Load",SendMessageOptions.DontRequireReceiver);
		playerObject.transform.localScale = new Vector3(0.5f,0.5f,1.0f);

		Camera.main.gameObject.AddComponent<FollowTarget>().target = playerObject;
		Camera.main.orthographicSize = 15;
	}
}
