using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBehavior : MonoBehaviour {

	public GameObject healthManagerObject;

	HealthManager healthManager;

	// Use this for initialization
	void Start () {
		healthManager = healthManagerObject.GetComponent<HealthManager>();
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
            var damageScript = collider.GetComponent<ApplyDamageBehavior>();
            if (damageScript != null)
            {
                healthManager.Damage(damageScript.damage);
            }
        }
	}
}
