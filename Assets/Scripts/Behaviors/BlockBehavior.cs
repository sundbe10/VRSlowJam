using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {

    public enum State
    {
        Blocking,
        NotBlocking
    }
    public int shieldStranght = 100;
    //min shieldStrangth required to block again after shieldStrangth hits 0
    public int breakLimit = 5;
    //number of frames befor regaming 1 shieldStrangth
    public int regenTime = 100;

    private Rigidbody rb;
    public HealthManager shield;
    //private new TakeDamageBehavior damageShield;

    public State state = State.NotBlocking;
    TakeDamageBehavior takeDamageBehavior;
    HealthManager healthManager;
    Animator animator;

    int count = 0;

    // Use this for initialization
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody>();
        takeDamageBehavior = gameObject.GetComponent<TakeDamageBehavior>();
        healthManager = gameObject.GetComponent<HealthManager>();
        animator = GetComponent<Animator>();

        shield = new HealthManager();
        shield.maxHealth = shieldStranght;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Blocking:
                shieldStranght = shield.currentHealth;
                break;
            case State.NotBlocking:
                count++;
                if (count == regenTime)
                {
                    shield.Heal(1);
                    count = 0;
                }
                if (shield.currentHealth >= breakLimit)
                { 
                    shieldStranght = shield.currentHealth;
                }
                break;
        }
    }

    public void ChangeState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Blocking:
                block();
                break;
            case State.NotBlocking:
                stopBlocking();
                break;
        }
    }

    public void block()
    {
        Debug.Log("blocking");
        //animator.SetInteger("Block",1);
        rb.isKinematic = true;
        takeDamageBehavior.healthManager = shield;
        //damageShield.enabled = true;
        return;
    }

    public void stopBlocking()
    {
        Debug.Log("not blocking");
        rb.isKinematic = false;
        takeDamageBehavior.healthManager = healthManager;
        //damageShield.enabled = false;
        return;
    }
}
