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
	public GameObject flameObject;

	State state;
	ParticleSystem fireParticleSystem;
	ParticleSystem smokeParticleSystem;
	DragonInputManager inputManager;
	SoundManager soundManager;

	// Use this for initialization
	void Start () {
		fireParticleSystem = fireObject.GetComponent<ParticleSystem>();
		smokeParticleSystem = smokeObject.GetComponent<ParticleSystem>();
		inputManager = GetComponent<DragonInputManager>();
		soundManager = GetComponent<SoundManager>();
		ChangeState(State.Active);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(inputManager.fire)){
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
		Debug.Log("launch");
		InvokeRepeating("LaunchFlame", 0, 0.2f);
		yield return new WaitForSeconds(fireTime);
		CancelInvoke();
		ChangeState(State.Smoke);
	}

	IEnumerator FireRefreshTimeout(){
		yield return new WaitForSeconds(fireRefreshTime);
		ChangeState(State.Active);
	}

	void LaunchFlame(){
		var flame = Instantiate(flameObject, fireObject.transform.position, fireObject.transform.rotation);
		soundManager.PlaySound("fire");
	}

}
