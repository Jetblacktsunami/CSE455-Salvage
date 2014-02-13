using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class PlayerInformation : MonoBehaviour 
{
   private string deviceID;

	private string ship;
	private int maxHealth;
	private int currentHealth;
	private int maxShields;
	private int currentShields;
	private int shieldRechargeDelay;
	private int shieldRechargeRate;
	private float maxFuel;
	private float currentFuel;
	private float fuelConsumptionRate;
	private float acceleration;
	private float speed;
	private float maxSpeed;
	private float thrusterDelay;
	private float rotationSpeed;
	
	private string weapon;
	private int weaponDamage;
	private float weaponFireRate;
	
#if UNITY_EDITOR || UNITY_PC 	
	private string savePath = "Assets/Resources/Player Data/Info.xml";
#endif
	
#if UNITY_IPHONE || UNITY_ANDROID
#if !UNITY_EDITOR
	private string savePath = Application.persistentDataPath + "/Info.xml";
#endif	
#endif

	void Start()
	{
#if UNITY_ANDROID
		if(Application.platform ==  RuntimePlatform.Android)
		{
			UnityEngine.AndroidJNI.AttachCurrentThread();
		}
#endif
	}
	
	public string getShip()
	{
		return ship;
	}
	
	public int getMaxHealth()
	{
		return maxHealth;
	}
	
	public int getCurrentHealth()
	{
		return currentHealth;
	}
	
	public int getMaxShields()
	{
		return maxShields;
	}
	
	public int getCurrentShields()
	{
		return currentShields;
	}
	
	public int getShieldRechargeDelay()
	{
		return shieldRechargeDelay;
	}
	
	public int getShieldRechargeRate()
	{
		return shieldRechargeRate;
	}
	
	public float getMaxFuel()
	{
		return maxFuel;
	}
	
	public float getCurrentFuel()
	{
		return currentFuel;
	}
	
	public float getFuelConsumptionRate()
	{
		return fuelConsumptionRate;
	}
	
	public float getAcceleration()
	{
		return acceleration;
	}
	
	public float getSpeed()
	{
		return speed;
	}
	
	public float getMaxSpeed()
	{
		return maxSpeed;
	}
	
	public float getThrusterDelay()
	{
		return thrusterDelay;
	}
	
	public float getRotationSpeed()
	{
		return rotationSpeed;
	}
	
	public string getWeapon()
	{
		return weapon;
	}
	
	public int getWeaponDamage()
	{
		return weaponDamage;
	}
	
	public float getWeaponFireRate()
	{
		return weaponFireRate;
	}
	
	public void setShip(string newShip)
	{
		ship = newShip;
	}
	
	public void setMaxHealth(int newMax)
	{
		maxHealth = newMax;
	}
	
	public void setCurrentHealth(int newCurrent)
	{
		currentHealth = newCurrent;
	}
	
	public void setMaxShields(int newMax)
	{
		maxShields = newMax;
	}
	
	public void setCurrentShields(int newCurrent)
	{
		currentShields = newCurrent;
	}
	
	public void setShieldRechargeDelay(int newDelay)
	{
		shieldRechargeDelay = newDelay;
	}
	
	public void setShieldRechargeRate(int newRate)
	{
		shieldRechargeRate = newRate;
	}
	
	public void setMaxFuel(float newMax)
	{
		maxFuel = newMax;
	}
	
	public void setCurrentFuel(float newCurrent)
	{
		currentFuel = newCurrent;
	}
	
	public void setFuelConsumptionRate(float newRate)
	{
		fuelConsumptionRate = newRate;
	}
	
	public void setAcceleration(float newAcceleration)
	{
		acceleration = newAcceleration;
	}
	
	public void setSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
	
	public void setMaxSpeed(float newMax)
	{
		maxSpeed = newMax;
	}
	
	public void setThrusterDelay(float newDelay)
	{
		thrusterDelay = newDelay;
	}
	
	public void setRotationSpeed(float newSpeed)
	{
		rotationSpeed = newSpeed;
	}
	
	public void setWeapon(string newWeapon)
	{
		weapon = newWeapon;
	}
	
	public void setWeaponDamage(int newDamage)
	{
		weaponDamage = newDamage;
	}
	
	public void setWeaponFireRate(float newRate)
	{
		weaponFireRate = newRate;
	}
		
	public void applyDamage(int damage)
	{
		if(currentShields > 0)
		{
			currentShields -= damage;
			if(currentShields < 0)
			{
				currentHealth += currentShields;
				currentShields = 0;
			}
		}
		else
		{
			currentHealth -= damage;
		}
	}
	
	public void accelerate()
	{
		if(speed != maxSpeed)
		{
			speed += acceleration;
			if(speed > maxSpeed)
			{
				speed = maxSpeed;
			}
		}
		currentFuel -= fuelConsumptionRate;
	}
}