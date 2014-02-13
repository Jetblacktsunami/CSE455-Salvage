
using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public NavMesh mesh;
	public Transform target;
	private NavMeshPath path;

	void Start() {

	}
	void Update(){

		if (target) 
		{

			NavMesh.CalculatePath (transform.position, target.position, -1, path);
		}
	}



}