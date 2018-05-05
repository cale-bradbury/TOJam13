using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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


public class GameManager : Singleton<GameManager>, IAddableList
{
	public const string FIRSTLEVEL = "__init";
	public const string SECONDLEVEL = "__init 1";



	public GameState gameState { get; private set; }

	public GameState nextState = GameState.Intro;
	public GameState currentState = GameState.NullState;

	[HideInInspector]
	public List<IDestroyable> scientists;



	public float durationBeforeFlare { get; set; }

	[HideInInspector]
	public float startFlareValue = 10;

	private float origDuration;
	public float rescueIncrement = 3;

	int numCrashes = 0;

	#region event declaration

	public delegate void OnStateChangeHandler(GameState nextState);

	public static event OnStateChangeHandler OnStateChange;

	public delegate void OnGameEvent();

	public static event OnGameEvent OnStateUpdateHandler, OnSwipeLeft, OnSwipeRight;

	public delegate void OnNewStartPosition(Vector3 pos, Quaternion rot);

	public static event OnNewStartPosition OnSetPosition;

	#endregion




	#region Monobehaviours

	void Awake()
	{

		durationBeforeFlare = 180;		
		OnStateChange += HandleOnStateChange;
		origDuration = durationBeforeFlare;
		Application.targetFrameRate = 60;
		#if UNITY_EDITOR
		Application.runInBackground = true;
		#endif
	}

	void Start()
	{
		if (instance == this)
			SetGameState(GameState.Intro);

	}

	void OnDestroy()
	{
		OnStateChange -= HandleOnStateChange;
	}

	void Update()
	{
		//for modularly adding/removing functionality from a state's update loop
		if (OnStateUpdateHandler != null)
			OnStateUpdateHandler();

	}

	void OnLevelWasLoaded(int level)
	{

	}

	#endregion


	#region EventHandlers


	public void SetGameState(GameState gameState)
	{
		if (gameState != currentState)
		{

			if (OnStateChange != null)
			{
				OnStateChange(gameState);
			}
			currentState = gameState;
		}
		else
		{
			Debug.Log("ALREADY IN THIS STATE");

		}

	}

	public static void SwipeRight()
	{
		if (OnSwipeRight != null)
		{
			OnSwipeRight();
		}
	}

	public static void SwipeLeft()
	{
		if (OnSwipeLeft != null)
		{
			OnSwipeLeft();
		}
	}

	#endregion

	#region Public Methods

	public void PrepareGame()
	{
		SetGameState(GameState.StartGame);
	}

	public void ResetGame()
	{
		SetGameState(GameState.MainMenu);
		//		LoadLevel(FIRSTLEVEL);
	}

	public void NextLevel()
	{
		//		LoadLevel(SECONDLEVEL);
	}

	#endregion

	void HandleOnStateChange(GameState state)
	{
		OnStateUpdateHandler = null;
		//				inTransition = true;
		OnStateUpdateHandler += () => {
			if (Input.GetKeyDown(KeyCode.Space))
				SetGameState(nextState);
		};
		switch (state)
		{
		case GameState.Intro:
			D.log ("intro state");

			nextState = GameState.MainMenu;

			StartCoroutine (Auto.Wait (0.1f, () => {

				SetGameState(nextState);
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
				if (durationBeforeFlare > 0)
				{
					durationBeforeFlare -= Time.deltaTime;
				}
				else if (durationBeforeFlare <= 0)
				{
					SetGameState(GameState.End);
				}
			};
			nextState = GameState.Summary;
			break;
		case GameState.Summary:
			durationBeforeFlare = origDuration;

			nextState = GameState.End;
			break;
		case GameState.End:
			nextState = GameState.StartGame;
			break;
		case GameState.Collision:



			numCrashes += 1;
			nextState = GameState.StartGame;

			break;
		}
		Debug.Log("switching state to " + state);

	}


	public void AddToList<T> (T obj)
	{
		IDestroyable scientist = obj as IDestroyable;
		scientists.Add (scientist);
	}

	public void RemoveFromList<T> (T obj)
	{
		IDestroyable scientist = obj as IDestroyable;
		scientists.Remove (scientist);
	}

}