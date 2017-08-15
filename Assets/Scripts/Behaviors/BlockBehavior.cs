using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {

    public enum State
    {
        Blocking,
        NotBlocking
    }
    public int shieldStranght = 40; 
    public int breakLimit = 5;  //min shieldStrangth required to block again after shieldStrangth hits 0   
    public int regenTime = 100; //number of frames befor regaming 1 shieldStrangth
    public bool shieldBroken = false;

    public HealthManager shield;
    public State state = State.NotBlocking;

    private Rigidbody rb;
    private TakeDamageBehavior takeDamageBehavior;
    private HealthManager knightHealth;
    private Animator animator;

    private int count = 0;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        takeDamageBehavior = gameObject.GetComponent<TakeDamageBehavior>();
        knightHealth = gameObject.GetComponent<HealthManager>();
        animator = GetComponent<Animator>();

        shield = gameObject.AddComponent<HealthManager>();
        shield.maxHealth = shieldStranght;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Blocking:
                shieldStranght = shield.currentHealth;
                if(shield.currentHealth == 0)
                {
                    shieldBroken = true;
                }
                break;
            case State.NotBlocking:
                count++;

                if (count == regenTime)
                {
                    shield.Heal(1);
                    count = 0;
                }
                if (shield.currentHealth >= breakLimit || shieldBroken == false)
                { 
                    shieldStranght = shield.currentHealth;
                    shieldBroken = false;
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
        //animator.SetInteger("Block",1);
        rb.isKinematic = true;
        takeDamageBehavior.healthManager = shield;
        return;
    }

    public void stopBlocking()
    {
        rb.isKinematic = false;                             // these two lines are cousing throwing an error at start
        takeDamageBehavior.healthManager = knightHealth;    // I really don't know why.
        return;
    }
}
