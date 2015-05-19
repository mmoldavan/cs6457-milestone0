using UnityEngine;
using UnityEngine.UI;

public class PlayerPosioning : MonoBehaviour
{
	public int damagePerShot = 100;
	public float timeBetweenPosions = 5f;
	public Slider posionSlider;

	float explosionTimer;

	ParticleSystem posionParticles;
	Light playerLight;
	float effectsDisplayTime = 0.2f;

	Ray posionRay;
	RaycastHit[] collisions;
	
	
	void Awake ()
	{
		posionParticles = GetComponent<ParticleSystem> ();
		playerLight = GetComponent<Light> ();
	}
	
	
	void Update ()
	{
		explosionTimer += Time.deltaTime;
		
		if(Input.GetButton ("Fire2") && explosionTimer >= timeBetweenPosions && Time.timeScale != 0)
		{
			Posion ();
		}

		if(explosionTimer >= timeBetweenPosions * effectsDisplayTime)
		{
			DisableEffects ();
		}

		posionSlider.value = Mathf.Min (explosionTimer, timeBetweenPosions) / timeBetweenPosions;
	}
	
	
	public void DisableEffects ()
	{
		playerLight.enabled = false;
	}
	
	
	void Posion ()
	{
		explosionTimer = 0f;
		
		playerLight.enabled = true;
		
		posionParticles.Stop ();
		posionParticles.Play ();

		posionRay.origin = transform.position;
		posionRay.direction = transform.forward;
		
		collisions = Physics.SphereCastAll (posionRay, 5);
		foreach (RaycastHit hit in collisions)
		{
			EnemyHealth enemyHealth = hit.collider.GetComponent <EnemyHealth> ();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (damagePerShot, hit.point);
			}
		}
	}
}
