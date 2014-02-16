using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static Action<FunctionCallType> gameManagerCalls;
	public static WorldGenerator.WorldSpecs savedLevel;

	public static string WorldName;
	public static int seed;

	public GameObject playerObject;
	public enum FunctionCallType{ load, save, exit };

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
				return new GameObject().AddComponent<GameManager>();
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
				savedLevel = default(WorldGenerator.WorldSpecs);
			}
			else
			{
				WorldGenerator.Instance.GenerateSpace(256 , 6 ,Vector2.zero, WorldName ,seed );
				WorldName = "";
				seed = 0;
			}
		}
		else if(Application.loadedLevelName == "MainMenu")
		{

		}
	}

	void OnEnable()
	{

		WorldGenerator.worldDoneLoading += OnWorldLoadDone;
	}

	void OnDisable()
	{
		WorldGenerator.worldDoneLoading -= OnWorldLoadDone;
	}

	void OnWorldLoadDone(WorldGenerator.ActionType action)
	{
		playerObject = GameObject.Instantiate((Resources.Load("Player"))) as GameObject;
		playerObject.SendMessage("Load",SendMessageOptions.DontRequireReceiver);
	}
}
