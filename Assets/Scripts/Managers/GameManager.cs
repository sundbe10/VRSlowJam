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
	public static event OnGameStateChangeDelegate onDragonWinEvent;
	public static event OnGameStateChangeDelegate onKnightsWinEvent;

	public delegate void OnKnightsSetDelegate (int numKnights);
	public static event OnKnightsSetDelegate onKnightsSetEvent;

	State state = State.Start;
	List<GameObject> knights = new List<GameObject>();
	int numDeadKnights;
	SoundManager soundManager;

	void Start ()
	{
		KnightSpawnBehavior.onKnightAdded += KnightAdded;
		KnightSpawnBehavior.onKnightRemove += KnightRemoved;
		KnightManager.onKnightDie += KnightDie;
		DragonManager.onDragonDie += DragonDie;

		soundManager = GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(state){
		case State.Start:
			if(Input.GetButtonDown("Submit")){
				if(knights.Count > 0){
					onKnightsSetEvent(knights.Count);
					onGameStartEvent();
				}
			}
			break;
		}
	}

	void KnightAdded(GameObject knight){
		Debug.Log(knight);
		if(!knights.Contains(knight)){
			knights.Add(knight);
		}
	}

	void KnightRemoved(GameObject knight){
		knights.Remove(knight);
	}

	void KnightDie(GameObject knight){
		Debug.Log("knight die");
		numDeadKnights++;
		if(numDeadKnights == knights.Count && state != State.End){
			ChangeState(State.End);
			onDragonWinEvent();
		}
	}

	void DragonDie(){
		Debug.Log("Dragon Die");
		ChangeState(State.End);
		StartCoroutine("DelayedCelebrate");
		onKnightsWinEvent();
	}

	void ChangeState(State newSate)
	{
		state = newSate;
	}

	IEnumerator DelayedCelebrate(){
		yield return new WaitForSeconds(1f);
		soundManager.PlaySound("knightCelebrate");
	}

	void OnDestroy(){
		KnightSpawnBehavior.onKnightAdded -= KnightAdded;
		KnightSpawnBehavior.onKnightRemove -= KnightRemoved;
		KnightManager.onKnightDie -= KnightDie;
		DragonManager.onDragonDie -= DragonDie;
	}
		
}
