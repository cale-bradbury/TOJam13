using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{



	Image fillImage;

	public CampReflectFloat input;

	//test value

	[Range (0, 1)]
	public float t = 0;
	public float numBars = 23;

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
		float desiredFill = (Mathf.Round (Mathf.Lerp (0, numBars, t)) / numBars);
		fillImage.fillAmount = desiredFill;

	}
}
