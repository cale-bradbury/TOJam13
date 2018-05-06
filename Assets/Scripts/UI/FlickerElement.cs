using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlickerElement : MonoBehaviour
{

	CanvasGroup cGroup;

	public float minAlpha = 0.1f;
	public float maxAlpha = 1.0f;
	public float speed = 10;
	float lerper = 0;

	void Awake ()
	{
		cGroup = GetComponent<CanvasGroup> ();
	}

	void Update ()
	{
		lerper = (1 + (Mathf.Sin (Time.time * speed))) / 2;
		cGroup.alpha = Mathf.Lerp (minAlpha, maxAlpha, lerper);
	}


}
