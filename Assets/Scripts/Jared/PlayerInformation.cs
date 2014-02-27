//Description: Used for maintaining and saving/loading player stats

using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerInformation : MonoBehaviour 
{
	private string deviceID;					//ID of device for verifying save files

	//Ship stats
	private string ship = "Ship 1";				//Name of ship currently selected
	private int maxHealth;						//Max health value of player
	private int currentHealth;					//Current health value of player
	private int maxShields;						//Max shields value of player
	private int currentShields;					//Current shields value of player
	private int shieldRechargeDelay;			//Delay before shields recharge
	private int shieldRechargeRate;				//How quickly the shields refill
	private float maxFuel;						//Maximum amount of fuel of player
	private float currentFuel;					//Current fuel level of player
	private float fuelConsumptionRate;			//How quickly the fuel gauge decreases
	private float speed;						//Current speed of the player
	private float maxSpeed;						//Max possible speed of player
	private float acceleration;					//Acceleration of player towards max speed
	private float thrusterDelay;				//Delay between input and movement
	private float rotationSpeed;				//Turning speed of the player

	//Weapon stats
	private string weapon = "Weapon 1";			//Name of currently selected weapon
	private int weaponDamage;					//Damage dealt by current weapon
	private int maxAmmo;						//Maximum ammo the weapon holds
	private int currentAmmo;					//Current amount of ammo held
	private float weaponFireRate;				//How fast the weapon fires

	//Selection of save file location based on environment
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

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
		if(File.Exists(savePath))
		{
			LoadData();
		}
		else
		{
			Initialize();
			SaveData();
		}
	}

	//Accessor functions
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

	public int getMaxAmmo()
	{
		return maxAmmo;
	}

	public int getCurrentAmmo()
	{
		return currentAmmo;
	}
	
	public float getWeaponFireRate()
	{
		return weaponFireRate;
	}

	//Mutator functions
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

	public void setMaxAmmo(int newMax)
	{
		maxAmmo = newMax;
	}
	
	public void setCurrentAmmo(int newCurrent)
	{
		currentAmmo = newCurrent;
	}
	
	public void setWeaponFireRate(float newRate)
	{
		weaponFireRate = newRate;
	}

	//Other functions
	//Takes away health from player when hit
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

	//Accelerates the ship towards max speed
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

	//Initialize values for new save
	public void Initialize()
	{
		ship = "Ship 1";
		weapon = "Weapon 1";

	}


	//Used for saving the player information to a file
	public void SaveData()
	{
		Debug.Log("Saving...");
		if(File.Exists(savePath))
		{
			File.Delete(savePath);
		}

		XmlWriter writer = new XmlTextWriter(savePath, System.Text.Encoding.UTF8);
		writer.WriteStartDocument();
		writer.WriteWhitespace("\n");
		writer.WriteStartElement("Root");
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("deviceID", SystemInfo.deviceUniqueIdentifier);
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("ship", ship);
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("maxHealth", maxHealth.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("currentHealth", currentHealth.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("maxShields", maxShields.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("currentShields", currentShields.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("shieldRechargeDelay", shieldRechargeDelay.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("shieldRechargeRate", shieldRechargeRate.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("maxFuel", maxFuel.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("currentFuel", currentFuel.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("fuelConsumptionRate", fuelConsumptionRate.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("acceleration", acceleration.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("speed", speed.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("maxSpeed", maxSpeed.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("thrusterDelay", thrusterDelay.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("rotationSpeed", rotationSpeed.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("weapon", weapon);
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("weaponDamage", weaponDamage.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("maxAmmo", maxAmmo.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("currentAmmo", currentAmmo.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("weaponFireRate", weaponFireRate.ToString());
		writer.WriteEndElement();
		writer.Close();

		XMLFileManager.EncryptFile(savePath);
	}

	//Used for loading saved player information
	public void LoadData()
	{
		Debug.Log("Loading...");
		if(!File.Exists(savePath))
		{
			return;
		}
		else
		{
			XMLFileManager.DecryptFile(savePath);
		}

		XmlReader reader = new XmlTextReader(savePath);
		while(reader.Read())
		{
			if(reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
			{
				switch(reader.Name)
				{
					case "deviceID":
						Debug.Log(deviceID + "\n we found the id");
						deviceID = reader.ReadElementString();
						break;
					case "ship":
						ship = reader.ReadElementString();
						break;
					case "maxHealth":
						maxHealth = int.Parse(reader.ReadElementString());
						break;
					case "currentHealth":
						currentHealth = int.Parse(reader.ReadElementString());
						break;
					case "maxShields":
						maxShields = int.Parse(reader.ReadElementString());
						break;
					case "currentShields":
						currentShields = int.Parse(reader.ReadElementString());
						break;
					case "shieldRechargeDelay":
						shieldRechargeDelay = int.Parse(reader.ReadElementString());
						break;
					case "shieldRechargeRate":
						shieldRechargeRate = int.Parse(reader.ReadElementString());
						break;
					case "maxFuel":
						maxFuel = float.Parse(reader.ReadElementString());
						break;
					case "currentFuel":
						currentFuel = float.Parse(reader.ReadElementString());
						break;
					case "fuelConsumptionRate":
						fuelConsumptionRate = float.Parse(reader.ReadElementString());
						break;
					case "acceleration":
						acceleration = float.Parse(reader.ReadElementString());
						break;
					case "speed":
						speed = float.Parse(reader.ReadElementString());
						break;
					case "maxSpeed":
						maxSpeed = float.Parse(reader.ReadElementString());
						break;
					case "thrusterDelay":
						thrusterDelay = float.Parse(reader.ReadElementString());
						break;
					case "rotationSpeed":
						rotationSpeed = float.Parse(reader.ReadElementString());
						break;
					case "weapon":
						weapon = reader.ReadElementString();
						break;
					case "weaponDamage":
						weaponDamage = int.Parse(reader.ReadElementString());
						break;
					case "maxAmmo":
						maxAmmo = int.Parse(reader.ReadElementString());
						break;
					case "currentAmmo":
						currentAmmo = int.Parse(reader.ReadElementString());
						break;
					case "weaponFireRate":
						weaponFireRate = float.Parse(reader.ReadElementString());
						break;
					default:
						break;
				}
			}
		}

		reader.Close();

		if(deviceID != SystemInfo.deviceUniqueIdentifier)
		{
			SaveData();
			Debug.Log("Invalid ID for Loading");
		}

		XMLFileManager.EncryptFile(savePath);
	}
}