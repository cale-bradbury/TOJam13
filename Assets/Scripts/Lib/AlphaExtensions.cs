using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;



public static class AlphaExtensions
{
	public  static IEnumerator Fade (
		this Material mat,
		float start,
		float target,
		float duration,
		System.Action onComplete)
	{
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			mat.SetFloat ("_Cutoff", temp);	
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	public static IEnumerator ChangeImageFill(this Image img, float target, float duration, EaseType easer, System.Action onComplete)
	{
		float elapsed = 0;
		float temp = img.fillAmount;
		var start = img.fillAmount;
		var range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			temp =  start + range * Ease.FromType(easer)(elapsed / duration);
			img.fillAmount = temp;
			yield return null;
		}
		img.fillAmount = target;

		if (onComplete != null)
		{
			onComplete();
		}
//		img.fillAmount
	}



	public  static IEnumerator ChangeMinX (
		this Material mat,
		float start,
		float target,
		float duration,
		System.Action onComplete)
	{
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			mat.SetFloat ("_MinX", temp);	
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	public  static IEnumerator ChangeFill (
		
		this Material mat,
		float start,
		float target,
		float duration,
		System.Action onComplete)
	{
		//	D.log ("Change Fill");
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			mat.SetFloat ("_Fill", temp);	
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	public  static IEnumerator SpeedUp (
		this Animator mat,
		float target,
		float duration,
		System.Action onComplete)
	{
		float start = mat.speed;
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			mat.speed = temp;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	
	
	public  static IEnumerator FadeTextAlpha (
		this Text mat,
		float start,
		float target,
		float duration,
		System.Action onComplete)
	{
		Color col = mat.color;
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			col.a = temp;
			mat.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	public  static IEnumerator LerpImageColor (
		this Graphic mat,
		Color start,
		Color target,
		float duration,
		System.Action onComplete)
	{
		Color col = mat.color;
		float elapsed = 0;
		Color temp = start;
		Color range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			col = temp;
			mat.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}

	public  static IEnumerator LerpImageColor (
		this Graphic mat,
		Color target,
		float duration,
		EaseType easer,
		System.Action onComplete)
	{
		Color col = mat.color;
		float elapsed = 0;
		Color start = mat.color;
		Color temp = start;
		Color range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * Ease.FromType(easer)(elapsed / duration);
			mat.color = temp;
			yield return 0;
		}
		mat.color = target;
		
		if (onComplete != null)
			onComplete ();
		
	}


	
	
	public  static IEnumerator LerpImageColor (
		this Material mat,
		Color target,
		float duration,
		EaseType easer,
		System.Action onComplete)
	{
		Color start = mat.color;
		Color col = mat.color;
		float elapsed = 0;
		Color temp = start;
		Color range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * Ease.FromType(easer)(elapsed / duration);
			col = temp;
			mat.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}

	
	
	public static IEnumerator Fade (
		this CanvasGroup spr,
		float target,
		float duration,
		EaseType ease,
		Action onComplete)
	{
		float elapsed = 0;
		var start = spr.alpha;
		var range = target - start;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			spr.alpha = start + range * Ease.FromType(ease) (elapsed / duration);
			yield return 0;
		}
		spr.alpha = target;
		if (onComplete != null)
			onComplete ();
	}


	
	
	public  static IEnumerator FadeImageAlpha (
		this Graphic mat,
		float start,
		float target,
		float duration,
		System.Action onComplete)
	{
		Color col = mat.color;
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			col.a = temp;
			mat.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}


	
	public  static IEnumerator FadeImageAlpha (
		this Graphic mat,
		float target,
		float duration,
		EaseType easer,
		System.Action onComplete)
	{
		Color col = mat.color;
		float elapsed = 0;
		float start = mat.color.a;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * Ease.FromType(easer)(elapsed / duration);
			col.a = temp;
			mat.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}

	public  static IEnumerator FadeAlpha (
		this CanvasGroup mat,
		float target,
		float duration,
		EaseType ease,
		System.Action onComplete)
	{
		float start = mat.alpha;
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * Ease.FromType(ease)  (elapsed / duration);
			mat.alpha = temp;
			yield return 0;
		}
		mat.alpha = target;
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	public  static IEnumerator FadeAlpha (
		this Material mat,
		float start,
		float target,
		float duration,
		System.Action onComplete)
	{
		Color col = mat.color;
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			col.a = temp;
			mat.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
	
	public  static IEnumerator FadeColor (
		this Material mat,
		float target,
		float duration,
		System.Action onComplete)
	{
		float elapsed = 0;
		float start = mat.color.a;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			mat.SetFloat ("_GrayScale", temp);	
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	

	public static IEnumerator FillUISprite (
		this Image spr,
		float target,
		float duration,
		EaseType ease,
		Action onComplete)
	{
		float elapsed = 0;
		var start = spr.fillAmount;
		var range = target - start;
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			spr.fillAmount = start + range * Ease.FromType(ease) (elapsed / duration);
			yield return 0;
		}
		spr.fillAmount = target;
		if (onComplete != null)
			onComplete ();
	}
	
	
	public  static IEnumerator FadeSprite (
		this SpriteRenderer spr,
		float start,
		float target,
		float duration,
		float delay,
		System.Action onComplete)
	{
		Color col = spr.color;
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		yield return new WaitForSeconds (delay);
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			col.a = temp;
			spr.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();


		//onCOMPLETE BREAKS BECAUSE FUCK UNITY AND EVERYTHING IS STUPID!!!
	}

	public  static IEnumerator FadeSprite (
		this SpriteRenderer spr,
		float target,
		float duration,
		float delay,
		System.Action onComplete)
	{
		Color col = spr.color;
		float elapsed = 0;
		float temp = spr.color.a;
		float start = spr.color.a;
		float range = target - temp;
		yield return new WaitForSeconds (delay);
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			col.a = temp;
			spr.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
		
		//onCOMPLETE BREAKS BECAUSE FUCK UNITY AND EVERYTHING IS STUPID!!!
	}

	
	public  static IEnumerator FadeSpriteColor (
		this SpriteRenderer spr,
		Color target,
		float duration,
		float delay,
		System.Action onComplete)	
	{
		Color col = spr.color;
		float elapsed = 0;
		Color temp = spr.color;
		float rangeR = target.r - temp.r;
		float rangeG = target.g - temp.g;
		float rangeB = target.b - temp.b;
		float rangeA = target.a - temp.a;
		yield return new WaitForSeconds (delay);
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp.r = spr.color.r + rangeR * (elapsed / duration);
			temp.g = spr.color.g + rangeG * (elapsed / duration);
			temp.b = spr.color.b + rangeB * (elapsed / duration);
			temp.a = spr.color.a + rangeA * (elapsed / duration);
			col.r = temp.r;
			col.g = temp.g;
			col.b = temp.b;
			col.a = temp.a;
			spr.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}

	
	
	public  static IEnumerator FadeSpriteColor (
		this SpriteRenderer spr,
		Color start,
		Color target,
		float duration,
		float delay,
		System.Action onComplete)	
	{
		Color col = spr.color;
		float elapsed = 0;
		Color temp = start;
		float rangeR = target.r - temp.r;
		float rangeG = target.g - temp.g;
		float rangeB = target.b - temp.b;
		float rangeA = target.a - temp.a;
		yield return new WaitForSeconds (delay);
		while (elapsed < duration) {
			elapsed = Mathf.MoveTowards (elapsed, duration, Time.deltaTime);
			temp.r = start.r + rangeR * (elapsed / duration);
			temp.g = start.g + rangeG * (elapsed / duration);
			temp.b = start.b + rangeB * (elapsed / duration);
			temp.a = start.a + rangeA * (elapsed / duration);
			col.r = temp.r;
			col.g = temp.g;
			col.b = temp.b;
			col.a = temp.a;
			spr.color = col;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete ();
		
	}
	
	
}