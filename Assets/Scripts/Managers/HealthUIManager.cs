using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour {

    public float max;
    private HealthManager health;

    // Use this for initialization
    void Start()
    {
        health = transform.parent.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update () {
        //makes sprite always face camera
        transform.LookAt(Camera.main.transform.position, -Vector3.up);

        //connects health bar width to current health left
        float healthLeftWidth = ((float)health.currentHealth / (float)health.maxHealth) * max;
        transform.localScale = new Vector3(healthLeftWidth, 0.2f, 1);
	}
    
}
