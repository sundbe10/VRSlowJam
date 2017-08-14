using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour {

	public enum State{
		Active,
		Fire,
		Smoke,
		Idle
	}

	public GameObject fireObject;
	public GameObject smokeObject;
	public int fireTime = 2;
	public int fireRefreshTime = 3;

	State state;
	ParticleSystem fireParticleSystem;
	ParticleSystem smokeParticleSystem;

	// Use this for initialization
	void Start () {
		fireParticleSystem = fireObject.GetComponent<ParticleSystem>();
		smokeParticleSystem = smokeObject.GetComponent<ParticleSystem>();
		ChangeState(State.Active);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			if(state == State.Active){
				ChangeState(State.Fire);
			}
		}
	}

	public void ChangeState(State newState){
		state  = newState;
		switch(state){
		case State.Fire:
			fireParticleSystem.enableEmission = true;
			smokeParticleSystem.enableEmission = false;
			StartCoroutine("FireTimeout");
			break;
		case State.Smoke:
			fireParticleSystem.enableEmission = false;
			smokeParticleSystem.enableEmission = true;
			StartCoroutine("FireRefreshTimeout");
			break;
		case State.Active:
		case State.Idle:
			fireParticleSystem.enableEmission = false;
			smokeParticleSystem.enableEmission = false;
			StopAllCoroutines();
			break;
		}
	}

	IEnumerator FireTimeout(){
		yield return new WaitForSeconds(fireTime);
		ChangeState(State.Smoke);
	}

	IEnumerator FireRefreshTimeout(){
		yield return new WaitForSeconds(fireRefreshTime);
		ChangeState(State.Active);
	}

}
