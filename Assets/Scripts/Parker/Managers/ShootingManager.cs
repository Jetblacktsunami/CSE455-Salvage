using UnityEngine;
using System.Collections;

public class ShootingManager : MonoBehaviour 
{
	public enum ammoType{none, standard, beam, chaser}
	public ammoType ammo = ammoType.none;
	public GameObject[] allbullets;

	private GameObject currentBullets;
	private BulletInfo bulInfo;
	private float fireTimer;
	private float resetTime;

	//this is specific to the beam weapon
	private bool hasSpawned = false;
	private GameObject spawnedObject;
	private BulletInfo spawnedBulInfo;

	private static ShootingManager instance;

	public static ShootingManager Instance
	{
		get
		{
			if(!instance)
			{
				instance = PlayerInformation.Instance.gameObject.AddComponent<ShootingManager>();
			}
			return instance;
		}
	}

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

	void Awake()
	{
		if(!instance || instance == this)
		{
			instance = this;
		}
		else 
		{
			Destroy(this);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(fireTimer > 0)
		{
			fireTimer -= Time.deltaTime;
		}



		if (Joystick.LeftStick.GetMagnitude () >= 0.1f) 
		{
			Debug.Log ("Joystick left");
			float mag = Joystick.LeftStick.GetMagnitude();
			float ang = Joystick.LeftStick.GetAngle();
			Debug.Log (Mathf.Cos(ang * Mathf.Deg2Rad));
			Vector3 newPosition = PlayerInformation.Instance.transform.position;
			newPosition.x += mag * PlayerInformation.Instance.getSpeed() * Mathf.Cos(ang * Mathf.Deg2Rad) * Time.deltaTime;
			newPosition.y += mag * PlayerInformation.Instance.getSpeed() * Mathf.Sin(ang * Mathf.Deg2Rad) * Time.deltaTime;
			PlayerInformation.Instance.transform.position = newPosition;
			PlayerInformation.Instance.transform.rotation = Quaternion.AngleAxis(Joystick.LeftStick.GetAngle()+180f, new Vector3(0f,0f,1.0f));
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

	public static void Wipe()
	{
		ShootingManager.instance = null;
	}
}
