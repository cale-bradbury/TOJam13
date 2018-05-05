using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjRef : Singleton<ObjRef>
{

	public static Transform playerTransform;
	public static PlayerMovement playerMove;
	public static Rigidbody playerRb;


	public static Canvas hudUI;


	public static Transform landingZone;



	void Awake()
	{
		playerTransform = GameObject.Find ("Player").transform;
		playerMove = playerTransform.GetComponent<PlayerMovement> ();
		playerRb = playerTransform.GetComponent<Rigidbody> ();

	}

}
