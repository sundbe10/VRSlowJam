using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWinUIBehavior : MonoBehaviour {

	Canvas canvas;

	// Use this for initialization
	void Start () {
		GameManager.onDragonWinEvent += ShowUI;
		canvas = GetComponent<Canvas>();
	}

	// Update is called once per frame
	void Update () {

	}

	void ShowUI(){
		canvas.enabled = true;
	}
}
