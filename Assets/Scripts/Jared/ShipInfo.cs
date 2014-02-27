//Description: Used for storing and loading ship stats to player info

using UnityEngine;
using System.Collections;

public class ShipInfo : MonoBehaviour
{
	public string shipName;
	private PlayerInformation pInfo;

	void Awake()
	{
		pInfo = GameObject.Find("Player").GetComponent<PlayerInformation>();
	}

	void getShipInfo()
	{
		shipName = pInfo.getShip();
		switch(shipName)	//Based on the ship currently selected, change stats
		{
			case "Ship 1":	//Write ship stats to player info
				pInfo.setSpeed(1f);
				pInfo.setRotationSpeed(1f);
				pInfo.setMaxHealth(5);
				pInfo.setCurrentHealth(5);
				pInfo.setMaxShields(5);
				pInfo.setCurrentShields(5);
				pInfo.setShieldRechargeDelay(5);
				pInfo.setShieldRechargeRate(1);
				pInfo.setMaxFuel(500f);
				pInfo.setCurrentFuel(500f);
				pInfo.setFuelConsumptionRate(1f);
			Debug.Log("ship successfully loaded");
				break;
		}
	}
}