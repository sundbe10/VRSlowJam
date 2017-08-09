using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTestScript : MonoBehaviour {

	ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Jump")){
			particleSystem.enableEmission = true;
		}
		else{
			particleSystem.enableEmission = false;
		}
	}
}
