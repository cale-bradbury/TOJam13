using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



//TODO:
/*
 * add reset to player state
 * add camera for death state that stays static
 * add UI for velocity, fuel, using boost, altitude
 * calculate distance
 * add score
 * add orbs that refill fuel from proximity
 * add rings to give good score
*/



[System.Flags]
public enum GameState
{
	NullState = 1 << 0,
	Intro = 1 << 1,
	MainMenu = 1 << 2,
	Instructions = 1 << 3,
	//just incase we add this in
	Shop = 1 << 4,
	StartGame = 1 << 5,
	Game = 1 << 6,
	Summary = 1 << 7,
	End = 1 << 8,
	Collision = 1 << 9,
}


public class GameManager : Singleton<GameManager>
{
	public const string FIRSTLEVEL = "__init";
	public const string SECONDLEVEL = "__init 1";



	public GameState gameState { get; private set; }

	public GameState nextState = GameState.Intro;
	public GameState currentState = GameState.NullState;








	private float origDuration;




	#region event declaration

	public delegate void OnStateChangeHandler (GameState nextState);

	public static event OnStateChangeHandler OnStateChange;

	public delegate void OnGameEvent ();

	public static event OnGameEvent OnStateUpdateHandler, OnSwipeLeft, OnSwipeRight;

	public delegate void OnNewStartPosition (Vector3 pos, Quaternion rot);

	public static event OnNewStartPosition OnSetPosition;

	#endregion




	#region Monobehaviours

	void Awake ()
	{
		OnStateChange += HandleOnStateChange;
		Application.targetFrameRate = 60;
		#if UNITY_EDITOR
		Application.runInBackground = true;
		#endif
	}

	void Start ()
	{
		if (instance == this)
			SetGameState (GameState.Intro);

	}

	void OnDestroy ()
	{
		OnStateChange -= HandleOnStateChange;
	}

	void Update ()
	{
		//for modularly adding/removing functionality from a state's update loop
		if (OnStateUpdateHandler != null)
			OnStateUpdateHandler ();

	}

	void OnLevelWasLoaded (int level)
	{

	}

	#endregion


	#region EventHandlers


	public void SetGameState (GameState gameState)
	{
		if (gameState != currentState) {

			if (OnStateChange != null) {
				OnStateChange (gameState);
			}
			currentState = gameState;
		} else {
			Debug.Log ("ALREADY IN THIS STATE");

		}

	}

	public static void SwipeRight ()
	{
		if (OnSwipeRight != null) {
			OnSwipeRight ();
		}
	}

	public static void SwipeLeft ()
	{
		if (OnSwipeLeft != null) {
			OnSwipeLeft ();
		}
	}

	#endregion

	#region Public Methods

	public void PrepareGame ()
	{
		SetGameState (GameState.StartGame);
	}

	public void ResetGame ()
	{
		SetGameState (GameState.MainMenu);
		//		LoadLevel(FIRSTLEVEL);
	}

	public void NextLevel ()
	{
		//		LoadLevel(SECONDLEVEL);
	}

	#endregion

	void HandleOnStateChange (GameState state)
	{
		OnStateUpdateHandler = null;
		//				inTransition = true;
		OnStateUpdateHandler += () => {
			if (Input.GetKeyDown (KeyCode.R))
				Application.LoadLevel (Application.loadedLevel);
			if (Input.GetKeyDown (KeyCode.T))
				SetGameState (nextState);
		};
		switch (state) {
		case GameState.Intro:
			D.log ("intro state");

			nextState = GameState.MainMenu;

			StartCoroutine (Auto.Wait (0.1f, () => {

				SetGameState (nextState);
			}));
			break;
		case GameState.MainMenu:


			nextState = GameState.StartGame;
			break;
		case GameState.StartGame:

			nextState = GameState.Game;
			break;
		case GameState.Game:
			OnStateUpdateHandler += () => {
				
			};
			nextState = GameState.Summary;
			break;
		case GameState.Summary:


			nextState = GameState.End;
			break;
		case GameState.End:
			nextState = GameState.StartGame;
			break;
		case GameState.Collision:




			nextState = GameState.StartGame;

			break;
		}
		Debug.Log ("switching state to " + state);

	}


	//	public void AddToList<T> (T obj)
	//	{
	//		IDestroyable scientist = obj as IDestroyable;
	//		scientists.Add (scientist);
	//	}
	//
	//	public void RemoveFromList<T> (T obj)
	//	{
	//		IDestroyable scientist = obj as IDestroyable;
	//		scientists.Remove (scientist);
	//	}

}