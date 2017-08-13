using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour {

	public enum State{
		Grounded,
		Jumping,
		Falling
	}

	public float gravity = -9.81f;
	public float jumpForce = 100f;
	public State state;

	Animator animator;
	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();
		ChangeState(State.Grounded);
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case State.Grounded:
			break;
		case State.Jumping:
			CheckIfFalling();
			break;
		case State.Falling:
			break;
		}

		if(!IsGrounded()){
			ApplyGravity();
		}
	}

	public void ChangeState(State newState){
		state = newState;
		switch(state){
		case State.Grounded:
			Land();
			break;
		case State.Jumping:
			Jump();
			break;
		case State.Falling:
			Fall();
			break;
		}
	}

	void Jump(){
		rigidbody.AddForce(0, jumpForce, 0);
		animator.SetTrigger("JumpTrigger");
		animator.SetInteger("Jumping",1);
	}

	void Fall(){
		animator.SetInteger("Jumping",2);
	}

	void Land(){
		animator.SetInteger("Jumping",0);
	}

	void CheckIfFalling(){
		if(rigidbody.velocity.y < 0){
			ChangeState(State.Falling);
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

	void ApplyGravity(){
		rigidbody.AddForce(0, gravity, 0, ForceMode.Acceleration);
	}
}
