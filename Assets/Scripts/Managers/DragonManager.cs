using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour {

	public enum State{
		Active,
		Prone,
		Dead
	}

	public GameObject bodyHealthManagerObject;
	public GameObject headHealthManagerObject;
	public float proneTimeout; // Time between when dragon becomes prone and then active again
	public State state  = State.Active;

	HealthManager bodyHealthManager;
	HealthManager headHealthManager;
	FireBehavior fireBehavior;
	StringBreakBehavior stringBreakBehavior;
	TakeDamageBehavior[] takeDamageBehaviors;
	SoundManager soundManager;
	TakeDamageBehavior[] damageBehaviors;
	float previousHealth;

	// Use this for initialization
	void Start () {
		bodyHealthManager = bodyHealthManagerObject.GetComponent<HealthManager>();
		headHealthManager = headHealthManagerObject.GetComponent<HealthManager>();
		fireBehavior = GetComponent<FireBehavior>();
		stringBreakBehavior = GetComponent<StringBreakBehavior>();
		takeDamageBehaviors = GetComponentsInChildren<TakeDamageBehavior>();
		soundManager = GetComponent<SoundManager>();
		float previousHealth = bodyHealthManager.currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case State.Active:
			CheckBodyHealth();
			DetectDamage(bodyHealthManager);
			break;
		case State.Prone:
			CheckHeadHealth();
			DetectDamage(headHealthManager);
			break;
		case State.Dead:
			break;
		}
	}

	void ChangeState(State newState){
		state = newState;
		switch(state){
		case State.Active:
			fireBehavior.ChangeState(FireBehavior.State.Active);
			stringBreakBehavior.ChangeState(StringBreakBehavior.State.Active);
			break;
		case State.Prone:
			fireBehavior.ChangeState(FireBehavior.State.Idle);
			stringBreakBehavior.ChangeState(StringBreakBehavior.State.Broken);
			StartCoroutine("ProneToActiveTimeout");
			// Hit and fall Animation
			break;
		case State.Dead:
			StopAllCoroutines();
			fireBehavior.ChangeState(FireBehavior.State.Idle);
			stringBreakBehavior.ChangeState(StringBreakBehavior.State.Broken);
			// End Game
			break;
		}
	}

	void CheckBodyHealth(){
		// If body health reaches zero, put the dragon in the prone state
		if(bodyHealthManager.currentHealth <= 0){
			soundManager.PlaySound("fall");
			ChangeState(State.Prone);
		}
		// We can add hooks here for health bars or indicators
	}

	void CheckHeadHealth(){
		// If dragon head health reaches zero, kill the dragon
		if(headHealthManager.currentHealth <= 0){
			ChangeState(State.Dead);
		}
		// We can add hooks here for health bars or indicators
	}

	void DetectDamage(HealthManager healthManager){
		if(previousHealth > healthManager.currentHealth){
			soundManager.PlaySound("hit");
		}
		previousHealth = healthManager.currentHealth;
	}

	void HealBody(){
		// Return dragon body to full health
		bodyHealthManager.currentHealth = bodyHealthManager.maxHealth;
	}

	IEnumerator ProneToActiveTimeout(){
		yield return new WaitForSeconds(proneTimeout);
		HealBody();
		soundManager.PlaySound("explosion");
		soundManager.PlaySound("rise");
		ChangeState(State.Active);
	}
}
