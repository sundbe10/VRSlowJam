using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_health : MonoBehaviour {

    private HealthManager health;

	// Use this for initialization
	void Start () {
        health = GetComponent<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            health.damage(20);
        }
        if (Input.GetKeyDown("h"))
        {
            health.heal(10);
        }
    }
}
