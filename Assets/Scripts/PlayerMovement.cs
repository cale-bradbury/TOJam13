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
		[HideInInspector]
		public float currentAngle;
		[HideInInspector]
		public float currentVelocity;
		[HideInInspector]
		public float currentLift;
		
		
		
		
		
		
		
		
		public delegate void OnPlayerStateEvent(PlayerState nextState);

		public static event OnPlayerStateEvent OnPlayerStateChange;




		public const string ANIM = "AnimState";
		AnimState currentAnimState = AnimState.Null;
		Animator[] anims;

		[HideInInspector]
		public bool canInput;

		public LayerMask mask;


		public IUserInputProxy _input;
		InputModel _i = new InputModel();


		IRaycastable[] raycasters;
		public float hitDistance { get; set; }
		public RaycastHit2D hit { get; set; }


		public void SetInputType(IUserInputProxy _inputType)
		{
			_input = _inputType;
		}

		void HandleOnStateChange(GameState state)
		{
			switch(state)
			{
			case GameState.MainMenu:
				Disable ();
				break;
			case GameState.StartGame:
				Enable ();
				
				break;
			case GameState.Summary:
				Reset ();
				break;
			case GameState.Collision:
				break;
			}
		}



	Vector3 inputAxis;
	public Camera camera;
	public float speed = 1.0f;
	public float angle = 1.0f;
	private Vector3 lastPosition;
	private Vector3 positionChange;
	private Vector3 mousePos;
	private float velocity;

	public static int numLives;

	public static float distTraveled;

	public float rotationSpeed = 5;

	void Awake()
	{
		
	}

	void Start () 
	{
		inputAxis = new Vector3(0,0,0);
		numLives = 50;

	}

	void Update () 
	{
		distTraveled = transform.parent.transform.localPosition.z;

		lastPosition = transform.position;

		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;


			if (Physics.Raycast(ray, out hit)) 
			{
				mousePos = new Vector3(hit.point.x,hit.point.y,transform.position.z);
				transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime);
			}

		


		positionChange = transform.position - lastPosition;
		velocity = positionChange.magnitude;
		angle = velocity * 3;
		if (positionChange.x <= 0) 
			angle *= -1;
		if (mousePos.x - transform.position.x <= 0.3f)
			transform.RotateAround(transform.forward, GetAngle(transform.position, mousePos)*Time.deltaTime);
		if (mousePos.x - transform.position.x >= 0.3f)
			transform.RotateAround(transform.forward, GetAngle(transform.position, mousePos)*Time.deltaTime *-1);
	}

	float GetAngle(Vector3 from, Vector3 to)
	{
		from = new Vector3(from.x, from.y, 20);
		to = new Vector3(to.x, to.y, 20);
		return Vector3.Angle(from, to);
	}





	void SmoothLookAt(Vector3 target, float smooth)
	{
		Vector3 dir = target - transform.position + transform.up;
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		targetRotation.x = 0;
		targetRotation.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
	}

	void OnGameOver()
	{
		distTraveled = 0;
		numLives = 50;
	}
		
		
		


}
