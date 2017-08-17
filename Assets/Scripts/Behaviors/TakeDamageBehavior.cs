using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TakeDamageBehavior : MonoBehaviour {

	public GameObject healthManagerObject;
	public bool tookDamage = false;
	public string hitSoundIdentifier = "hit";

	public HealthManager healthManager;
	ParticleSystem particleSystem;
	SoundManager soundManager;

	// Use this for initialization
	void Start () {
		healthManager = healthManagerObject.GetComponent<HealthManager>();
		particleSystem = GetComponent<ParticleSystem>();
		soundManager = GetComponent<SoundManager>();
		if(healthManager == null){
			Debug.LogError("Health manager was not found on gameObject!");
		}
	}

	void Update(){
		tookDamage = false;
	}

	void OnCollisionEnter(Collision collision){
		TestDamage(collision.collider);
	}

	void OnTriggerEnter(Collider collider){
		TestDamage(collider);
	}

	void TestDamage(Collider collider){
        if (collider.tag != gameObject.tag)
        {
            var damageScript = collider.GetComponent<ApplyDamageBehavior>();
			if (damageScript != null && damageScript.enabled == true)
            {
                healthManager.Damage(damageScript.damage);
				if(particleSystem != null){
					particleSystem.Emit(20);
				}
				soundManager.PlaySound(hitSoundIdentifier);
				tookDamage = true;
            }
        }
	}
}
