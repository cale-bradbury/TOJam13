using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    public static ScoreManager instance;
	public CampReflectFloat distanceInput;
	public int score;
	public int scoreBonuses;

	public Text inGameScoreLabel;
	public Text endGameScoreLabel;

	public void CalculateScore ()
	{
		if (GameManager.instance.currentState == GameState.StartGame)
			score = Mathf.RoundToInt (distanceInput.GetFloat ()) + scoreBonuses;
	}

	public void DisplayScore ()
	{
		string inGame = score.ToString ();
		string endGame = "S C O R E : " + score.ToString ();
		if (inGameScoreLabel.gameObject.activeInHierarchy)
			inGameScoreLabel.text = inGame;
		if (endGameScoreLabel.gameObject.activeInHierarchy)
			endGameScoreLabel.text = endGame;
	}

    public void AddBonus(int amount)
    {
        scoreBonuses += amount;
    }

	void Awake ()
    {
        instance = this;
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
