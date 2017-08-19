using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringBreakBehavior : MonoBehaviour {

	public enum State{
		Active,
		Broken,
		Dead
	}

	public GameObject[] stringConnectionObjects;
	public int taughtSpringValue = 50;
	public int looseSpringValue = 0;

	SpringJoint[] joints;
	State state;

	// Use this for initialization
	void Start () {
		joints = new SpringJoint[stringConnectionObjects.Length];
		for(var i = 0; i < stringConnectionObjects.Length; i++){
			joints[i] = stringConnectionObjects[i].GetComponent<SpringJoint>();
		}
		ChangeState(State.Active);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeState(State newState){
		Debug.Log("String state change");
		state = newState;
		switch(state){
		case State.Active:
			ChangeStringTenstion(taughtSpringValue);
			break;
		case State.Broken:
			ChangeStringTenstion(looseSpringValue);
			break;
		}
	}

	void ChangeStringTenstion(int value){
		foreach(SpringJoint joint in joints){
			joint.spring = value;
		}
	}
}
