using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour {

	Canvas canvas;

	// Use this for initialization
	void Awake () {
		GameManager.onGameStartEvent += HideMenu;
		canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HideMenu(){
		canvas.enabled = false;
	}

	void OnDestroy(){
		GameManager.onGameStartEvent -= HideMenu;
	}
}
