using UnityEngine;
using System.Collections;

public class EnemyInfo : MonoBehaviour 
{
	private float _health;
	public float health = 100f;
	public float speed = 1f;
	public float shootingRange = 5f;
	void Start()
	{
		_health = health;
	}

	public void ApplyDamage (float amount) 
	{
		_health -= amount;
		if(_health <= 0)		
		{
			DestroySelf();
		}
	}

	private void Update()
	{
		if(Mathf.Abs(Vector2.Distance(PlayerInformation.Instance.gameObject.transform.position, transform.position)) > WorldGenerator.worldspec.cellLength * 2.5) 
		{
			EnemyManager.Instance.DecreaseEnemyCount ();
			Destroy(gameObject);
		}
	}
	void DestroySelf()
	{
		EnemyManager.Instance.DecreaseEnemyCount ();
		ParticleContainer.Instance.SpawnParticleOnce ("ShipExplosion", gameObject.transform.position);
		Destroy (gameObject);
	}
}
