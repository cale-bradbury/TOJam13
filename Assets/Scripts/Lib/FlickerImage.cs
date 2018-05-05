using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlickerImage : MonoBehaviour {

	public float duration;
	public float targetAlpha = 0.2f;
	
	
	Graphic r;
	float origAlpha;
	Color origColor;
	//public int numFlashes;     //0 == infinite
	//System.Action onComplete;
	bool fadeIn = true;
	
	void Awake()
	{
		r = GetComponent<Graphic>();
		origColor = r.color;
		origAlpha = r.color.a;
	}
	
	void Start()
	{
		FadeSprite();
	}

	void OnDisable()
	{
		StopAllCoroutines();
		Color col = r.color;
		col.a = origAlpha;
		r.color = col;
	}
	
	
	
	void FadeSprite()
	{
		StopAllCoroutines();
		if (fadeIn)
		{

			StartCoroutine(r.FadeImageAlpha(r.color.a, targetAlpha, duration, ()=>{
				fadeIn = !fadeIn;
				FadeSprite();
			}));
		}
		else if (!fadeIn)
		{
			StartCoroutine(r.FadeImageAlpha(r.color.a, origAlpha, duration, ()=>{
				fadeIn = !fadeIn;
				FadeSprite();
			}));
			
		}
		
	}
}
