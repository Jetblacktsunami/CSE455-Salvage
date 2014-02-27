//Description: Used for storing and loading weapon information to player info

using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour
{
	public string weaponName;
	private PlayerInformation pInfo;

	void Awake()
	{
		pInfo = GameObject.Find("Player").GetComponent<PlayerInformation>();
	}
	
	void getWeaponInfo()
	{
		weaponName = pInfo.getWeapon();
		switch(weaponName)		//Based on the weapon currently selected, change stats
		{
			case "Weapon 1":	//Write weapon stats to player info
				pInfo.setWeaponDamage(1);
				pInfo.setWeaponFireRate(0.33f);
				pInfo.setMaxAmmo(100);
				pInfo.setCurrentAmmo(100);
			Debug.Log("weapon successfully loaded");
				break;
		}
	}
}