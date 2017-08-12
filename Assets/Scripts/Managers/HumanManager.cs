using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour {

	public enum State
	{
		Idle,
		Active,
		Dead,
		Celebrate
	}

	State state = State.Idle;
	HumanMoveBehavior moveBehavior;

	void Awake ()
	{
		// Add event subscription with callback
		GameManager.onGameStartEvent += EnablePlayer;

		// Get Behaviors
		moveBehavior = GetComponent<HumanMoveBehavior>();
	}

	void Start()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void EnablePlayer()
	{
		Debug.Log("Player enabled!");
		ChangeState(State.Active);
	}

	void ChangeState(State newState)
	{
		state = newState;
		switch(state)
		{
		case State.Idle:
			break;
		case State.Active:
			moveBehavior.ChangeState(HumanMoveBehavior.State.Active);
			break;
		case State.Dead: 
			moveBehavior.ChangeState(HumanMoveBehavior.State.Idle);
			break;
		case State.Celebrate: 
			break;
		}
	}

	void OnDestroy()
	{
		// Remove event subscription
		GameManager.onGameStartEvent -= EnablePlayer;
	}
}
