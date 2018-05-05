using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public Transform targetRotation;

	public float smoothSpeed = 0.125f;
	public float smoothLookSpeed = 0.125f;
	public Vector3 offset;

	void LateUpdate ()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;

		Quaternion targetRot = targetRotation.localRotation;
		transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRot, smoothLookSpeed * Time.deltaTime);

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