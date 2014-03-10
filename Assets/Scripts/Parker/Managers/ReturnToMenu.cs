using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour 
{
	void OnClick()
	{
		Time.timeScale = 1.0f;
		GameManager.saving = true;
		LoadingBar.Instance.Acivate();
		GameManager.Instance.Save ();
		this.enabled = false;
	}

	public static void OnSaveDone()
	{
		Application.LoadLevel ("MainMenu");
	}
}
