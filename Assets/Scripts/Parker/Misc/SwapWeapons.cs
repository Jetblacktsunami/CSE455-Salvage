using UnityEngine;
using System.Collections;

public class SwapWeapons : MonoBehaviour 
{
	public ShootingManager.ammoType SwitchTo = ShootingManager.ammoType.standard;

	void OnClick()
	{
		ShootingManager.Instance.ChangeAmmoType(SwitchTo);
	}
}
