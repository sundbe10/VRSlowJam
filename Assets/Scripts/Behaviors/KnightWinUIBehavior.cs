using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWinUIBehavior : MonoBehaviour {

	Canvas canvas;

	// Use this for initialization
	void Start () {
		GameManager.onKnightsWinEvent += ShowUI;
		canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ShowUI(){
		canvas.enabled = true;
	}
}
