using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHealthDisplay : MonoBehaviour {

	public HealthManager BodyHealthManager;
	public HealthManager HeadHealthManager;

	Canvas canvas;
	TextMesh text;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
		text = GetComponentInChildren<TextMesh> ();
		GameManager.onGameStartEvent += ShowUI;
	}

	// Update is called once per frame
	void Update () {
		float bodyHealth = BodyHealthManager.currentHealth;
		float headHealth = HeadHealthManager.currentHealth;
		text.text = "Dragon Body Health = "+bodyHealth+"\nDragon Head Health = "+headHealth;
	}

	void ShowUI(){
		canvas.enabled = true;
	}
}
