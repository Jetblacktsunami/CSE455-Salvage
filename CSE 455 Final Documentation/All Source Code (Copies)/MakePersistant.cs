﻿using UnityEngine;
using System.Collections;

public class MakePersistant : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject);
	}
}
