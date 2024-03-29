﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManager : MonoBehaviour {

	public enum State
	{
		Start,
		Active,
        Block,
        Attack,
		Jump,
		Hit,
		Dead,
		Celebrate
	}

    Animator animator;

	State state;
	MoveBehavior moveBehavior;
    AttackBehavior attackBehavior;
    BlockBehavior blockBehavior;
    JumpBehavior jumpBehavior;
	TakeDamageBehavior takeDamageBehavior;
	HealthManager healthManager;
	KnightInputManager inputManager;
	SoundManager soundManager;

	public delegate void OnKnightEventDelegate (GameObject knight);
	public static event OnKnightEventDelegate onKnightDie;

	void Awake ()
	{
		// Add event subscription with callback
		GameManager.onGameStartEvent += EnablePlayer;
		GameManager.onKnightsWinEvent += Celebrate;
        animator = gameObject.GetComponent<Animator>();
		healthManager = GetComponent<HealthManager>();
		inputManager = GetComponent<KnightInputManager>();
		soundManager = GetComponent<SoundManager>();

		// Get Behaviors
		moveBehavior = GetComponent<MoveBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
        blockBehavior = GetComponent<BlockBehavior>();
        jumpBehavior = GetComponent<JumpBehavior>();
		takeDamageBehavior = GetComponent<TakeDamageBehavior>();

		//Invoke("Die", 5);
	}

	void Start()
	{
		ChangeState(State.Start);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(state != State.Dead){
			switch(state)
			{
			case State.Start:
				break;
			case State.Active:
				DetectAttack();
                DetectBlock();
				DetectJump();
				DetectDamage();
				break;
			case State.Block:
                DetectBlock();
				break;
			case State.Attack:
				DetectDamage();
				break;
			case State.Jump:
				DetectDamage();
				if(IsGrounded()) ChangeState(State.Active);
				break;
			case State.Hit:
				DetectHitInAir();
				break;
			}

			if(healthManager.currentHealth <= 0){
				ChangeState(State.Dead);
			}
		}
    }

	public void PlayFootstep(){
		soundManager.PlaySound("footstep");
	}

	void EnablePlayer()
	{
		Debug.Log("Player enabled!");
		ChangeState(State.Active);
	}

    void DetectAttack()
    {
		if(Input.GetButtonDown(inputManager.attack)){
			ChangeState(State.Attack);
			attackBehavior.ChangeState(AttackBehavior.State.Attacking);
		}
    }

	void DetectDamage(){
		if(takeDamageBehavior.tookDamage){
			ChangeState(State.Hit);
		}
	}

	void DetectJump(){
		if(Input.GetButtonDown(inputManager.jump)){
			ChangeState(State.Jump);
			soundManager.PlaySound("jump");
		}
	}

	void DetectHitInAir(){
		// Double check for falling hit to turn bacl to active state
		if(jumpBehavior.state != JumpBehavior.State.Grounded && IsGrounded()){
			ChangeState(State.Active);
		}
	}

    void DetectBlock()
    {
        //simplifyed for testing: replace me!
		if (Input.GetButton(inputManager.block) && !blockBehavior.shieldBroken)
        {
            ChangeState(State.Block);
        }
        else if (state == State.Block)
        {
            ChangeState(State.Active);
        }
    }

    bool IsGrounded(){
		float distanceToGround;
		float threshold = 0.45f;
		RaycastHit hit;
		Vector3 offset = new Vector3(0, 0.4f, 0);
		if(Physics.Raycast((transform.position + offset), -Vector3.up, out hit, 100f)){
			distanceToGround = hit.distance;
			return distanceToGround < threshold;
		}
		return false;
	}

	void Hit(){
		animator.SetTrigger("LightHitTrigger");
	}

	void Die(){
		animator.SetTrigger("DeathTrigger"); 
		soundManager.PlaySound("die");
		onKnightDie(gameObject);
	}

	void Celebrate(){
		animator.SetTrigger("SpecialAttack1Trigger");
		ChangeState(State.Celebrate);
	}

	void ChangeState(State newState)
	{
        if (state == State.Dead) return;
        state = newState;
        Debug.Log(state);
		switch(state)
		{
	    case State.Start:
			soundManager.PlaySound("start");
		    break;
	    case State.Active:
		    moveBehavior.ChangeState(MoveBehavior.State.Grounded);
            attackBehavior.ChangeState(AttackBehavior.State.NotAttacking);
            blockBehavior.ChangeState(BlockBehavior.State.NotBlocking);
			jumpBehavior.ChangeState(JumpBehavior.State.Grounded);
            break;
        case State.Block:
            blockBehavior.ChangeState(BlockBehavior.State.Blocking);
            moveBehavior.ChangeState(MoveBehavior.State.Idle);
            break;
        case State.Attack:
			moveBehavior.ChangeState(MoveBehavior.State.TurnOnly);
            attackBehavior.ChangeState(AttackBehavior.State.Attacking);
            break;
		case State.Jump:
			moveBehavior.ChangeState(MoveBehavior.State.Jump);
			jumpBehavior.ChangeState(JumpBehavior.State.Jumping);
			break;
		case State.Hit:
			moveBehavior.ChangeState(MoveBehavior.State.Idle);
			attackBehavior.ChangeState(AttackBehavior.State.NotAttacking);
			Hit();
			break;
		case State.Celebrate:
			moveBehavior.ChangeState(MoveBehavior.State.Idle);
			attackBehavior.ChangeState(AttackBehavior.State.NotAttacking);
			jumpBehavior.ChangeState(JumpBehavior.State.Grounded);
			break;
	    case State.Dead: 
		    moveBehavior.ChangeState(MoveBehavior.State.Idle);
			attackBehavior.ChangeState(AttackBehavior.State.NotAttacking);
			jumpBehavior.ChangeState(JumpBehavior.State.Grounded);
			Die();
		    break;
		}
	}

	void OnDestroy()
	{
		// Remove event subscription
		GameManager.onGameStartEvent -= EnablePlayer;
		GameManager.onKnightsWinEvent -= Celebrate;
	}


	/*----- Animation Event Functions -----*/

	public void AttackEnded(){
		if(IsGrounded()){
			ChangeState(State.Active);
		}
		else{
			ChangeState(State.Jump);
		}
	}

	public void LightHitEnded(){
		if(IsGrounded()){
			ChangeState(State.Active);
		}
	}
}
