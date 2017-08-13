using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour {

	public enum State{
		Grounded,
		TurnOnly,
		Jump,
		Idle
	}

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float rotateSpeed = 10f;
	private Vector3 moveDirection = Vector3.zero;

	State state = State.Idle;
	CharacterController controller;
	Rigidbody rigidBody;
	Animator animator;
	KnightInputManager inputManager;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		rigidBody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		inputManager = GetComponent<KnightInputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case State.Grounded:
			Run();
			break;
		case State.TurnOnly:
			TurnOnly();
			break;
		case State.Jump:
			JumpMovement();
			break;
		case State.Idle:
			StopMovement();
			break;
		}
	}

	void LateUpdate(){
		//Get local velocity of charcter
		float velocityXel = transform.InverseTransformDirection(rigidBody.velocity).x;
		float velocityZel = transform.InverseTransformDirection(rigidBody.velocity).z;
		//Update animator with movement values
		animator.SetFloat("Input X", velocityXel / speed);
		animator.SetFloat("Input Z", velocityZel / speed);
	}

	public void ChangeState(State newState){
		state = newState;
	}

	void Run()
	{
		var lookDirection = GetControllerDirection();
		if(lookDirection.magnitude != 0){
			animator.SetBool("Moving", true);
			animator.SetBool("Running", true);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), rotateSpeed);
			UpdateVelocity(speed);
		}
		else{
			StopMovement();
		}
	}

	void TurnOnly(){
		var lookDirection = GetControllerDirection();
		if(lookDirection.magnitude != 0){
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), rotateSpeed/2);
			StopMovement();
		}
	}

	void JumpMovement(){
		var lookDirection = GetControllerDirection();
		if(lookDirection.magnitude != 0){
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), rotateSpeed);
		}
	}

	void StopMovement(){
		animator.SetBool("Moving", false);
		animator.SetBool("Running", false);
		UpdateVelocity(0);
	}

	void UpdateVelocity(float moveSpeed){
		var yVel = rigidBody.velocity.y;
		rigidBody.velocity = transform.forward * moveSpeed;
		rigidBody.velocity += Vector3.up * yVel;
	}

	Vector3 GetControllerDirection(){
		return new Vector3(Input.GetAxis(inputManager.horizontal), 0, Input.GetAxis(inputManager.vertical));
	}
}
