using UnityEngine;

public class PlayerPosioning : MonoBehaviour
{
	public int damagePerShot = 100;
	public float timeBetweenPosions = 15f;

	float explosionTimer;

	ParticleSystem posionParticles;
	Light playerLight;
	float effectsDisplayTime = 0.2f;
	
	
	void Awake ()
	{
		posionParticles = GetComponent<ParticleSystem> ();
		playerLight = GetComponent<Light> ();
	}
	
	
	void Update ()
	{
		explosionTimer += Time.deltaTime;
		
		if(Input.GetButton ("Fire1") && explosionTimer >= timeBetweenPosions && Time.timeScale != 0)
		{
			Posion ();
		}

		if(explosionTimer >= timeBetweenPosions * effectsDisplayTime)
		{
			DisableEffects ();
		}
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
	}
}
