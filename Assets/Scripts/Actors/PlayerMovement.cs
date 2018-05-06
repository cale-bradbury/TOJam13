using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlayerState
{
	NullState,
	Intro,
	Flying,
	Danger,
	Crashed
}


public class PlayerMovement : MonoEx
{
	public static PlayerMovement instance;


	//roll
	//pitch
	//current velocity
	//
	//boost
	//

	public Transform CameraTargetLook;
	public Vector3 CameraTargetLookDefault;
	public Vector3 CameraTargetLookDown;
	public Vector3 CameraTargetLookUp;
	public Vector3 CameraTargetLookLeft;
	public Vector3 CameraTargetLookRight;
	public float CameraTargetLookSmoothing = .2f;




	bool isPlayable = true;

	public Transform planeRoot;
	public Transform boostTrail;
	public FlickerElement pullUp;
	public FlickerElement danger;
	public AudioHelm.HelmController warningSound;
	public int warningSoundKeyPullup;
	public int warningSoundKeyDanger;
	public GameObject renderMesh;
	public GameObject deathMesh;
	public Light boostLight;

	//	[Header ("FOR REFLECTION :)")]
	//THESE ARE THE PUBLIC VARS FOR CALE
	[HideInInspector]
	public float currentPitch;
	//	[HideInInspector]
	public float currentVelocity;
	[HideInInspector]
	public float currentLift;
	[HideInInspector]
	public float currentAltitude;
	[HideInInspector]
	public float currentDistance;
	[HideInInspector]
	public float currentFuel = 100;
	[HideInInspector]
	public float boostPercent;

	[HideInInspector]
	public float fuelPercent;

	[HideInInspector]
	public float altitudePercent;

	[HideInInspector]
	public float velocityPercent;

	[HideInInspector]
	public float pitchPercent;

	[HideInInspector]
	public float distance = 0;


	public float pitchInputSpeed = 30;
	public float smoothTime = 10;


	public float liftFactor = 1.2f;
	public float fuelConsumptionFactor = 1;
	public float MaxFuel = 100;

	public float velocityForce = 50;
	public float lateralVelocityForce = 30;

	//	float boostVelocity

	public float maxBoostFactor = 3;
	public float boostIncrease = 1;
	public float boostDecrease = 3;

	Vector3 origTrailScale;
	public Vector3 trailMin, trailMax, trailMaxBoost;
	public float minLight, maxLight;
	public AnimationCurve trailCurve;


	[HideInInspector]
	public PlayerState currentState;

	float dropVelocity = 20;
	float velocityAdd = 0;
	float boostFactor = 1;
	float origVelocityForce = 50;
	float maxAltitude = 150;
	float hInput = 0;
	float maxVelocity = 110;

	public delegate void OnPlayerStateEvent (PlayerState nextState);

	public static event OnPlayerStateEvent OnPlayerStateChange;


	public bool canInput = false;

	void HandleOnStateChange (GameState state)
	{
		switch (state) {
		case GameState.Intro:
			//			Disable ();
			canInput = false;
			rb.isKinematic = true;
			break;
		case GameState.StartGame:
			Reset ();
			Enable ();
			//			renderMesh.SetActive (true);
			//			deathMesh.SetActive (false);
			canInput = true;

			break;
		case GameState.Summary:
			Reset ();
			break;
		case GameState.Collision:
			//			renderMesh.SetActive (false);
			//			GameObject.Instantiate (deathMesh, renderMesh.transform.position, renderMesh.transform.rotation);
			//			deathMesh.SetActive (true);
			SetState (PlayerState.Crashed);
			break;
		}
	}

