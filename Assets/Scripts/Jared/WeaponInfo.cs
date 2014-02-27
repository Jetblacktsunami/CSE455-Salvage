//Description: Used for storing and loading weapon information to player info

using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour
{
	public string weaponName;

	private static WeaponInfo instance;

	public static WeaponInfo Instance
	{
		get
		{
			if(instance)
			{
				return instance;
			}
			else 
			{
				return new GameObject().AddComponent<WeaponInfo>();
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

	public void getWeaponInfo()
	{
		weaponName = PlayerInformation.Instance.getWeapon();
		switch(weaponName)		//Based on the weapon currently selected, change stats
		{
			case "Weapon 1":	//Write weapon stats to player info
				PlayerInformation.Instance.setWeaponDamage(1);
				PlayerInformation.Instance.setWeaponFireRate(0.33f);
				PlayerInformation.Instance.setMaxAmmo(100);
				PlayerInformation.Instance.setCurrentAmmo(100);
				Debug.Log("weapon successfully loaded");
				break;
		}
	}
}