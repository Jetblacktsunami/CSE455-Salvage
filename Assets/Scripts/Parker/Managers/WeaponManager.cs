using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour 
{
	private int maxAmmo = 100;
	private int currentAmmo = 100;
	public enum ammoType{none, standard, beam, chaser}
	public ammoType ammo = ammoType.none;
	public GameObject[] allbullets;

	public Dictionary<ammoType, int> totalMaxAmmo = new Dictionary<ammoType, int>();
	public Dictionary<ammoType, int> totalCurrentAmmo = new Dictionary<ammoType, int> ();

	private static WeaponManager man;
	public static WeaponManager Instance
	{
		get
		{
			return man;
		}
	}

	private void Awake()
	{
		man = this;
		Initalize ();
	}

	public void Initalize()
	{
		ChangeAmmoType (WeaponManager.ammoType.standard);

		foreach(GameObject obj in allbullets)
		{
			if(obj.name.Contains(ammoType.standard.ToString()))
			{
				totalMaxAmmo.Add(ammoType.standard, 100);
				totalCurrentAmmo.Add(ammoType.standard, 100);
			}
			else if(obj.name.Contains(ammoType.beam.ToString()))
			{
				totalMaxAmmo.Add(ammoType.beam, 100);
				totalCurrentAmmo.Add(ammoType.beam, 100);
			}
			else if(obj.name.Contains(ammoType.chaser.ToString()))
			{
				totalMaxAmmo.Add(ammoType.chaser, 100);
				totalCurrentAmmo.Add(ammoType.chaser, 100);
			}
		}
	}

	public int MaxAmmo
	{
		get
		{
			return maxAmmo;
		}
		set
		{
			maxAmmo = value;
			UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
		}
	}
	
	public int CurrentAmmo
	{
		get
		{
			return currentAmmo;
		}
		set
		{
			currentAmmo = value;
			UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
		}
	}
	
	public void ConsumeAmmo(int amount)
	{
		if(currentAmmo - amount > 0)
		{
			currentAmmo = 0;
		}
		else
		{
			currentAmmo -= amount;
		}
		UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
	}

	public void ChangeAmmoType(ammoType aType)
	{
		ammo = aType;
		foreach(GameObject obj in allbullets)
		{
			if(obj.name == "ammo_" + ammo.ToString())
			{
				ShootingManager.Instance.SetCurrentBullets (obj );
				ShootingManager.Instance.SetCurrentBulletInfo( obj.GetComponent<BulletInfo>());
			}
		}
	}
}
