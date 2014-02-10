using UnityEngine;
using System.Collections;

int GameObject target;

public class gui : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.F)){
			Attack();
		}
	}

	public void Attack () {
		<ui.cs> eh = (<ui.cs>)target.GetComponent("<ui.cs>");
		eh.AdjustcurHealth(-1);
	}

}
