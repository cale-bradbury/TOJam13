using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LerpImageBetweenColours : MonoBehaviour
{
	
	
	public EaseType easer;
	public float targetDuration;
	float flickerTime = 0.5f;
	public Color targetCol;
	public Color fadeCol;
	public float maxFlicker, minFlicker;
	public float maxTime, minTime;

	[HideInInspector] public float flickerAlpha = 0.6f;
	[HideInInspector] public Color currentCol;
	[HideInInspector] public float currentLerpTime = 0;
	[HideInInspector] public Color origColor;
	public bool shouldLerp;
	bool shouldFlicker = false;

	bool isRunning = false;

	Graphic spr;
	Color start;
	float t = 0;
	// Use this for initialization
	
	void Awake ()
	{
		start = new Color (0, 0, 0);
		spr = GetComponent<Graphic> ();
		currentCol = spr.color;
		origColor = spr.color;
		currentLerpTime = 0;
		start = currentCol;
		fadeCol = start;
		fadeCol.a = 0;
		spr.color = fadeCol;
		enabled = false;
		
	}

	void OnEnable()
	{
		isRunning = false;
		spr.color = fadeCol;
		StartCoroutine(spr.LerpImageColor(fadeCol, 1.5f, EaseType.Linear, ()=>{
			isRunning = true;
			shouldFlicker = true;
			shouldLerp = true;
		}));
	}

	void OnDisable()
	{
		spr.color = fadeCol;


		isRunning = false;
	}

	void Update ()
	{

		if (Input.GetKeyDown(KeyCode.N))
		{
			ChangeColor(origColor, 1, EaseType.SmoothStepInOut);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			ChangeColor(Color.green, 1, EaseType.SmoothStepInOut);
		}

		if (!isRunning) return;

		currentCol = spr.color;
		if (shouldLerp)
		{

			if (currentLerpTime < targetDuration) {
				currentLerpTime = Mathf.MoveTowards (currentLerpTime, targetDuration, Time.deltaTime);
				if (currentLerpTime > targetDuration) {
					currentLerpTime = targetDuration;
				}
				currentCol = Color.Lerp (start, targetCol, Ease.FromType (easer) (currentLerpTime / targetDuration));
			}
			else
			{
				shouldLerp = false;
			}
		}

		if (shouldFlicker)
		{
			currentCol.a = Auto.Wave(flickerTime, 0, flickerAlpha);
		}
		spr.color = currentCol;
	}

	public void Show()
	{
		StartCoroutine(Auto.Wait(1.5f, ()=>{
			ChangeColor(targetCol, 0.5f);
			shouldFlicker = true;
		}));
	}

	public void Hide()
	{
		shouldFlicker = false;
		spr.color = fadeCol;
		shouldLerp = false;
//		ChangeColor(fadeCol, 0.5f);
	}

	public void ChangeFlicker(float t)
	{
		if (t == 1)
		{
			//flickerAlpha = 0;
			shouldFlicker = false;
			ChangeColor(fadeCol, 0.5f);
		}
		else
		{
			shouldFlicker = true;
			flickerAlpha = Mathf.Lerp(minFlicker, maxFlicker, (1-t));
			//flickerTime = Mathf.Lerp(minTime, maxTime, (t));
		}
	}
	
	
	public void ChangeColor (Color target, float duration)
	{	
		shouldLerp = true;
		start = currentCol;
		targetCol = target;
		targetDuration = duration;
		easer = EaseType.Linear;
		currentLerpTime = 0;
	}
	
	
	public void ChangeColor (Color target, float duration, EaseType ease)
	{
		shouldLerp = true;
		start = currentCol;
		targetCol = target;
		targetDuration = duration;
		this.easer = ease;
		currentLerpTime = 0;
	}
	
}