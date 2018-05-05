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
		public float currentDotProduct;
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







		void Awake()
		{
			
		}

		void Start () 
		{


		}

		void Update () 
		{

		}

		float GetAngle(Vector3 from, Vector3 to)
		{
			from = new Vector3(from.x, from.y, 20);
			to = new Vector3(to.x, to.y, 20);
			return Vector3.Angle(from, to);
		}


		float GetPlaneDotProduct()
		{
			return Vector3.Dot (Vector3.up, transform.forward);
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

		}
			
		
		


}
