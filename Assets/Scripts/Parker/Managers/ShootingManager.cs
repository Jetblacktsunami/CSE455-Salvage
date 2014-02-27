﻿using UnityEngine;
using System.Collections;

public class ShootingManager : MonoBehaviour 
{
	public enum ammoType{none, standard, beam, chaser}
	public ammoType ammo = ammoType.none;
	public GameObject[] allbullets;
	public PlayerInformation Instance;

	private GameObject currentBullets;
	private BulletInfo bulInfo;
	private float fireTimer;
	private float resetTime;

	//this is specific to the beam weapon
	private bool hasSpawned = false;
	private GameObject spawnedObject;
	private BulletInfo spawnedBulInfo;

	public void ChangeAmmoType(ammoType aType)
	{
		ammo = aType;
		foreach(GameObject obj in allbullets)
		{
			if(obj.name == "ammo_" + ammo.ToString())
			{
				currentBullets = obj;
				bulInfo = currentBullets.GetComponent<BulletInfo>();
				resetTime = fireTimer = bulInfo.fireRate;
			}
		}
	}

	void Start()
	{
		ChangeAmmoType(ammoType.standard);
	}

	// Update is called once per frame
	void Update () 
	{
		if(fireTimer > 0)
		{
			fireTimer -= Time.deltaTime;
		}

		if(Joystick.RightStick.GetMagnitude() >= 0.5f && fireTimer <= 0)
		{
			if(currentBullets)
			{
				if(ammo != ammoType.beam)
				{
					bulInfo.travelAngle = Joystick.RightStick.GetAngle();
					GameObject.Instantiate(currentBullets, transform.position, Quaternion.identity);
				}
				else if(ammo == ammoType.beam)
				{
					if(!hasSpawned)
					{
						bulInfo.travelAngle = Joystick.RightStick.GetAngle();
						spawnedObject = GameObject.Instantiate(currentBullets, transform.position, Quaternion.identity) as GameObject;
						spawnedBulInfo = spawnedObject.GetComponent<BulletInfo>();
						hasSpawned = true;
					}
					else
					{
						spawnedObject.transform.rotation = Quaternion.AngleAxis(Joystick.RightStick.GetAngle(), new Vector3(0f,0f,1.0f));
						spawnedBulInfo.travelAngle = Joystick.RightStick.GetAngle();
					}
				}
			}
			else if(!currentBullets)
			{
				ChangeAmmoType(ammoType.standard);
				Debug.Log("Your ammo is empty dawg");
			}
			fireTimer = resetTime;
		}
		else if(Joystick.RightStick.GetMagnitude() < 0.5f && hasSpawned)
		{
			Destroy(spawnedObject);
			hasSpawned = false;
		}

	}
}