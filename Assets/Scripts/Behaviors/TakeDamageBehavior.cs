using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBehavior : MonoBehaviour {

	public GameObject healthManagerObject;

	HealthManager healthManager;
	ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		healthManager = healthManagerObject.GetComponent<HealthManager>();
		particleSystem = GetComponent<ParticleSystem>();
		if(healthManager == null){
			Debug.LogError("Health manager was not found on gameObject!");
		}
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
			Debug.Log(gameObject);
			Debug.Log(collider);
            var damageScript = collider.GetComponent<ApplyDamageBehavior>();
			if (damageScript != null && damageScript.enabled == true)
            {
                healthManager.Damage(damageScript.damage);
				if(particleSystem != null){
					particleSystem.Emit(20);
				}
            }
        }
	}
}
