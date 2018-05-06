using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingIndicator : MonoBehaviour
{

	public CampReflectFloat input;
	public Vector3 minPos;
	public Vector3 maxPos;

	RectTransform rt;

	// Use this for initialization
	void Start ()
	{
		rt = GetComponent<RectTransform> ();
		FindPos (input.GetFloat ());
	}

	void Update ()
	{
		FindPos (input.GetFloat ());
	}

	void FindPos (float t)
	{
		Vector3 desiredPos = Vector3.Lerp (minPos, maxPos, t);
		rt.anchoredPosition = desiredPos;
	}
}
