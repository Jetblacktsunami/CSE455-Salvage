//Description: Used for storing and loading ship stats to player info

using UnityEngine;
using System.Collections;

public class ShipInfo : MonoBehaviour
{
	public string shipName;

	void getShipInfo()
	{
	   switch(shipName)   //Based on the ship currently selected, change stats
	   {
			case "Ship 1":
				//Write ship stats to player info
				break;
		}
	}
}