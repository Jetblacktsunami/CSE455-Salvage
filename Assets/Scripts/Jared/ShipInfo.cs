//Description: Used for storing and loading ship stats to player info

using UnityEngine;
using System.Collections;

public class ShipInfo : MonoBehaviour
{
	public string shipName;

	private static ShipInfo instance;

	public static ShipInfo Instance
	{
		get
		{
			if(instance)
			{
				return instance;
			}
			else 
			{
				return new GameObject().AddComponent<ShipInfo>();
			}
		}
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

	public void getShipInfo()
	{
		shipName = PlayerInformation.Instance.getShip();
		switch(shipName)	//Based on the ship currently selected, change stats
		{
			case "Ship 1":	//Write ship stats to player info
				
				break;
		}
	}
}