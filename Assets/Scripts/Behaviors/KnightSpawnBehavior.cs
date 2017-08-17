using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpawnBehavior : MonoBehaviour {

	public enum State{
		Active,
		Added,
		Idle
	}

	public GameObject knightObject;
	public int playerNumber = 1;
	public Color32 playerColor = Color.white;

	State state = State.Active;
	KnightInputManager inputManager;
	GameObject spawnedKnight;


	// Use this for initialization
	void Awake () {
		GameManager.onGameStartEvent += GameStarted;
		inputManager = GetComponent<KnightInputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case State.Active:
			AddKnight();
			break;
		case State.Added:
			RemoveKnight();
			break;
		case State.Idle:
			break;
		}
	}

	void AddKnight(){
		if(Input.GetButtonDown(inputManager.jump)){
			spawnedKnight = Instantiate(knightObject, transform.position, transform.rotation) as GameObject;

			// Set input for player
			var knightInputManager = spawnedKnight.GetComponent<KnightInputManager>();
			knightInputManager.horizontal = inputManager.horizontal;
			knightInputManager.vertical = inputManager.vertical;
			knightInputManager.attack = inputManager.attack;
			knightInputManager.jump = inputManager.jump;
			knightInputManager.block = inputManager.block;

			//Set aura color
			var light = spawnedKnight.GetComponentInChildren<Light>();
			light.color = playerColor;

			ChangeState(State.Added);
		}
	}

	void RemoveKnight(){
		if(Input.GetButtonDown(inputManager.block)){
			Destroy(spawnedKnight);
			ChangeState(State.Active);
		}
	}

	void GameStarted(){
		ChangeState(State.Idle);
	}

	void ChangeState(State newState){
		state = newState;
	}

	void OnDestroy()
	{
		// Remove event subscription
		GameManager.onGameStartEvent -= GameStarted;
	}
}
