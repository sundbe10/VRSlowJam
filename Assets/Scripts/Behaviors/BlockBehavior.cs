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
    private TakeDamageBehavior takeDamageBehavior;
    //private new HealthManager shield;
    State state = State.NotBlocking;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        takeDamageBehavior = gameObject.GetComponent<TakeDamageBehavior>();
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
