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


	//	[HideInInspector]
	public float currentPitch;
	//	[HideInInspector]
	public float currentVelocity;
	//	[HideInInspector]
	public float currentLift;
	
	public float startForce = 10;
	public float pitchInputSpeed = 30;
	public float liftFactor = 1.2f;
	public float maxDragFactor = 5;
	public float dropVelocity = 180;
	public AnimationCurve pitchCurve;

	float lastZPos;
	float currentZPos;
	float currentDrag = 0;
	float velocityAdd = 0;

	Vector3 targetVelocity = Vector3.zero;
	float tempYVel;


	
	
	
	
	
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




		var localVel = transform.InverseTransformDirection (new Vector3 (rb.velocity.x, 0, rb.velocity.z));
		currentVelocity = localVel.z;

		currentPitch = GetPlaneDotProduct ();



		if (currentPitch > 0) {
			velocityAdd = -currentPitch * dropVelocity;		
			currentLift = currentPitch * currentVelocity * liftFactor;

		} else if (currentPitch < 0) {

			velocityAdd = -currentPitch * dropVelocity * 4;		
			currentLift = (currentPitch * currentVelocity * liftFactor) / 2;
		}

			



//		currentDrag = Mathf.Lerp (0, origDrag * maxDragFactor, ((currentPitch + 1) / 2));


		lastZPos = transform.position.z;

		if (canInput) {
			
			float vInput = Input.GetAxis ("Vertical");

			if (vInput != 0) {
				transform.Rotate (transform.right, Time.deltaTime * vInput * pitchInputSpeed, Space.Self);
			}
		}
			


	}

	void FixedUpdate ()
	{
		rb.AddForce (Vector3.up * currentLift * Mathf.Abs (currentPitch * 3), ForceMode.Force);
		rb.AddForce (transform.forward * velocityAdd, ForceMode.Force);


//		rb.drag = currentDrag;
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
