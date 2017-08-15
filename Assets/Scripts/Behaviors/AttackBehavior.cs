using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour {

    public enum State
    {
        Attacking,
        NotAttacking
    }

	private SphereCollider collider;
    //private AnimationState animationState;

    State state;
	ApplyDamageBehavior applyDamageBehavior;
	Animator animator;
	SoundManager soundManager;

    // Use this for initialization
    void Start () {
		collider = GetComponent<SphereCollider>();
		applyDamageBehavior = GetComponent<ApplyDamageBehavior>();
		animator = GetComponent<Animator>();
		soundManager = GetComponent<SoundManager>();
		ChangeState(State.NotAttacking);
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
				Attack();
                break;
            case State.NotAttacking:
                StopAttack();
                break;
        }
    }

	// Animation Event
	public void UpdateAttackStatus(int status){
		if(status == 1){
			collider.enabled = true;
			applyDamageBehavior.enabled = true;
			soundManager.PlaySound("sword_air");
		}else{
			collider.enabled = false;
			applyDamageBehavior.enabled = false;
		}
	}

	void Attack()
    {
		animator.SetInteger("Attack",1);
	}

    void StopAttack()
    {
		animator.SetInteger("Attack",0);
		collider.enabled = false;
		applyDamageBehavior.enabled = false;
		return;
    }
		
}
