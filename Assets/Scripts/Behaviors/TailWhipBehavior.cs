using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ApplyDamageBehavior))]
[RequireComponent(typeof(TakeDamageBehavior))]
public class TailWhipBehavior : MonoBehaviour {

	public float minTailVelocity = 10f;

	Collider collider;
	Rigidbody rigidBody;
	ApplyDamageBehavior applyDamageBehavior;
	TakeDamageBehavior takeDamageBehavior;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider>();
		rigidBody = GetComponent<Rigidbody>();
		applyDamageBehavior = GetComponent<ApplyDamageBehavior>();
		takeDamageBehavior = GetComponent<TakeDamageBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidBody.velocity.magnitude > minTailVelocity){
			applyDamageBehavior.enabled = true;
		}else{
			applyDamageBehavior.enabled = false;
		}
	}

	void OnDrawGizmos(){
		if(rigidBody.velocity.magnitude > minTailVelocity){
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, 1);
		}
	}
}
