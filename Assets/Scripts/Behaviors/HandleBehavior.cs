using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBehavior : MonoBehaviour {

	public enum State{
		Active,
		Dead
	}

	public GameObject invisibleHandle;

	State state = State.Active;
	Rigidbody rigidBody;
	InteractableObj interactionScript;

	// Use this for initialization
	void Start () {
		GameManager.onKnightsWinEvent += DragonLose;
		rigidBody = GetComponent<Rigidbody>();
		interactionScript = GetComponent<InteractableObj>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeInactive(){
		rigidBody.isKinematic = false;
		interactionScript.enabled = false;
		Destroy(invisibleHandle);
		//Rigidbody invisibleHandleBody = invisibleHandle.GetComponent<Rigidbody>();
		//invisibleHandleBody.isKinematic = false;
	}

	void DragonLose(){
		ChangeState(State.Dead);
	}

	void ChangeState(State newState){
		state = newState;
		switch(state){
		case State.Dead:
			MakeInactive();
			break;
		}
	}

	void OnDestroy(){
		GameManager.onKnightsWinEvent -= DragonLose;
	}

}
