using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameRigidBody : MonoEx
{

	protected override void Awake ()
	{
		base.Awake ();
		GameManager.OnStateChange += HandleStateChange;
		GetComponent<Collider> ().enabled = false;
	}

	void OnDestroy ()
	{
		GameManager.OnStateChange -= HandleStateChange;
	}

	void HandleStateChange (GameState state)
	{
		switch (state) {
		case GameState.Intro:
			//			Disable ();
			GetComponent<Collider> ().enabled = false;

			break;
		case GameState.StartGame:
			Reset ();
			Disable ();


			break;
		case GameState.Summary:
			
			break;
		case GameState.Collision:
			Enable ();
			
			GetComponent<Collider> ().enabled = true;
			rb.isKinematic = false;

			break;
		}
	}
}