	void HandlePlayerStateChange (PlayerState state)
	{
		switch (state) {
		case PlayerState.Intro:
			Enable ();
			canInput = false;
			break;
		case PlayerState.Flying:
			canInput = true;
			break;
		case PlayerState.Danger:
			canInput = true;
			break;
		case PlayerState.Crashed:
			canInput = false;

			StartCoroutine (Auto.Wait (2, () => {
				//				deathMesh.SetActive (false);
				//				renderMesh.SetActive (true);
				Disable ();
				Reset ();
			}));
			break;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();
		instance = this;
		GameManager.OnStateChange += HandleOnStateChange;
	}

	void OnDestroy ()
	{
		GameManager.OnStateChange -= HandleOnStateChange;
	}

	protected override void Init ()
	{
		base.Init ();
		rb.isKinematic = true;
		currentState = PlayerState.NullState;
		SetState (PlayerState.Intro);
		origVelocityForce = velocityForce;
		origTrailScale = boostTrail.localScale;
		boostTrail.localScale = trailMin;
		canInput = false;
		distance = 0;
	}

	public override void Reset ()
	{
		base.Reset ();
		currentVelocity = 0;
		distance = 0;
		currentFuel = MaxFuel;
		velocityForce = origVelocityForce;
		boostTrail.localScale = origTrailScale;

	}

	public void AddFuel (float fuel)
	{
		currentFuel = Mathf.Min (currentFuel + fuel, MaxFuel);
	}



	void Update ()
	{
		distance = transform.position.z;
		var localVel = transform.InverseTransformDirection (new Vector3 (rb.velocity.x, 0, rb.velocity.z));
		currentVelocity = localVel.z;
		currentAltitude = transform.position.y;


		//PITCH
		currentPitch = GetPlaneDotProduct ();

		if (currentPitch > 0) {
			velocityAdd = -currentPitch * dropVelocity;
			velocityForce += (-currentPitch * Time.deltaTime * 28);
			if (currentVelocity > 20) {
				currentLift = currentPitch * (currentVelocity * liftFactor);
				velocityForce -= 2 * Time.deltaTime;
			} else
				currentLift = -(20 - currentVelocity);
		} else if (currentPitch < 0) {
			velocityAdd = -currentPitch * dropVelocity * 4;
			velocityForce += (-currentPitch * Time.deltaTime * 17);
			currentLift = 0;
		}

		Vector3 target = CameraTargetLookDefault;

		if (canInput) {

			float vInput = Input.GetAxis ("Vertical");
			hInput = Input.GetAxis ("Horizontal");

			if (vInput != 0) {
				transform.Rotate (transform.right, Time.deltaTime * vInput * pitchInputSpeed, Space.Self);
				if (vInput > 0) {
					target = Vector3.Lerp (target, CameraTargetLookUp, vInput);
				} else {
					target = Vector3.Lerp (target, CameraTargetLookDown, -vInput);
				}
			}
			Vector3 horizontalAdtionalTarget = Vector3.zero;
			if (hInput > 0) {
				horizontalAdtionalTarget = Vector3.Lerp (horizontalAdtionalTarget, CameraTargetLookRight, hInput);
			} else {
				horizontalAdtionalTarget = Vector3.Lerp (horizontalAdtionalTarget, CameraTargetLookLeft, -hInput);
			}
			target += horizontalAdtionalTarget;
		}
		CameraTargetLook.localPosition = Vector3.Lerp (CameraTargetLook.localPosition, target, CameraTargetLookSmoothing);

		float t = currentVelocity / 90;
		boostTrail.localScale = Vector3.Lerp (trailMin, trailMax, t);

		//BOOST
		if (currentFuel > 0) {
			
			if (Input.GetKey (KeyCode.Space) && currentFuel > 0) {
				boostTrail.localScale = Vector3.Lerp (trailMax, trailMaxBoost, boostPercent * 1.5f);
				currentFuel -= fuelConsumptionFactor * Time.deltaTime;
				if (velocityForce < origVelocityForce) {
					velocityForce += 15 * Time.deltaTime;
				}
				if (boostFactor < maxBoostFactor) {
					boostFactor += boostIncrease * Time.deltaTime;
				}
			}
			if (boostFactor > 1 && Input.GetKey (KeyCode.Space) == false) {
				boostFactor -= boostDecrease * Time.deltaTime;
			}
		}

		boostLight.intensity = Mathf.Lerp (minLight, maxLight, boostPercent);


		altitudePercent = currentAltitude / maxAltitude;
		boostPercent = (boostFactor - 1) / (maxBoostFactor - 1);
		fuelPercent = currentFuel / MaxFuel;
		velocityPercent = currentVelocity / maxVelocity;
		pitchPercent = (currentPitch + 1) / 2;





		//ROLL

		Quaternion targetRot = Quaternion.Euler (0, 0, 35 * -hInput);
		planeRoot.localRotation = Quaternion.Slerp (planeRoot.localRotation, targetRot, smoothTime * Time.deltaTime);

		if (transform.position.y < 20) {
			if (!pullUp.gameObject.activeSelf) {
				warningSound.NoteOn (warningSoundKeyPullup);
				pullUp.gameObject.SetActive (true);
			}
		}
		if (transform.position.y > 20) {
			if (pullUp.gameObject.activeSelf) {
				warningSound.NoteOff (warningSoundKeyPullup);
				pullUp.gameObject.SetActive (false);
			}

		}

		if (transform.position.y < 8) {
			if (!danger.gameObject.activeSelf) {
				warningSound.NoteOn (warningSoundKeyDanger);
				danger.gameObject.SetActive (true);
			}
		}
		if (transform.position.y > 8) {
			if (danger.gameObject.activeSelf) {
				warningSound.NoteOff (warningSoundKeyDanger);
				danger.gameObject.SetActive (false);
			}
		}

		if (transform.position.y < 2 && currentState == PlayerState.Flying) {
			SetState (PlayerState.Danger);
		}
		if (currentState == PlayerState.Danger && transform.position.y > 2) {
			SetState (PlayerState.Flying);
		}
		if (transform.position.y < 0 && GameManager.instance.currentState != GameState.Collision) {
			GameManager.instance.SetGameState (GameState.Collision);
		}
		if (transform.position.y > maxAltitude) {
			Vector3 temp = transform.position;
			temp.y = maxAltitude;
			transform.position = temp;
		}
	}

	public void SetState (PlayerState state)
	{

		if (state != currentState) {
			print ("setting player state to " + state);
			if (OnPlayerStateChange != null) {
				OnPlayerStateChange (state);
			}
			currentState = state;
		} else {
			Debug.Log ("ALREADY IN THIS PlayerState  " + state);

		}

	}

	void FixedUpdate ()
	{
		if (GameManager.instance.currentState == GameState.StartGame || GameManager.instance.currentState == GameState.Game)
			rb.velocity = (transform.forward * ((velocityForce + velocityAdd) * boostFactor) + (Vector3.up * currentLift) + (transform.right * hInput * lateralVelocityForce));
	}

	float GetPlaneDotProduct ()
	{
		return Vector3.Dot (Vector3.up, transform.forward);
	}

	void OnGameOver ()
	{
		//		canInput = false;
	}




	void OnCollisionEnter (Collision col)
	{
		if (currentState != PlayerState.Crashed && GameManager.instance.currentState != GameState.Collision)
			GameManager.instance.SetGameState (GameState.Collision);
	}



}
