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



	public CampBindAxis fogTransform;
	public GameObject endGameUI;
	public GameObject inGameUI;
	public GameObject introUI;




	private float origDuration;




	#region event declaration

	public delegate void OnStateChangeHandler (GameState nextState);

	public static event OnStateChangeHandler OnStateChange;

	public delegate void OnGameEvent ();

	public static event OnGameEvent OnStateUpdateHandler;

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
			Debug.Log ("ALREADY IN THIS STATE  " + gameState);

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
				SetGameState (GameState.Collision);
			if (Input.GetKeyDown (KeyCode.T))
				SetGameState (nextState);
		};
		StopAllCoroutines ();
		switch (state) {

		case GameState.Intro:
			D.log ("intro state");
			fogTransform.enabled = true;
			nextState = GameState.MainMenu;
			FadeAndToggle (introUI, true, 1, 1, 0);
			FadeAndToggle (inGameUI, false, 0, 0.01f, 0);
			FadeAndToggle (endGameUI, false, 0, 0.01f, 0);
//			StartCoroutine (Auto.Wait (0.1f, () => {
//
//				SetGameState (nextState);
//			}));
			break;
		case GameState.MainMenu:


			nextState = GameState.StartGame;
			break;
		case GameState.StartGame:
			FadeAndToggle (introUI, false, 0, 0.25f, 0);
			FadeAndToggle (inGameUI, true, 1, 0.5f, 0.25f);
			FadeAndToggle (endGameUI, false, 0, 0.01f, 0);
			fogTransform.enabled = true;
			nextState = GameState.Game;
			break;
		case GameState.Game:
			fogTransform.enabled = true;
			nextState = GameState.Summary;
			break;
		case GameState.Summary:


			nextState = GameState.End;
			break;
		case GameState.End:
			nextState = GameState.StartGame;
			break;
		case GameState.Collision:
			FadeAndToggle (introUI, false, 0, 0.01f, 0);
			FadeAndToggle (inGameUI, false, 0, 1f, 0);
			FadeAndToggle (endGameUI, true, 1, 1f, 1.2f);
			fogTransform.enabled = false;


			nextState = GameState.StartGame;
		

			break;
		}
		Debug.Log ("switching state to " + state);

	}



	void FadeAndToggle (GameObject gObj, bool active, float targetAlpha, float duration, float delay)
	{
		StartCoroutine (Auto.Wait (0.001f + delay, () => {
		
		
			CanvasGroup cGroup = gObj.GetComponent<CanvasGroup> ();
			if (active == true) {
				if (targetAlpha == 1 && cGroup != null) {
					cGroup.alpha = 0;
				}
				gObj.SetActive (true);
			}
			if (cGroup != null) {
				StartCoroutine (cGroup.FadeAlpha (targetAlpha, duration, EaseType.SmoothStepInOut, () => {
					gObj.SetActive (active);
				}));
			} else {
				gObj.SetActive (active);
			}
		}));
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