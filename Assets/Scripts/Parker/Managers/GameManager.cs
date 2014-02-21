﻿using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static Action<FunctionCallType> gameManagerCalls;
	public static WorldGenerator.WorldSpecs savedLevel;

	public static string WorldName;
	public static int seed;

	public GameObject playerObject;
	public float SavePercentage = 0f;
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
			}
			else
			{
				WorldGenerator.Instance.GenerateSpace(256 , 10 ,Vector2.zero, WorldName ,seed );
			}
			SavePercentage = 0.0f;
		}
		else if(Application.loadedLevelName == "MainMenu")
		{
			savedLevel = default(WorldGenerator.WorldSpecs);
			WorldName = "";
			seed = 0;
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
		Debug.Log (SavePercentage);
		Debug.Log (WorldGenerator.worldspec.totalNumberOfCells);
		SavePercentage += (100f / (WorldGenerator.worldspec.totalNumberOfCells));
		if(SavePercentage >= 100f)
		{
			ReturnToMenu.OnSaveDone();
		}
	}

	private void OnWorldLoadDone(WorldGenerator.ActionType action)
	{
		playerObject = GameObject.Instantiate((Resources.Load("Player"))) as GameObject;
		playerObject.SendMessage("Load",SendMessageOptions.DontRequireReceiver);
		playerObject.transform.localScale = new Vector3(0.5f,0.5f,1.0f);
		Camera.main.gameObject.AddComponent<FollowTarget>().target = playerObject;
		Camera.main.orthographicSize = 15;

	}
}
