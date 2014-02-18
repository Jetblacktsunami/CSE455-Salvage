using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulateLoadGame : MonoBehaviour 
{
	public Object loadlabel;
	public GameObject window;
	public List <WorldGenerator.WorldSpecs> worldinfo;
	private LoadData Ldata;

	void Start () 
	{
		worldinfo = WorldGenerator.GetCreatedWorlds();
		for(int i = 0; i < worldinfo.Count; i++)
		{
			GameObject newLoad = (GameObject)Instantiate (loadlabel);
			//Ldata = newLoad.GetComponent<LoadData>();
			Ldata = newLoad.GetComponentInChildren<LoadData>();
			Ldata.LoadLabel.text = "Name: " + worldinfo[i].spaceName + "    Seed: " + worldinfo[i].seed;
			//Ldata.LoadName = worldinfo[i].spaceName;
			//Ldata.LoadSeed = worldinfo[i].seed;
			Ldata.LoadedLevel = worldinfo[i];

			Ldata.LoadLabel.GetComponent<UIStretch>().container = window;
			newLoad.transform.parent = gameObject.transform;
			newLoad.transform.localScale = Vector3.one;
		}
			//GameObject newLoad = (GameObject)Instantiate (loadlabel);
			//newLoad.transform.parent = gameObject.transform;
	}

}
