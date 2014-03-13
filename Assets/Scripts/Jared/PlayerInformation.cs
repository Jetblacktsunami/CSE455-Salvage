//Description: Used for maintaining and saving/loading player stats

using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class PlayerInformation : MonoBehaviour 
{
	private string deviceID;					//ID of device for verifying save files

	//Ship stats
	private string ship = "Ship 1";				//Name of ship currently selected
	private int maxHealth = 100;						//Max health value of player
	private int currentHealth = 100;					//Current health value of player
	private int maxShields = 100;						//Max shields value of player
	private int currentShields = 100;					//Current shields value of player
	private int shieldRechargeDelay = 2;			//Delay before shields recharge
	private int shieldRechargeRate = 10;				//How quickly the shields refill
	private float maxFuel = 100f;						//Maximum amount of fuel of player
	private float currentFuel = 100f;					//Current fuel level of player
	private float fuelConsumptionRate = 1f;			//How quickly the fuel gauge decreases
	private float speed = 10f;						//Current speed of the player
	private float maxSpeed = 10f;						//Max possible speed of player
	private float acceleration = 10f;					//Acceleration of player towards max speed
	private float thrusterDelay = 10f;				//Delay between input and movement
	private float rotationSpeed = 10f;				//Turning speed of the player
	private bool bConsumeFuel = false;
	//Selection of save file location based on environment
	private string savePath;

	private static PlayerInformation instance;
	public static PlayerInformation Instance
	{
		get
		{
			if(instance)
			{
				return instance;
			}
			else 
			{
				return new GameObject().AddComponent<PlayerInformation>();
			}
		}
	}

	void Start()
	{
		Initialize();
	}

	void Awake()
	{
		savePath = WorldGenerator.directory + "/" + WorldGenerator.worldspec.spaceName + "/" + "PlayerInformation.xml";

		if(!instance)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
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

	public bool CanUseFuel
	{
		get
		{
			return bConsumeFuel;
		}
		set
		{
			bConsumeFuel = value;
		}
	}

	public float getMaxFuel()
	{
		return maxFuel;
	}
	
	public float getCurrentFuel()
	{
		if(bConsumeFuel == false)
		{
			return maxFuel;
		}
		return currentFuel;
	}
	
	public float getFuelConsumptionRate()
	{
		if(bConsumeFuel == false)
		{
			return  0f;
		}
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

	//Mutator functions
	public void setShip(string newShip)
	{
		ship = newShip;
	}
	
	public void setMaxHealth(int newMax)
	{
		maxHealth = newMax;
		UpdateUI.Health.UpdateBar (currentHealth, maxHealth);
	}
	
	public void setCurrentHealth(int newCurrent)
	{
		currentHealth = newCurrent;
		UpdateUI.Health.UpdateBar (currentHealth, maxHealth);
	}
	
	public void setMaxShields(int newMax)
	{
		maxShields = newMax;
		UpdateUI.Shield.UpdateBar (currentShields, maxShields);
	}
	
	public void setCurrentShields(int newCurrent)
	{
		currentShields = newCurrent;
		UpdateUI.Shield.UpdateBar (currentShields, maxShields);
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
		UpdateUI.Fuel.UpdateBar (currentFuel, maxFuel);
	}
	
	public void setCurrentFuel(float newCurrent)
	{
		currentFuel = newCurrent;
		UpdateUI.Fuel.UpdateBar (currentFuel, maxFuel);
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

	public void ConsumeFuel()
	{
		float tempFuel = getCurrentFuel() - (getFuelConsumptionRate() * Time.deltaTime);

		if(tempFuel <= 0)
		{
			setCurrentFuel(0);
		}
		else
		{
			setCurrentFuel(tempFuel);
		}
	}

	//Other functions
	//Takes away health from player when hit
	public void applyDamage(int damage)
	{
		if(currentShields > 0)
		{
			int totalDamage = currentShields - damage;
			if(totalDamage <= 0)
			{
				setCurrentShields( 0 );
				setCurrentHealth( currentHealth + totalDamage );
			}
			else 
			{
				setCurrentShields ( totalDamage );
			}
		}
		else
		{
			setCurrentHealth ( currentHealth - damage);
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
		if(File.Exists(savePath))
		{
			LoadData();
		}
		else
		{
			AutoSave();
		}
	}

	public void AutoSave()
	{
		if(!Directory.Exists(WorldGenerator.directory + "/" + WorldGenerator.worldspec.spaceName + "/"))
		{
			Directory.CreateDirectory(WorldGenerator.directory + "/" + WorldGenerator.worldspec.spaceName + "/");
		}
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
		writer.WriteElementString("playerPosition", transform.position.x.ToString() + "," + transform.position.y.ToString());
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
		writer.WriteElementString("currentWeapon", WeaponManager.Instance.ammo.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("currentMaxAmmo", WeaponManager.Instance.MaxAmmo.ToString());
		writer.WriteWhitespace("\n\t");
		writer.WriteElementString("currentAmmo", WeaponManager.Instance.CurrentAmmo.ToString());
		
		Dictionary<WeaponManager.ammoType, float>.KeyCollection keys = WeaponManager.Instance.totalMaxAmmo.Keys;
		for( Dictionary<WeaponManager.ammoType, float>.KeyCollection.Enumerator i = keys.GetEnumerator(); ; )
		{
			if(i.Current == WeaponManager.ammoType.none)
			{
				if(!i.MoveNext())
				{
					break;
				}
			}
			writer.WriteWhitespace("\n\t");
			writer.WriteElementString( i.Current.ToString() + "_MAX", WeaponManager.Instance.totalMaxAmmo[i.Current].ToString());
			if(!i.MoveNext())
			{
				break;
			}
		}
		
		keys = WeaponManager.Instance.totalCurrentAmmo.Keys;
		for( Dictionary<WeaponManager.ammoType, float>.KeyCollection.Enumerator i = keys.GetEnumerator(); ; )
		{
			if(i.Current == WeaponManager.ammoType.none)
			{
				if(!i.MoveNext())
				{
					break;
				}
			}
			writer.WriteWhitespace("\n\t");
			writer.WriteElementString( i.Current.ToString() + "_CURRENT", WeaponManager.Instance.totalCurrentAmmo[i.Current].ToString());
			if(!i.MoveNext())
			{
				break;
			}
		}
		
		writer.WriteEndElement();
		writer.Close();
		
		XMLFileManager.EncryptFile(savePath);
	}

	//Used for saving the player information to a file
	public void SaveData()
	{
		AutoSave();
		GameManager.Instance.AddToSavePercentage();
	}

	//Used for loading saved player information
	public void LoadData()
	{
		XMLFileManager.DecryptFile(savePath);

		XmlReader reader = new XmlTextReader(savePath);
		while(reader.Read())
		{
			if(reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
			{
				switch(reader.Name)
				{
					case "deviceID":
						deviceID = reader.ReadElementString();
						break;
					case "ship":
						ship = reader.ReadElementString();
						break;
					case "playerPosition":
						string[] positions = reader.ReadElementString().Split(',');
						transform.position = new Vector2(float.Parse(positions[0]), float.Parse(positions[1]) );
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
					case "currentWeapon":
						WeaponManager.Instance.ChangeStartUpAmmoType( (WeaponManager.ammoType)Enum.Parse(typeof(WeaponManager.ammoType),reader.ReadElementString()));
						break;
					case "currentMaxAmmo":
						WeaponManager.Instance.MaxAmmo = float.Parse(reader.ReadElementString());
						break;
					case "currentAmmo":
						WeaponManager.Instance.CurrentAmmo = float.Parse(reader.ReadElementString());
						break;
					case "beam_MAX":
						WeaponManager.Instance.totalMaxAmmo[WeaponManager.ammoType.beam] = float.Parse(reader.ReadElementString());
						break;
					case "chaser_MAX":
						WeaponManager.Instance.totalMaxAmmo[WeaponManager.ammoType.chaser] = float.Parse(reader.ReadElementString());
						break;					
					case "standard_MAX":
						WeaponManager.Instance.totalMaxAmmo[WeaponManager.ammoType.standard] = float.Parse(reader.ReadElementString());
						break;	
					case "beam_CURRENT":
						WeaponManager.Instance.totalCurrentAmmo[WeaponManager.ammoType.beam] = float.Parse(reader.ReadElementString());
						break;
					case "chaser_CURRENT":
						WeaponManager.Instance.totalCurrentAmmo[WeaponManager.ammoType.chaser] = float.Parse(reader.ReadElementString());
						break;					
					case "standard_CURRENT":
						WeaponManager.Instance.totalCurrentAmmo[WeaponManager.ammoType.standard] = float.Parse(reader.ReadElementString());
						break;		
					default:
						Debug.Log(reader.Name);
						break;
				}
			}
		}

		reader.Close();

		if(deviceID != SystemInfo.deviceUniqueIdentifier)
		{
			Initialize();
			AutoSave();
		}

		XMLFileManager.EncryptFile(savePath);
	}

	public static void Wipe()
	{
		PlayerInformation.instance = null;
	}
}