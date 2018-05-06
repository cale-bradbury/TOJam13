using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
	public CampReflectFloat distanceInput;
	public int score;
	public int scoreBonuses;

	public Text inGameScoreLabel;
	public Text endGameScoreLabel;

	public void CalculateScore ()
	{
		score = Mathf.RoundToInt (distanceInput.GetFloat ()) + scoreBonuses;
	}

	public void DisplayScore ()
	{
		string s = "Score: " + score.ToString ();
		if (inGameScoreLabel.gameObject.activeInHierarchy)
			inGameScoreLabel.text = s;
		if (endGameScoreLabel.gameObject.activeInHierarchy)
			endGameScoreLabel.text = s;
	}

	void Awake ()
	{
		GameManager.OnStateChange += HandleStateChange;
	}

	void OnDestroy ()
	{
		GameManager.OnStateChange -= HandleStateChange;
	}

	void HandleStateChange (GameState state)
	{
		switch (state) {
		case GameState.Intro:			
			score = 0;
			scoreBonuses = 0;
			break;
		case GameState.StartGame:
			score = 0;
			scoreBonuses = 0;
			break;
		case GameState.Collision:
			
			break;
		}
	}

	void Update ()
	{
		CalculateScore ();
		DisplayScore ();
	}
}
