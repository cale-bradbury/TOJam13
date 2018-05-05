using UnityEngine;
using System.Collections;

public class FlickerTransparentRenderer : MonoBehaviour {

	public float duration;


	SpriteRenderer r;
	Color origColor;
	Color targetColor;
	float targetAlpha;
	//public int numFlashes;     //0 == infinite
	//System.Action onComplete;
	bool fadeIn = true;
	bool shouldLerp = true;

	void Awake()
	{
		r = GetComponent<SpriteRenderer>();
		origColor = r.color;
		targetColor = r.color;
		targetAlpha = 0.2f;
	}

	void Start()
	{
//		FadeSprite();
		Color c = r.color;
		c.a = 0;
		r.color = c;
	}



	void FadeSprite()
	{
		if (shouldLerp)
		{
			
			StopAllCoroutines();
			float tar;
			if (fadeIn)
			{
				StartCoroutine(r.FadeSprite(targetAlpha, duration, 0, ()=>{
					fadeIn = !fadeIn;
					FadeSprite();
				}));
			}
			else if (!fadeIn)
			{
				StartCoroutine(r.FadeSprite(origColor.a, duration, 0, ()=>{
					fadeIn = !fadeIn;
					FadeSprite();
				}));
			
			}
		}

	}

	public void StartFade(float duration)
	{
		StartCoroutine (FadeSequence (duration));
	}

	IEnumerator FadeSequence(float duration)
	{
		
		yield return StartCoroutine (r.FadeSprite (targetAlpha, duration/12, 0, null));
		yield return new WaitForSeconds(duration/18);
		FadeSprite ();
		yield return StartCoroutine (Auto.Wait (duration));
		shouldLerp = false;
		r.color = origColor;


	}




}
