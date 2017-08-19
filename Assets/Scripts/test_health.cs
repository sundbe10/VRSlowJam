using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_health : MonoBehaviour {

    private HealthManager health;

	// Use this for initialization
	void Awake () {
        health = GetComponent<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
//        if (Input.GetKeyDown("k"))
//        {
//            health.Damage(20);
//        }
//        if (Input.GetKeyDown("h"))
//        {
//            health.Heal(10);
//        }
    }
}
