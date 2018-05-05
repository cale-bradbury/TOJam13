using System.Collections;
using System.Collections.Generic;
using UnityEngine;

		
		
	
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


	//	[HideInInspector]
	public float currentPitch;
	//	[HideInInspector]
	public float currentVelocity;
	//Just checking Z velocity (airspeed)
	//	[HideInInspector]
	public float currentLift;
	
	public float startForce = 10;
	public float pitchInputSpeed = 30;


	float lastZPos;
	float currentZPos;

	float currentDrag = 0;

	
	
	
	
	
	
	public delegate void OnPlayerStateEvent (PlayerState nextState);

	public static event OnPlayerStateEvent OnPlayerStateChange;


	#region inputStuff

	public const string ANIM = "AnimState";
	AnimState currentAnimState = AnimState.Null;
	Animator[] anims;

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
		currentDrag = origDrag;
		currentZPos = transform.position.z;
		lastZPos = transform.position.z;
	}

	void Start ()
	{
		rb.AddForce ((transform.forward + new Vector3 (0, 0.2f, 0)) * startForce, ForceMode.Impulse);

	}

	

	void Update ()
	{
		currentZPos = transform.position.z;	
		currentVelocity = ((currentZPos - lastZPos) / Time.deltaTime);
		currentPitch = GetPlaneDotProduct ();


		if (currentPitch > 0 && currentVelocity > 25) {
			currentLift = currentPitch * currentVelocity * 1.5f;
		} else
			currentLift = 0;

//		transform.Rotate (transform.right, -1 * Time.deltaTime * rb.velocity.y, Space.Self);
		currentDrag = Mathf.Lerp (origDrag * 0.1f, origDrag * 3, ((currentPitch + 1) / 2));


		lastZPos = transform.position.z;

		if (canInput) {
			
			float vInput = Input.GetAxis ("Vertical");

			if (vInput != 0) {
				transform.Rotate (transform.right, Time.deltaTime * vInput * pitchInputSpeed, Space.Self);
			}


//			Vector3 tempEuler = transform.eulerAngles;
//
//			tempEuler.x = Mathf.Clamp (transform.eulerAngles.x, -80, 80);
//			transform.eulerAngles = tempEuler;
		}
	}

	void FixedUpdate ()
	{
		rb.AddForce (Vector3.up * currentLift, ForceMode.Acceleration);
		rb.drag = currentDrag;
	}

	float GetAngle (Vector3 from, Vector3 to)
	{
		from = new Vector3 (from.x, from.y, 20);
		to = new Vector3 (to.x, to.y, 20);
		return Vector3.Angle (from, to);
	}


	float GetPlaneDotProduct ()
	{
		return Vector3.Dot (Vector3.up, transform.forward);
	}

	void SmoothLookAt (Vector3 target, float smooth)
	{
		Vector3 dir = target - transform.position + transform.up;
		Quaternion targetRotation = Quaternion.LookRotation (dir);
		targetRotation.x = 0;
		targetRotation.y = 0;
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * smooth);
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
