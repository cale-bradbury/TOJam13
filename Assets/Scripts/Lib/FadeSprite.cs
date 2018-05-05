using UnityEngine;
using System.Collections;

public class FadeSprite : MonoBehaviour
{

	SpriteRenderer spr;
	public float duration;
	public bool startInvis = true;
	public bool fadeOutAfter = true;

	public float delay = 9.0f;


	void Awake()
	{
		spr = GetComponent<SpriteRenderer>();
		if (startInvis)
		{
			Color col = spr.color;
			col.a = 0;
			spr.color = col;
		}
	}

	void OnEnable()
	{
		//add your event hook
//		TaskManager.OnNearTask += HandleOnTaskArriving;
	}

	void OnDisable()
	{
		//remove your event hook
//		TaskManager.OnNearTask -= HandleOnTaskArriving;
	}



	//	void HandleOnTaskArriving (Task task, bool isStart)
	//	{
	//		if (task != taskID) return;
	//
	//		if (isStart)
	//			FadeIn();
	//		else
	//			FadeOut();
	//	}
	//
	public void FadeIn()
	{
//		print("fading " + taskID + "  in");
		StartCoroutine(spr.FadeSprite(spr.color.a, 0.9f, duration, 0, () => { 
					if (fadeOutAfter)
						FadeOut(0.8f, delay);
				}));
	}

	public void FadeOut()
	{
//		print ("FADING OUT!!!!");
		StartCoroutine(spr.FadeSprite(spr.color.a, 0f, duration, 0, null));
	}

	public void FadeOut(float t, float delay)
	{
//		print ("FADING OUT!!!!");
		StartCoroutine(spr.FadeSprite(spr.color.a, 0, t, delay, null));
	}
}
