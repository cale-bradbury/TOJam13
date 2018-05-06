using UnityEngine;

public class CameraFollow : MonoEx
{

	public Transform targetPosition;
	public Transform targetLookAt;


	public Transform target;
	public Transform targetRotation;

	public float smoothSpeed = 0.125f;
	public float smoothLookSpeed = 0.125f;

	public float minY, maxY;
	public Vector3 offset;
	Vector3 m_targetPosition = Vector3.zero;
	bool followPlayer = true;


	void Awake ()
	{
		GameManager.OnStateChange += HandleStateChange;
	}

	void OnDestroy ()
	{
		GameManager.OnStateChange -= HandleStateChange;
	}



	void HandleStateChange (GameState state)
	{
		if (state == GameState.Collision) {
			followPlayer = false;	
		} else if (state == GameState.End) {
			
		} else if (state == GameState.StartGame || state == GameState.Game) {
			Reset ();
			Enable ();
			followPlayer = true;
		}
	}

	void LateUpdate ()
	{
		if (followPlayer) {
			
			Vector3 targetPos = targetPosition.position;
			if (targetPos.y < minY) {
				targetPos.y = minY;
			}
			if (targetPos.y > maxY) {
				targetPos.y = maxY;
			}
			transform.position = targetPos;
		}
		Quaternion rot = transform.rotation;
		transform.LookAt (targetLookAt);
		Vector3 temp = rot.eulerAngles;

		temp.z = target.rotation.eulerAngles.z;
		rot.eulerAngles = temp;
		transform.rotation = Quaternion.Lerp (rot, transform.rotation, Time.deltaTime * 4);
		return;

//		Vector3 desiredPosition = target.position + offset;
//
//		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//
//		if (smoothedPosition.y < minY)
//			smoothedPosition.y = minY;
//		transform.position = smoothedPosition;
//
//		Quaternion targetRot = targetRotation.localRotation;
//		transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRot, smoothLookSpeed * Time.deltaTime);
//

		// Get the inverse of the players velocity
//		Vector3 direction = -(target.transform.GetComponent<Rigidbody> ().velocity.normalized);

		//  Set the position of the camera relative to the player, with some distance and height
//		m_targetPosition = target.transform.position + (direction * offset.z) + (Vector3.up * offset.y) + (Vector3.right * Input.GetAxis ("Horizontal"));
//		m_targetPosition.x += Input.GetAxis ("Horizontal");



		// Set camera position                
//		transform.position = m_targetPosition;

		// Let the camera look at the player                    
//		SmoothLookAt (target.position, smoothLookSpeed);
//		transform.LookAt (target.transform);

	}




	void SmoothLookAt (Vector3 target, float smooth)
	{
		Vector3 dir = target - transform.position + transform.up;
		Quaternion targetRotation = Quaternion.LookRotation (dir);
		targetRotation.x = 0;
		targetRotation.y = 0;
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * smooth);
	}
}