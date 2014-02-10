using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ParkerSpaceSystem;

public class TestWorldGenerator : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		//ParkerSpaceSystem.WorldGenerator.Instance.GenerateSpace(128, 8, new Vector2(0.0f,0.0f),"TEST Space");
		List<ParkerSpaceSystem.WorldGenerator.WorldSpecs> worlds = ParkerSpaceSystem.WorldGenerator.GetCreatedWorlds();

		ParkerSpaceSystem.WorldGenerator.Instance.GenerateSpace (worlds [0]);
	}
	
}
