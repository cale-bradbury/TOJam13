using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{


	bool firstGame = false;
	private string HIGH_SCORE = "highScore";
	public CampReflectFloat distanceInput;
	public int score;
	public int scoreBonuses;

	public Text inGameScoreLabel;
	public Text endGameScoreLabel;
	public Text bestScoreLabel;

	int bestScore;

	public void GetPrefs ()
	{
		if (!PlayerPrefs.HasKey (HIGH_SCORE)) {
			firstGame = true;
			PlayerPrefs.SetInt (HIGH_SCORE, 0);
			bestScore = 0;
		}
	}


	public void SubmitScore (int score)
	{

		if (!PlayerPrefs.HasKey (HIGH_SCORE) || PlayerPrefs.GetInt (HIGH_SCORE) < score) {
			PlayerPrefs.SetInt (HIGH_SCORE, score);
		}
	}


	public void CheckHighScore ()
	{
		int scoreToCheck = score;

		if (!firstGame && bestScore < PlayerPrefs.GetInt (HIGH_SCORE)) {
			bestScore = PlayerPrefs.GetInt (HIGH_SCORE);
		}

		if (firstGame) {
			bestScore = scoreToCheck;
			PlayerPrefs.SetInt (HIGH_SCORE, bestScore);
		
		} else if (scoreToCheck > bestScore) {
			bestScore = scoreToCheck;
			PlayerPrefs.SetInt (HIGH_SCORE, bestScore);


		} 
		SubmitScore (score);
	}

	public void AddBonus (int t)
	{
		scoreBonuses += t;
	}

	public void CalculateScore ()
	{
		if (GameManager.instance.currentState == GameState.StartGame)
			score = Mathf.RoundToInt (distanceInput.GetFloat ()) + scoreBonuses;
	}

	public void DisplayScore ()
	{
		string inGame = score.ToString ();
		string endGame = "S C O R E : " + score.ToString ();
		string bestScoreString = "B E S T : " + bestScore.ToString ();
		if (inGameScoreLabel.gameObject.activeInHierarchy)
			inGameScoreLabel.text = inGame;
		if (endGameScoreLabel.gameObject.activeInHierarchy)
			endGameScoreLabel.text = endGame;
		if (bestScoreLabel.gameObject.activeInHierarchy)
			bestScoreLabel.text = bestScoreString;

	}

	void Awake ()
	{
		GameManager.OnStateChange += HandleStateChange;
		GetPrefs ();
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
			CheckHighScore ();
			break;
		}
	}

	void Update ()
	{
		CalculateScore ();
		DisplayScore ();
	}
}
