using UnityEngine;
using System.Collections;

public class UpdateUI : MonoBehaviour 
{
	public enum barType{health, shield, ammo, fuel, none}
	public barType AttachedType = barType.none;

	private static UpdateUI healthBar;
	private static UpdateUI shieldBar;
	private static UpdateUI ammoBar;
	private static UpdateUI fuelBar;

	private UISlider slider;

	public static UpdateUI Health
	{
		get
		{
			return healthBar;
		}
	}

	public static UpdateUI Shield
	{
		get
		{
			return shieldBar;
		}
	}

	public static UpdateUI Ammo
	{
		get
		{
			return ammoBar;
		}
	}
	public static UpdateUI Fuel
	{
		get
		{
			return fuelBar;
		}
	}

	private void Awake()
	{
		if(AttachedType == barType.health)
		{
			UpdateUI.healthBar = this;
		}
		else if(AttachedType == barType.shield)
		{
			UpdateUI.shieldBar = this;
		}
		else if(AttachedType == barType.ammo)
		{
			UpdateUI.ammoBar = this;
		}
		else if(AttachedType == barType.fuel)
		{
			UpdateUI.fuelBar = this;
		}

		slider = gameObject.GetComponent<UISlider> ();
	}

	public void UpdateBar(float current, float max)
	{
		slider.value = current / max;
	}

	public void UpdateBar(int current, int max)
	{
		slider.value = (float)current / (float)max;
	}

}
