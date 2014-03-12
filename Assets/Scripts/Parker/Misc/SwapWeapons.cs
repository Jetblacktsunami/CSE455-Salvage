using UnityEngine;
using System.Collections;

public class SwapWeapons : MonoBehaviour 
{
	public WeaponManager.ammoType SwitchTo = WeaponManager.ammoType.standard;

	void OnClick()
	{
		WeaponManager.Instance.ChangeAmmoType(SwitchTo);
	}
}
