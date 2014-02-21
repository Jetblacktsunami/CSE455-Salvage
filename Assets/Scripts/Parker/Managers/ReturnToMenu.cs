using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour 
{
	void OnClick()
	{
		GameManager.Instance.Save ();
	}

	public static void OnSaveDone()
	{
		Application.LoadLevel ("MainMenu");
	}
}
