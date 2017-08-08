using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour {

	public enum State{
		Active,
		Idle
	}

	State state = State.Idle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case State.Active:
			Move();
			break;
		case State.Idle:
			break;
		}
	}

	public void Move()
	{
		Debug.Log("Character can Move!");
	}

	public void ChangeState(State newState){
		state = newState;
	}
}
