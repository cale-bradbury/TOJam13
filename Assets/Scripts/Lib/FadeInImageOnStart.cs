using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInImageOnStart : MonoBehaviour {

	Graphic img;
	public float duration;
	public float delay;
	public float targetAlpha;
	public EaseType easer;

	Color origColor;

	void Awake()
	{
		img = GetComponent<Graphic>();
		origColor = img.color;
	}

	void OnEnable()
	{
		StartCoroutine(Auto.Wait(delay, ()=>{
			StartCoroutine(img.FadeImageAlpha(targetAlpha, duration, easer, null));
		}));
	}

	void OnDisable()
	{
		img.color = origColor;
	}


}
