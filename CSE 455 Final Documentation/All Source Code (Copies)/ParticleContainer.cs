using UnityEngine;
using System.Collections;

public class ParticleContainer : MonoBehaviour 
{
	public ParticleSystem[] particleSystems;

	private static ParticleContainer particles;
	public static ParticleContainer Instance
	{
		get
		{
			return particles;
		}
	}

	// Use this for initialization
	void Awake() 
	{
		if(particles == null || particles == this)
		{
			particles = this;
		}
		else
		{
			Destroy(this);
		}
	}	

	public void SpawnParticleOnce(string name, Vector2 position)
	{
		foreach(ParticleSystem obj in particleSystems)
		{
			if(obj.name.Contains(name))
			{
				Instantiate(obj,position,Quaternion.identity);
			}
		}
	}

	public ParticleSystem GetParticleSystem(string name)
	{
		foreach(ParticleSystem obj in particleSystems)
		{
			if(obj.name.Contains(name))
			{
				return obj;
			}
		}

		return null;
	}
}
