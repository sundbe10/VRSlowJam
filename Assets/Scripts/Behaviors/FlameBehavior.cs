using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBehavior : MonoBehaviour {

	public float flameLifetime = 0.5f;
	public float flameSpeed = 10;

	// Use this for initialization
	void Start () {
		StartCoroutine("DestroyFlame");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += transform.forward * flameSpeed;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 1f);
	}

	IEnumerator DestroyFlame(){
		yield return new WaitForSeconds(flameLifetime);
		GameObject.Destroy(gameObject);
	}
}
