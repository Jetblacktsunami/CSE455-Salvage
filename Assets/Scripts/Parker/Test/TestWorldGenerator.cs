using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestWorldGenerator : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		//WorldGenerator.Instance.GenerateSpace(128, 8, new Vector2(0.0f,0.0f),"TEST Space", 200);
		//List<WorldGenerator.WorldSpecs> worlds = WorldGenerator.GetCreatedWorlds();
		//GameManager.savedLevel = worlds [1];
		GameManager.WorldName = "Parker";
		GameManager.seed = 29;
		Application.LoadLevel ("InGame");
		//WorldGenerator.Instance.GenerateSpace (worlds [0]);
	}
}
