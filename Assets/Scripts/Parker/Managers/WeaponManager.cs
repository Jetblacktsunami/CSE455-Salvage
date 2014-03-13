using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour 
{
	private float maxAmmo = 100f;
	private float currentAmmo = 100f;
	public enum ammoType{none, standard, beam, chaser}
	public ammoType ammo = ammoType.none;
	public GameObject[] allbullets;

	public Dictionary<ammoType, float> totalMaxAmmo = new Dictionary<ammoType, float>();
	public Dictionary<ammoType, float> totalCurrentAmmo = new Dictionary<ammoType, float> ();

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
		foreach(GameObject obj in allbullets)
		{
			if(obj.name.Contains(ammoType.standard.ToString()))
			{
				totalMaxAmmo.Add(ammoType.standard, 100f);
				totalCurrentAmmo.Add(ammoType.standard, 100f);
			}
			else if(obj.name.Contains(ammoType.beam.ToString()))
			{
				totalMaxAmmo.Add(ammoType.beam, 100f);
				totalCurrentAmmo.Add(ammoType.beam, 100f);
			}
			else if(obj.name.Contains(ammoType.chaser.ToString()))
			{
				totalMaxAmmo.Add(ammoType.chaser, 100f);
				totalCurrentAmmo.Add(ammoType.chaser, 100f);
			}
		}

		ChangeStartUpAmmoType (WeaponManager.ammoType.standard);
	}

	public float MaxAmmo
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
	
	public float CurrentAmmo
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
		if(currentAmmo - (float)amount > 0)
		{
			currentAmmo = 0;
			totalCurrentAmmo[ammo] = 0;
		}
		else
		{
			currentAmmo -= (float)amount;
			totalCurrentAmmo[ammo] = currentAmmo;
		}
		UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
	}

	public void ConsumeAmmo(float amount)
	{
		if(currentAmmo - amount < 0)
		{
			currentAmmo = 0;
			totalCurrentAmmo[ammo] = 0;
		}
		else
		{
			currentAmmo -= amount;
			totalCurrentAmmo[ammo] = currentAmmo;
		}
		UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
	}

	public void ReplenishAllAmmo()
	{
		currentAmmo = maxAmmo;
		totalCurrentAmmo [ammoType.standard] = totalMaxAmmo [ammoType.standard];
		totalCurrentAmmo [ammoType.beam] = totalMaxAmmo [ammoType.beam];
		totalCurrentAmmo [ammoType.chaser] = totalMaxAmmo [ammoType.chaser];
		UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
	}

	public void ChangeStartUpAmmoType(ammoType aType)
	{
		ammo = aType;

		foreach(GameObject obj in allbullets)
		{
			if(obj.name == "ammo_" + ammo.ToString())
			{
				ShootingManager.Instance.SetCurrentBullets (obj);
				ShootingManager.Instance.SetCurrentBulletInfo( obj.GetComponent<BulletInfo>());
				maxAmmo = totalMaxAmmo [ammo];
				currentAmmo = totalCurrentAmmo[ammo];
				UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
			}
		}
	}

	public void ChangeAmmoType(ammoType aType)
	{
		totalMaxAmmo [ammo] = maxAmmo;
		totalCurrentAmmo [ammo] = currentAmmo;

		ammo = aType;

		foreach(GameObject obj in allbullets)
		{
			if(obj.name == "ammo_" + ammo.ToString())
			{
				ShootingManager.Instance.SetCurrentBullets (obj );
				ShootingManager.Instance.SetCurrentBulletInfo( obj.GetComponent<BulletInfo>());
				maxAmmo = totalMaxAmmo [ammo];
				currentAmmo = totalCurrentAmmo[ammo];
				UpdateUI.Ammo.UpdateBar (currentAmmo, maxAmmo);
			}
		}
	}
}
