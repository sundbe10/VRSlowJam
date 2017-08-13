using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTestScript : MonoBehaviour {

	public float rotateSpeed = 80;
	public float moveSpeed = 10;

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
		var forward = Input.GetAxis("Vertical2");
		var rotate = Input.GetAxis("Rotate");
		if(vertical != 0){
			transform.Rotate(rotateSpeed*Time.deltaTime*vertical,0,0);
		}
		if(horizontal != 0){
			transform.Rotate(0, 0, -rotateSpeed*Time.deltaTime*horizontal);
		}
		if(forward != 0){
			transform.position += transform.forward * Time.deltaTime * forward * moveSpeed;
		}
		if(rotate != 0){
			transform.Rotate(0, rotateSpeed*Time.deltaTime*rotate,0);
		}*/
		transform.Rotate(Vector3.up * 2);

			
	}
}
