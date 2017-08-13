using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour {

    public enum State
    {
        Attacking,
        NotAttacking
    }

    private BoxCollider col;
    //private AnimationState animationState;

    State state = State.NotAttacking;

    // Use this for initialization
    void Start () {
        col = GameObject.Find("Sword").GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    public void ChangeState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Attacking:
                attack();
                break;
            case State.NotAttacking:
                stopAttack();
                break;
        }
    }

    public void attack()
    {
        Debug.Log("attacking");
        col.enabled = true;
        return;
    }

    public void stopAttack()
    {
        Debug.Log("not attacking");
        col.enabled = false;
        return;
    }
}
