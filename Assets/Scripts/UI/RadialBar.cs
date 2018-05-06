using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour
{

	Image fillImage;

	public CampReflectFloat input;
	public float maxFill = 1;
	public float minFill = 0;






	// Use this for initialization
	void Start ()
	{
		fillImage = GetComponent<Image> ();
		FindFill (input.GetFloat ());

	}

	void Update ()
	{
		
		FindFill (input.GetFloat ());
	}


	void FindFill (float t)
	{
		float desiredFill = Mathf.Lerp (minFill, maxFill, t);
		fillImage.fillAmount = desiredFill;

	}
}
