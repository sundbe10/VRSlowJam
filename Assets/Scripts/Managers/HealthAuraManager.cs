using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAuraManager : MonoBehaviour {

    public float maxSize = 40;
    public float minSize = 0;
    private HealthManager health;
    private Light Aura;

    // Use this for initialization
    void Start()
    {
        health = transform.parent.GetComponent<HealthManager>();
        Aura = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthLeft = ((float)health.currentHealth / (float)health.maxHealth) * 
            (maxSize-minSize) + minSize;
        Aura.spotAngle = healthLeft;
    }

}
