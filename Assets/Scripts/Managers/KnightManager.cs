using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManager : MonoBehaviour {

	public enum State
	{
		Idle,
		Active,
        Block,
        Attack,
		Dead,
		Celebrate
	}

    Animator animator;

	State state = State.Idle;
	MoveBehavior moveBehavior;
    AttackBehavior attackBehavior;

	void Awake ()
	{
		// Add event subscription with callback
		GameManager.onGameStartEvent += EnablePlayer;
        animator = gameObject.GetComponent<Animator>();

		// Get Behaviors
		moveBehavior = GetComponent<MoveBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
	}

	void Start()
	{
       
	}
	
	// Update is called once per frame
	void Update ()
	{
        detectAttack();
    }

	void EnablePlayer()
	{
		Debug.Log("Player enabled!");
		ChangeState(State.Active);
	}

    void detectAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            ChangeState(State.Attack);   
        }
        else if (state == State.Attack && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            ChangeState(State.Active);
        }
    }

	void ChangeState(State newState)
	{
		state = newState;
		switch(state)
		{
		    case State.Idle:
			    break;
		    case State.Active:
			    moveBehavior.ChangeState(MoveBehavior.State.Active);
                attackBehavior.ChangeState(AttackBehavior.State.NotAttacking);
                Debug.Log("state: Active");
                break;
            case State.Block:
                moveBehavior.ChangeState(MoveBehavior.State.Idle);
                break;
            case State.Attack:
                moveBehavior.ChangeState(MoveBehavior.State.Idle);
                attackBehavior.ChangeState(AttackBehavior.State.Attacking);
                Debug.Log("state: Attack");
                break;
		    case State.Dead: 
			    moveBehavior.ChangeState(MoveBehavior.State.Idle);
			    break;
		    case State.Celebrate: 
			    break;
		}
	}

	void OnDestroy()
	{
		// Remove event subscription
		GameManager.onGameStartEvent -= EnablePlayer;
	}
}
