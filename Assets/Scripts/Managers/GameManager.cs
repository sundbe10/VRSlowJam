using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum State
	{
		Start,
		Active,
		Pause,
		End
	}

	public delegate void OnGameStateChangeDelegate ();
	public static event OnGameStateChangeDelegate onGameStartEvent;
	public static event OnGameStateChangeDelegate onGameEndEvent;
	public static event OnGameStateChangeDelegate onGamePauseEvent;
	public static event OnGameStateChangeDelegate onGameResumeEvent;

	State state = State.Start;

	void Start ()
	{
		// Send game start event
		//onGameStartEvent();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(state){
		case State.Start:
			if(Input.GetButtonDown("Submit")){
				GameObject[] knights = GameObject.FindGameObjectsWithTag("Knight");
				if(knights.Length > 0) onGameStartEvent();
					
			}
			break;
		}
	}

	void ChangeState(State newSate)
	{
		state = newSate;
	}
}
