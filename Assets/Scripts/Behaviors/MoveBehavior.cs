using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour {

	public enum State{
		Active,
		Idle
	}

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	State state = State.Idle;
	CharacterController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
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
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			controller.Move(transform.forward * speed * Time.deltaTime);
		}
	}

	public void ChangeState(State newState){
		state = newState;
	}
}
