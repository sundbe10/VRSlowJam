using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderMoveForHealthAndAblityTest : MonoBehaviour {

    public float maxSpeed = 10;
    public float acceloration = 2;
    public float rotationRate = 2;

    private Rigidbody rb;

    public enum State
    {
        Active,
        Idle
    }

    State state = State.Active;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Active:
                Move();
                break;
            case State.Idle:
                break;
        }
    }

    public void Move()
    {
        if (!atMaxSpeed())
        {
            if (Input.GetKey("d"))
            {
                rb.AddForce(new Vector3(acceloration, 0, 0));
            }
            if (Input.GetKey("a"))
            {
                rb.AddForce(new Vector3(-acceloration, 0, 0));
            }
            if (Input.GetKey("w"))
            {
                rb.AddForce(new Vector3(0, 0, acceloration));
            }
            if (Input.GetKey("s"))
            {
                rb.AddForce(new Vector3(0, 0, -acceloration));
            }
        }
        if (Input.GetKey("e"))
        {
            rb.transform.Rotate(new Vector3(0, rotationRate, 0));
        }
        if (Input.GetKey("q"))
        {
            rb.transform.Rotate(new Vector3(0, -rotationRate, 0));
        }
    }

    public void ChangeState(State newState)
    {
        state = newState;
    }


    public bool atMaxSpeed()
    {
        if (rb.velocity.magnitude >= maxSpeed)
        {
            return true;
        }
        return false;
    }
}