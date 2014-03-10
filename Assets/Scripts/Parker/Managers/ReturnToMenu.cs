using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour 
{
	void OnClick()
	{
		LoadingBar.Instance.Acivate();
		GameManager.Instance.Save ();
		this.enabled = false;
	}

	public static void OnSaveDone()
	{
		Application.LoadLevel ("MainMenu");
	}
}
