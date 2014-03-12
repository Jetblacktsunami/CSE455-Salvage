using UnityEngine;
using System.Collections;

public class DebugCommands : MonoBehaviour 
{
	public WeaponManager.ammoType SwitchTo = WeaponManager.ammoType.standard;
	public bool WeaponRelated = false;
	public enum MenuAction{ back, reload, none, openConsole, fuelToggle }
	public MenuAction command = MenuAction.none;
	public UIPanel console;

	private UIPanel panel;

	private void Start()
	{
		if(command == MenuAction.back)
		{
			panel = gameObject.transform.parent.gameObject.GetComponent<UIPanel>();
		}
	}

	void OnClick()
	{
		if(WeaponRelated)
		{
			WeaponManager.Instance.ChangeAmmoType(SwitchTo);
		}
		else
		{
			if(command == MenuAction.back)
			{
				panel.alpha = 0f;
			}
			else if(command == MenuAction.reload)
			{
				WeaponManager.Instance.ReplenishAllAmmo();
			}
			else if(command == MenuAction.openConsole && console)
			{
				console.alpha = 1.0f;
			}
			else if(command == MenuAction.fuelToggle)
			{
				PlayerInformation.Instance.CanUseFuel = !(PlayerInformation.Instance.CanUseFuel);
			}
		}
	}
}
