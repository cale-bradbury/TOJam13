using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LerpImageColorAfterDelay : MonoBehaviour {

	public Color targetColor;
	public EaseType easer;
	public float duration;
	public float delay;

	Graphic img;
	Color origColor;

	void Awake()
	{
		img = GetComponent<Graphic>();
		origColor = img.color;
	}

	void OnEnable()
	{
		StartCoroutine(Auto.Wait(delay, ()=>{
			StartCoroutine(img.LerpImageColor(targetColor, duration, easer, null));
		}));
	}

	void OnDisable()
	{
		img.color = origColor;
	}

}
