using UnityEngine;
using System.Collections;

public class DestroyBullets : MonoBehaviour 
{
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.layer == 13)
		{
			Destroy(other.gameObject);
		}
	}

}
