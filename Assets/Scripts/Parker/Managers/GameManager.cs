using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static Action<FunctionCallType> gameManagerCalls;
	public static ParkerSpaceSystem.WorldGenerator.WorldSpecs savedLevel;

	public static string WorldName;
	public static int seed;

	public GameObject playerObject;
	public enum FunctionCallType{ load, save, exit };

	private FunctionCallType functionCall = FunctionCallType.load;
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
				ParkerSpaceSystem.WorldGenerator.Instance.GenerateSpace(savedLevel);
				savedLevel = default(ParkerSpaceSystem.WorldGenerator.WorldSpecs);
			}
			else
			{
				ParkerSpaceSystem.WorldGenerator.Instance.GenerateSpace(256 , 6 ,Vector2.zero, WorldName ,seed );
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
		ParkerSpaceSystem.WorldGenerator.worldDoneLoading += OnWorldLoadDone;
	}

	void OnDisable()
	{
		ParkerSpaceSystem.WorldGenerator.worldDoneLoading -= OnWorldLoadDone;
	}

	void OnWorldLoadDone(ParkerSpaceSystem.WorldGenerator.ActionType action)
	{
		playerObject = GameObject.Instantiate((Resources.Load("Player"))) as GameObject;
		playerObject.SendMessage("Load",SendMessageOptions.DontRequireReceiver);
	}
}
