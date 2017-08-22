using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour {

	public enum State{
		Idle,
		Active,
		Prone,
		Dead
	}

	public GameObject bodyHealthManagerObject;
	public GameObject headHealthManagerObject;
	public Rigidbody bodyRigidbody;
	public float proneTimeout; // Time between when dragon becomes prone and then active again
	public State state  = State.Idle;
	public float twitchForce = 100;
	public int baseHealthPerPlayer = 30;

	HealthManager bodyHealthManager;
	HealthManager headHealthManager;
	FireBehavior fireBehavior;
	StringBreakBehavior stringBreakBehavior;
	TakeDamageBehavior[] takeDamageBehaviors;
	SoundManager soundManager;
	TakeDamageBehavior[] damageBehaviors;
	Rigidbody rigidBody;
	float previousHealth;

	public delegate void OnDragonEventDelegate ();
	public static event OnDragonEventDelegate onDragonDie;

	// Use this for initialization
	void Start () {
		GameManager.onGameStartEvent += EnableDragon;
		GameManager.onDragonWinEvent += Celebrate;
		GameManager.onKnightsSetEvent += SetHealth;

		bodyHealthManager = bodyHealthManagerObject.GetComponent<HealthManager>();
		headHealthManager = headHealthManagerObject.GetComponent<HealthManager>();
		fireBehavior = GetComponent<FireBehavior>();
		stringBreakBehavior = GetComponent<StringBreakBehavior>();
		takeDamageBehaviors = GetComponentsInChildren<TakeDamageBehavior>();
		soundManager = GetComponent<SoundManager>();
		rigidBody = bodyRigidbody;
		float previousHealth = bodyHealthManager.currentHealth;

		//ChangeState(State.Prone);
		//Invoke("Die", 2);
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case State.Idle:
			break;
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
		case State.Idle:
			break;
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
			fireBehavior.ChangeState(FireBehavior.State.Idle);
			stringBreakBehavior.ChangeState(StringBreakBehavior.State.Dead);
			Die();
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
			rigidBody.AddForce(Vector3.up * twitchForce);
		}
		previousHealth = healthManager.currentHealth;
	}

	void HealBody(){
		// Return dragon body to full health
		bodyHealthManager.currentHealth = bodyHealthManager.maxHealth;
	}

	void Die(){
		soundManager.PlaySound("explosion");
		soundManager.PlaySound("fall");
		rigidBody.AddForce(Vector3.up * twitchForce * 3);
		StopAllCoroutines();
		onDragonDie();
	}

	void Celebrate ()
	{
		ChangeState(State.Active);
		Debug.Log("celebrate");
		StartCoroutine("DelayedCelebrate");
	}

	void EnableDragon(){
		ChangeState(State.Active);
	}

	void SetHealth(int numPlayers){
		// headHealthManager.SetBaseHealth(baseHealthPerPlayer * numPlayers);
		bodyHealthManager.SetBaseHealth(baseHealthPerPlayer * numPlayers);
	}

	IEnumerator ProneToActiveTimeout(){
		yield return new WaitForSeconds(proneTimeout);
		HealBody();
		soundManager.PlaySound("explosion");
		soundManager.PlaySound("rise");
		ChangeState(State.Active);
	}

	IEnumerator DelayedCelebrate(){
		yield return new WaitForSeconds(1);
		soundManager.PlaySound("taunt");
	}

	void OnDestroy(){
		GameManager.onGameStartEvent -= EnableDragon;
		GameManager.onDragonWinEvent -= Celebrate;
		GameManager.onKnightsSetEvent -= SetHealth;
	}
}
