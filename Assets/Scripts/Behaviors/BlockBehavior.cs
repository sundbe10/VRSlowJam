using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {

    public enum State
    {
        Blocking,
        NotBlocking
    }
    public int shieldStrength = 40; 
    public int breakLimit = 5;  //min shieldStrength required to block again after shieldStrength hits 0   
    public int regenTime = 100; //number of frames befor regaming 1 shieldStrength
    public bool shieldBroken = false;

    public HealthManager shieldHealth;
    public State state = State.NotBlocking;
	public GameObject shieldObject;

    private Rigidbody rb;
    private TakeDamageBehavior takeDamageBehavior;
    private HealthManager knightHealth;
    private Animator animator;

    private int count = 0;

    // Use this for initialization
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        takeDamageBehavior = gameObject.GetComponent<TakeDamageBehavior>();
        knightHealth = gameObject.GetComponent<HealthManager>();
        animator = GetComponent<Animator>();

        shieldHealth = gameObject.AddComponent<HealthManager>();
        shieldHealth.maxHealth = shieldStrength;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
        case State.Blocking:
            if(shieldHealth.currentHealth == 0)
            {
				BreakShield();
            }
            break;
        case State.NotBlocking:
            count++;
            if (count == regenTime)
            {
                shieldHealth.Heal(1);
                count = 0;
            }
            if (shieldHealth.currentHealth >= breakLimit && shieldBroken)
            { 
				HealShield();
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
                Block();
                break;
            case State.NotBlocking:
                StopBlocking();
                break;
        }
    }

    void Block()
    {
		animator.SetBool("Block", true);
        rb.isKinematic = true;
        takeDamageBehavior.healthManager = shieldHealth;
        return;
    }

    void StopBlocking()
    {
		animator.SetBool("Block", false);
        rb.isKinematic = false;                            
        takeDamageBehavior.healthManager = knightHealth;    
        return;
    }

	void BreakShield(){
		shieldBroken = true;
		animator.SetTrigger("BlockBreakTrigger");
		shieldObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
		ChangeState(State.Blocking);
	}

	void HealShield(){
		shieldBroken = false;
		shieldObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
	}
}
