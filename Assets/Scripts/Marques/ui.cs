using UnityEngine;
using System.Collections;

public class ui : MonoBehaviour 
{
	private int maxHealth = 100;
	public int curHealth = 100;
	private float healthBarlenght;

	// Use this for initialization
	void Start () 
	{
		healthBarlenght = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		AdjustcurHealth (0) ;
	}

	void OnGUI () 
	{
		GUI.Box(new Rect(10, 10, healthBarlenght, 20), curHealth / maxHealth);
	}

	public void AdjustcurHealth (adj) 
	{
		curHealth += adj;
		if(curHealth < 0)
			curHealth = 0;
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		if(maxHealth < 1)
			maxHealth = 1;
		healthBarlenght = (Screen.width / 2) * (curHealth / (float)maxHealth);\
		if (curHealth = 0) {
			<what you want to be done>
		}
	}
};
