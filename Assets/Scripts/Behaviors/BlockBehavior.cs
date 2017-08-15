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

    private Rigidbody rb;

    //private new HealthManager shield;
    State state = State.NotBlocking;
    TakeDamageBehavior takeDamageBehavior;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        takeDamageBehavior = gameObject.GetComponent<TakeDamageBehavior>();
        animator = GetComponent<Animator>();
        //shield.maxHealth = shieldStranght;
    }

    // Update is called once per frame
    void Update()
    {
    //    switch (state)
    //    {
    //        case State.Blocking:
    //            break;
    //        case State.NotBlocking:
    //            break;
    //    }
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
        animator.SetInteger("Block",1);
        rb.isKinematic = true;
        takeDamageBehavior.enabled = false;
        return;
    }

    public void stopBlocking()
    {
        Debug.Log("not blocking");
        rb.isKinematic = false;
        takeDamageBehavior.enabled = true;
        return;
    }
}
