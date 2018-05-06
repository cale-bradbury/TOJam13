using System.Collections;
using System.Collections.Generic;
using UnityEngine;

		
/// <summary>
/// NOTES FOR MYSELF FOR TOMORROW::::
/// 
/// Make velocity manually instead of add force.
/// plane should immediately adjust y from angle of attack 
/// (if plane tilts above 0, should immediately go up)
/// all y calculations for plane should be made explicitly by its angle 
/// unless under a specified stall speed at which point negative lift factor gradually worsens
/// 
/// need to calculate all needed forces and apply them manually in fixedupdate per frame
/// </summary>
	
public enum PlayerState
{
	NullState,
	Intro,
	Flying,
	Landing,
	Landed,
	Crashed,
	Takeoff,
	Dead
}

		
public class PlayerMovement : MonoEx, IRaycastable
{
	public enum AnimState
	{
		Idle = 0,
		Landing = 1,
		Landed = 2,
		LandedIdle = 3,
		Takeoff = 4,
		Null = 5,
	}
	
	
	//roll
	//pitch
	//current velocity
	//
	//boost
	//



	bool isPlayable = true;

	public Transform planeRoot;
	public Transform boostTrail;
	public Light boostLight;


	//THESE ARE THE PUBLIC VARS FOR CALE
	[HideInInspector]
	public float currentPitch;
	[HideInInspector]
	public float currentVelocity;
	[HideInInspector]
	public float currentLift;
	[HideInInspector]
	public float currentAltitutde;
	[HideInInspector]
	public float currentDistance;
	[HideInInspector]
	public float currentFuel = 100;


	[Header ("FOR REFLECTION :)")]
	public float boostPercent;



	public float pitchInputSpeed = 30;
	public float smoothTime = 10;


	public float liftFactor = 1.2f;
	public float fuelConsumptionFactor = 1;
	public float MaxFuel = 100;

	public float velocityForce = 50;
	public float lateralVelocityForce = 30;

	public float maxBoostFactor = 3;
	public float boostIncrease = 1;
	public float boostDecrease = 3;

	Vector3 origTrailScale;
	public Vector3 trailMin, trailMax, trailMaxBoost;
	public float minLight, maxLight;
	public AnimationCurve trailCurve;

	float dropVelocity = 20;
	float velocityAdd = 0;
	float boostFactor = 1;
	float origVelocityForce = 50;
	float hInput = 0;

	public delegate void OnPlayerStateEvent (PlayerState nextState);

	public static event OnPlayerStateEvent OnPlayerStateChange;

	#region inputStuff

	//	[HideInInspector]
	public bool canInput = true;

	public LayerMask mask;


	public IUserInputProxy _input;
	InputModel _i = new InputModel ();


	IRaycastable[] raycasters;

	public float hitDistance { get; set; }

	public RaycastHit2D hit { get; set; }


	public void SetInputType (IUserInputProxy _inputType)
	{
		_input = _inputType;
	}

	#endregion

	void HandleOnStateChange (GameState state)
	{
		switch (state) {
		case GameState.MainMenu:
			Disable ();
			break;
		case GameState.StartGame:
			Enable ();
			canInput = true;
			
			break;
		case GameState.Summary:
			Reset ();
			break;
		case GameState.Collision:
			break;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Init ()
	{
		base.Init ();
		origVelocityForce = velocityForce;
		origTrailScale = boostTrail.localScale;
		boostTrail.localScale = trailMin;

	}

	void Update ()
	{
		
		var localVel = transform.InverseTransformDirection (new Vector3 (rb.velocity.x, 0, rb.velocity.z));
		currentVelocity = localVel.z;



		//PITCH
		currentPitch = GetPlaneDotProduct ();

		if (currentPitch > 0) {
			velocityAdd = -currentPitch * dropVelocity;
			velocityForce += (-currentPitch * Time.deltaTime * 5);
			if (currentVelocity > 20) {
				currentLift = currentPitch * (currentVelocity * liftFactor);
				velocityForce -= 2 * Time.deltaTime;
			} else
				currentLift = -(20 - currentVelocity);
		} else if (currentPitch < 0) {
			velocityAdd = -currentPitch * dropVelocity * 4;
			velocityForce += (-currentPitch * Time.deltaTime * 20);
			currentLift = 0;
		}



		if (canInput) {
			
			float vInput = Input.GetAxis ("Vertical");
			hInput = Input.GetAxis ("Horizontal");

			if (vInput != 0) {
				transform.Rotate (transform.right, Time.deltaTime * vInput * pitchInputSpeed, Space.Self);
			}
		}

		float t = currentVelocity / 90;
		boostTrail.localScale = Vector3.Lerp (trailMin, trailMax, t);

		//BOOST
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
		boostPercent = (boostFactor - 1) / (maxBoostFactor - 1);

		boostLight.intensity = Mathf.Lerp (minLight, maxLight, boostPercent);


		//ROLL

		Quaternion targetRot = Quaternion.Euler (0, 0, 35 * -hInput);
		planeRoot.localRotation = Quaternion.Slerp (planeRoot.localRotation, targetRot, smoothTime * Time.deltaTime);

	}

	void FixedUpdate ()
	{

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

	public override void Enable ()
	{
		base.Enable ();

	}

	public override void Disable ()
	{
		base.Disable ();
	}

		


}
