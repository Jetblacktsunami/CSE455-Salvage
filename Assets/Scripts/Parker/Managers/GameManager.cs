using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static Action<FunctionCallType> gameManagerCalls;

	public enum FunctionCallType{ load, save, exit };
	private FunctionCallType functionCall = FunctionCallType.load;

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "InGame")
		{

		}
	}

	void Save()
	{

	}


}
