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
				PlayerInformation.Instance.setSpeed(1f);
				PlayerInformation.Instance.setRotationSpeed(1f);
				PlayerInformation.Instance.setMaxHealth(5);
				PlayerInformation.Instance.setCurrentHealth(5);
				PlayerInformation.Instance.setMaxShields(5);
				PlayerInformation.Instance.setCurrentShields(5);
				PlayerInformation.Instance.setShieldRechargeDelay(5);
				PlayerInformation.Instance.setShieldRechargeRate(1);
				PlayerInformation.Instance.setMaxFuel(500f);
				PlayerInformation.Instance.setCurrentFuel(500f);
				PlayerInformation.Instance.setFuelConsumptionRate(1f);
				break;
		}
	}
}