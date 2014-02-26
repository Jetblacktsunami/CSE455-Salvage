using UnityEngine;
using System.Collections;

public class ShootingManager : MonoBehaviour 
{
	public enum ammoType{none, standard, beam}
	public ammoType ammo = ammoType.none;
	public GameObject[] allbullets;
	public PlayerInformation Instance;

	private GameObject currentBullets;
	private BulletInfo bulInfo;
	private float fireTimer;
	private float resetTime;

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
				bulInfo.travelAngle = Joystick.RightStick.GetAngle();
				GameObject.Instantiate(currentBullets, transform.position, Quaternion.identity);
			}
			else if(!currentBullets)
			{
				ChangeAmmoType(ammoType.standard);
				Debug.Log("Your ammo is empty dawg");
			}
			fireTimer = resetTime;
		}



	}
}
