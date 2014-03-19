using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour 
{
	public float duration = 0f;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, duration);
	}
}
