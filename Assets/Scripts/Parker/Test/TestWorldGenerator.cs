using UnityEngine;
using System.Collections;
using ParkerSpaceSystem;

public class TestWorldGenerator : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		ParkerSpaceSystem.WorldGenerator.Instance.GenerateSpace(128, 8, new Vector2(0.0f,0.0f),"TEST Space");
	}
	
}
