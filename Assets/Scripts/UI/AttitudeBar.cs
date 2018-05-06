using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttitudeBar : MonoBehaviour
{
	public Transform targetRotator;

	RectTransform rt;

	void Awake ()
	{
		rt = GetComponent<RectTransform> ();
	}

	void Update ()
	{
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z = targetRotator.localRotation.z * 100;
		rt.rotation = Quaternion.Euler (rot);
	}
}
