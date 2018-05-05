using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public delegate bool Predicate();
public delegate float Easer(float t);
public static class Auto
{
	
	
	public static Vector3 GetPosByAngle(this Vector3 pos, float xPos, float angleInDegrees)
	{
		//need to find b
		float b = pos.y - (Mathf.Tan(angleInDegrees) * pos.x);
		float y = (Mathf.Tan(angleInDegrees) * xPos) + b;//y=mx+b
		return new Vector3(xPos, y, pos.z);
	}

	public static Vector3 SetX(this Transform trans, float ammount)
	{
		return new Vector3(ammount, trans.position.y, trans.position.z);
	}

	public static Vector3 MoveX(this Transform trans, float ammount)
	{
		return new Vector3(trans.position.x + ammount, trans.position.y, trans.position.z);
	}

	#region Transform coroutines

	public static void AddX(this Vector3 vec3, float amt)
	{
		Vector3 p = vec3;
		p.x += amt;
		vec3 = p;
	}

	
	public static IEnumerator MoveRectTo(this RectTransform trans, Vector2 target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		Vector2 start = trans.anchoredPosition;
		while (elapsed < duration)
		{
			var range = target - start;
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			trans.anchoredPosition = (start + range * ease(elapsed / duration));
			yield return 0;
		}
		trans.anchoredPosition = (target);
		if (onComplete != null)
			onComplete();
	}

	
	public static IEnumerator MoveRectTo(this RectTransform trans, Vector2 target, float duration, Action onComplete)
	{
		return MoveRectTo(trans, target, duration, Ease.Linear, onComplete);
	}

	
	public static IEnumerator MoveRectTo(this RectTransform trans, Vector2 target, float duration, EaseType ease, Action onComplete)
	{
		return MoveRectTo(trans, target, duration, Ease.FromType(ease), onComplete);
	}

	
	
	
	public static IEnumerator MoveRectToTransform(this RectTransform trans, RectTransform target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = trans.anchoredPosition;
		while (elapsed < duration)
		{
			var range = target.anchoredPosition - start;
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			trans.anchoredPosition = (start + range * ease(elapsed / duration));
			yield return 0;
		}
		trans.anchoredPosition = (target.anchoredPosition);
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator MoveRectToTransform(this RectTransform trans, RectTransform target, float duration, Action onComplete)
	{
		return MoveRectTo(trans, target.anchoredPosition, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator MoveRectToTransform(this RectTransform trans, RectTransform target, float duration, EaseType ease, Action onComplete)
	{
		return MoveRectToTransform(trans, target, duration, Ease.FromType(ease), onComplete);
	}

	
	
	
	public static IEnumerator MoveToTransformOffset(this Transform transform, Transform target, Vector3 offset, float duration, Easer ease, Action onComplete)
	{
		
		float elapsed = 0;
		var start = transform.localPosition;
		//				D.log ("OFFSET IS: " + offset);
		while (elapsed < duration)
		{
			//Vector3 tar = target.position + offset;
			//						D.log ("TargetPos is: " + target.position);
			//						D.log ("combined is: " + tar);
			var range = offset - start;
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localPosition = (start + (range) * ease(elapsed / duration));
			yield return 0;
		}
		transform.localPosition = offset;
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator MoveToTransformOffset(this Transform transform, Transform target, Vector3 offset, float duration, EaseType ease, Action onComplete)
	{
		return MoveToTransformOffset(transform, target, offset, duration, Ease.FromType(ease), onComplete);
	}

	
	
	
	public static IEnumerator MoveToTransform(this Transform transform, Transform target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.position;
		while (elapsed < duration)
		{
			var range = target.position - start;
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.position = (start + range * ease(elapsed / duration));
			yield return 0;
		}
		transform.position = (target.position);
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator MoveToTransform(this Transform transform, Transform target, float duration, Action onComplete)
	{
		return MoveTo(transform, target.position, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator MoveToTransform(this Transform transform, Transform target, float duration, EaseType ease, Action onComplete)
	{
		return MoveToTransform(transform, target, duration, Ease.FromType(ease), onComplete);
	}


	public static IEnumerator MoveToLocal(this Transform transform, Vector3 target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.localPosition;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.position = (start + range * ease(elapsed / duration));
			yield return 0;
		}
		transform.localPosition = (target);
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator MoveToLocal(this Transform transform, Vector3 target, float duration, Action onComplete)
	{
		return MoveToLocal(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator MoveToLocal(this Transform transform, Vector3 target, float duration, EaseType ease, Action onComplete)
	{
		return MoveToLocal(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	

	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.position;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.position = (start + range * ease(elapsed / duration));
			yield return 0;
		}
		transform.position = (target);
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration, Action onComplete)
	{
		return MoveTo(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator MoveTo(this Transform transform, Vector3 target, float duration, EaseType ease, Action onComplete)
	{
		return MoveTo(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration, Easer ease, Action onComplete)
	{
		var start = transform.localPosition;
		transform.localPosition = target;
		return MoveTo(transform, start, duration, ease, onComplete);
	}

	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration, Action onComplete)
	{
		return MoveFrom(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator MoveFrom(this Transform transform, Vector3 target, float duration, EaseType ease, Action onComplete)
	{
		return MoveFrom(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	//
	//	public static IEnumerator ScaleRectTo(this RectTransform transform, Vector2 target, float duration, Easer ease, Action onComplete)
	//	{
	//		float elapsed = 0;
	//		var start = transform.sizeDelta;
	//		var range = target - start;
	//		while (elapsed < duration)
	//		{
	//			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
	//			transform.sizeDelta = start + range * ease(elapsed / duration);
	//			yield return 0;
	//		}
	//		transform.sizeDelta = target;
	//		if (onComplete != null)
	//			onComplete();
	//	}
	//


	public static IEnumerator ScaleRectTo(this RectTransform trans, Vector2 target, float duration, Easer ease, System.Action onComplete)
	{
		float elapsed = 0;
		Vector2 start = trans.rect.size;
		while (elapsed < duration)
		{
			var range = target - start;
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (start.x + range.x * ease(elapsed / duration)));
			trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (start.y + range.y * ease(elapsed / duration)));
			yield return 0;
		}
		trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.x);
		trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.y);
		if (onComplete != null)
			onComplete();
	}


	public static IEnumerator ScaleRectTo(this RectTransform trans, Vector2 target, float duration, System.Action onComplete)
	{
		return ScaleRectTo(trans, target, duration, Ease.Linear, onComplete);
	}


	public static IEnumerator ScaleRectTo(this RectTransform trans, Vector2 target, float duration, EaseType ease, System.Action onComplete)
	{
		return ScaleRectTo(trans, target, duration, Ease.FromType(ease), onComplete);
	}




	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.localScale;
		var range = target - start;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localScale = start + range * ease(elapsed / duration);
			yield return 0;
		}
		transform.localScale = target;
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration, Action onComplete)
	{
		return ScaleTo(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator ScaleTo(this Transform transform, Vector3 target, float duration, EaseType ease, Action onComplete)
	{
		return ScaleTo(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration, Easer ease, Action onComplete)
	{
		var start = transform.localScale;
		transform.localScale = target;
		return ScaleTo(transform, start, duration, ease, onComplete);
	}

	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration, Action onComplete)
	{
		return ScaleFrom(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator ScaleFrom(this Transform transform, Vector3 target, float duration, EaseType ease, Action onComplete)
	{
		return ScaleFrom(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.rotation;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.rotation = Quaternion.Lerp(start, target, ease(elapsed / duration));
			yield return 0;
		}
		transform.rotation = target;
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration, Action onComplete)
	{
		return RotateTo(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator RotateTo(this Transform transform, Quaternion target, float duration, EaseType ease, Action onComplete)
	{
		return RotateTo(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator RotateToLocal(this Transform transform, Quaternion target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.localRotation;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			transform.localRotation = Quaternion.Lerp(start, target, ease(elapsed / duration));
			yield return 0;
		}
		transform.localRotation = target;
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator RotateToLocal(this Transform transform, Quaternion target, float duration, Action onComplete)
	{
		return RotateToLocal(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator RotateToLocal(this Transform transform, Quaternion target, float duration, EaseType ease, Action onComplete)
	{
		return RotateToLocal(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration, Easer ease, Action onComplete)
	{
		var start = transform.localRotation;
		transform.localRotation = target;
		return RotateTo(transform, start, duration, ease, onComplete);
	}

	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration, Action onComplete)
	{
		return RotateFrom(transform, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator RotateFrom(this Transform transform, Quaternion target, float duration, EaseType ease, Action onComplete)
	{
		return RotateFrom(transform, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration, Easer ease, Action onComplete)
	{
		float elapsed = 0;
		var start = transform.localPosition;
		Vector3 position;
		float t;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			t = ease(elapsed / duration);
			position.x = start.x * (1 - t) * (1 - t) + control.x * 2 * (1 - t) * t + target.x * t * t;
			position.y = start.y * (1 - t) * (1 - t) + control.y * 2 * (1 - t) * t + target.y * t * t;
			position.z = start.z * (1 - t) * (1 - t) + control.z * 2 * (1 - t) * t + target.z * t * t;
			transform.localPosition = position;
			yield return 0;
		}
		transform.localPosition = target;
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration, Action onComplete)
	{
		return CurveTo(transform, control, target, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator CurveTo(this Transform transform, Vector3 control, Vector3 target, float duration, EaseType ease, Action onComplete)
	{
		return CurveTo(transform, control, target, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator CurveFrom(this Transform transform, Vector3 control, Vector3 start, float duration, Easer ease, Action onComplete)
	{
		var target = transform.localPosition;
		transform.localPosition = start;
		return CurveTo(transform, control, target, duration, ease, onComplete);
	}

	public static IEnumerator CurveFrom(this Transform transform, Vector3 control, Vector3 start, float duration, Action onComplete)
	{
		return CurveFrom(transform, control, start, duration, Ease.Linear, onComplete);
	}

	public static IEnumerator CurveFrom(this Transform transform, Vector3 control, Vector3 start, float duration, EaseType ease, Action onComplete)
	{
		return CurveFrom(transform, control, start, duration, Ease.FromType(ease), onComplete);
	}

	public static IEnumerator Shake(this Transform transform, Vector3 amount, float duration, Action onComplete)
	{
		var start = transform.localPosition;
		var shake = Vector3.zero;
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			shake.Set(UnityEngine.Random.Range(-amount.x, amount.x), UnityEngine.Random.Range(-amount.y, amount.y), UnityEngine.Random.Range(-amount.z, amount.z));
			transform.localPosition = start + shake;
			yield return 0;
		}
		transform.localPosition = start;
		if (onComplete != null)
			onComplete();
	}

	public static IEnumerator Shake(this Transform transform, float amount, float duration, Action onComplete)
	{
		return Shake(transform, new Vector3(amount, amount, amount), duration, onComplete);
	}

	#endregion

	#region Waiting coroutines


	
	public static IEnumerator Wait(float duration)
	{
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			yield return 0;
		}
	}

	public static IEnumerator Wait(float duration, System.Action OnComplete)
	{
		while (duration > 0)
		{
			duration -= Time.deltaTime;
			yield return 0;
		}
		if (OnComplete != null)
			OnComplete();
	}

	public static IEnumerator WaitUntil(Predicate predicate)
	{
		while (!predicate())
			yield return 0;
	}

	#endregion

	#region Time-based motion

	public static float Loop(float duration, float from, float to, float offsetPercent)
	{
		var range = to - from;
		var total = (Time.time + duration * offsetPercent) * (Mathf.Abs(range) / duration);
		if (range > 0)
			return from + Time.time - (range * Mathf.FloorToInt((Time.time / range)));
		else
			return from - (Time.time - (Mathf.Abs(range) * Mathf.FloorToInt((total / Mathf.Abs(range)))));
	}

	public static float Loop(float duration, float from, float to)
	{
		return Loop(duration, from, to, 0);	
	}

	public static Vector3 Loop(float duration, Vector3 from, Vector3 to, float offsetPercent)
	{
		return Vector3.Lerp(from, to, Loop(duration, 0, 1, offsetPercent));
	}

	public static Vector3 Loop(float duration, Vector3 from, Vector3 to)
	{
		return Vector3.Lerp(from, to, Loop(duration, 0, 1));
	}

	public static Quaternion Loop(float duration, Quaternion from, Quaternion to, float offsetPercent)
	{
		return Quaternion.Lerp(from, to, Loop(duration, 0, 1, offsetPercent));
	}

	public static Quaternion Loop(float duration, Quaternion from, Quaternion to)
	{
		return Quaternion.Lerp(from, to, Loop(duration, 0, 1));
	}

	public static float Wave(float duration, float from, float to, float offsetPercent)
	{
		var range = (to - from) / 2;
		return from + range + Mathf.Sin(((Time.time + duration * offsetPercent) / duration) * (Mathf.PI * 2)) * range;
	}

	public static float Wave(float duration, float from, float to)
	{
		return Wave(duration, from, to, 0);
	}

	

	public static Vector3 Wave(float duration, Vector3 from, Vector3 to, float offsetPercent)
	{
		return Vector3.Lerp(from, to, Wave(duration, 0, 1, offsetPercent));
	}

	public static Vector3 Wave(float duration, Vector3 from, Vector3 to)
	{
		return Vector3.Lerp(from, to, Wave(duration, 0, 1));
	}

	public static Quaternion Wave(float duration, Quaternion from, Quaternion to, float offsetPercent)
	{
		return Quaternion.Lerp(from, to, Wave(duration, 0, 1, offsetPercent));
	}

	public static Quaternion Wave(float duration, Quaternion from, Quaternion to)
	{
		return Quaternion.Lerp(from, to, Wave(duration, 0, 1));
	}

	
	/// <summary>
	/// pass in a string, hope to god you don't mistype it (super foolproof)
	/// animate that string's float value with math to make it look purdy
	/// </summary>
	/// <returns>Coroutine to ease float values in audio mixer because fuck inspectors</returns>
	/// <param name="audio">AudioMixer.</param>
	/// <param name="param">Dumb String That Is the Bane of my existence.</param>
	/// <param name="target">Target Float.</param>
	/// <param name="duration">Duration float.</param>
	/// <param name="ease">Ease equation</param>
	public  static IEnumerator ChangeAudioParam(this AudioMixer audio, string param, float start, float target, float duration, EaseType ease, Action onComplete)
	{
		
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			temp = (start + range * Ease.FromType(ease)(elapsed / duration));
			audio.SetFloat(param, temp);
			yield return 0;
		}
		audio.SetFloat(param, target);
		if (onComplete != null)
			onComplete();
		
	}



	public static IEnumerator LerpImageAlpha(this Graphic img, float target, float duration, EaseType ease, System.Action onComplete)
	{
		float elapsed = 0;
		float start = img.color.a;
		while (elapsed < duration)
		{
			var range = target - start;
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			Color col = img.color;
			col.a = (start + range * Ease.FromType(ease)(elapsed / duration));
			img.color = col;
			yield return 0;
		}
		Color newCol = img.color;
		newCol.a = target;
		img.color = newCol;
		if (onComplete != null)
			onComplete();
	}

	
	
	
	public  static IEnumerator ChangePitch(this AudioSource audio, float start, float target, float duration, EaseType ease, Action onComplete)
	{
		
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			temp = (start + range * Ease.FromType(ease)(elapsed / duration));
			audio.pitch = temp;
			yield return 0;
		}
		audio.pitch = target;
		if (onComplete != null)
			onComplete();
		
	}

	public  static IEnumerator ChangeVolume(this AudioSource audio, float start, float target, float duration, System.Action onComplete)
	{
		float elapsed = 0;
		float temp = start;
		float range = target - temp;
		while (elapsed < duration)
		{
			elapsed = Mathf.MoveTowards(elapsed, duration, Time.deltaTime);
			temp = start + range * (elapsed / duration);
			audio.volume = temp;
			yield return 0;
		}
		
		if (onComplete != null)
			onComplete();
		
	}

	#endregion

	#region misc

	public static List<Vector3> GetVertices(this LineRenderer line)
	{
		return new List<Vector3>();	
	}

	public static void SetVertices(this LineRenderer line, int index, Vector3 pos)
	{
		
	}

	/// <summary>
	/// Pop the last item from the stack.
	/// </summary>
	/// <param name="theList">The list.</param>
	public static T Pop<T>(this List<T> theList)
	{
		var local = theList[theList.Count - 1];
		theList.RemoveAt(theList.Count - 1);
		return local;
	}

	public static void Push<T>(this List<T> theList, T item)
	{
		theList.Add(item);
	}

	#endregion
	
}

#region Easing functions
public enum EaseType
{
	Linear,
	QuadIn,
	QuadOut,
	QuadInOut,
	CubeIn,
	CubeOut,
	CubeInOut,
	BackIn,
	BackOut,
	BackInOut,
	ExpoIn,
	ExpoOut,
	ExpoInOut,
	SineIn,
	SineOut,
	SineInOut,
	ElasticIn,
	ElasticOut,
	ElasticInOut,
	SmoothStepInOut
}

public static class Ease
{
	public static readonly Easer Linear = (t) => {
		return t;
	};
	public static readonly Easer QuadIn = (t) => {
		return t * t;
	};
	public static readonly Easer QuadOut = (t) => {
		return 1 - QuadIn(1 - t);
	};
	public static readonly Easer QuadInOut = (t) => {
		return (t <= 0.5f) ? QuadIn(t * 2) / 2 : QuadOut(t * 2 - 1) / 2 + 0.5f;
	};
	public static readonly Easer CubeIn = (t) => {
		return t * t * t;
	};
	//t * t * t * (t * (6f * t - 15f) + 10f);
	public static readonly Easer CubeOut = (t) => {
		return 1 - CubeIn(1 - t);
	};
	public static readonly Easer CubeInOut = (t) => {
		return (t <= 0.5f) ? CubeIn(t * 2) / 2 : CubeOut(t * 2 - 1) / 2 + 0.5f;
	};
	public static readonly Easer BackIn = (t) => {
		return t * t * (2.70158f * t - 1.70158f);
	};
	public static readonly Easer BackOut = (t) => {
		return 1 - BackIn(1 - t);
	};
	public static readonly Easer BackInOut = (t) => {
		return (t <= 0.5f) ? BackIn(t * 2) / 2 : BackOut(t * 2 - 1) / 2 + 0.5f;
	};
	public static readonly Easer ExpoIn = (t) => {
		return (float)Mathf.Pow(2, 10 * (t - 1));
	};
	public static readonly Easer ExpoOut = (t) => {
		return 1 - ExpoIn(t);
	};
	public static readonly Easer ExpoInOut = (t) => {
		return t < .5f ? ExpoIn(t * 2) / 2 : ExpoOut(t * 2) / 2;
	};
	public static readonly Easer SineIn = (t) => {
		return -Mathf.Cos(Mathf.PI / 2 * t) + 1;
	};
	public static readonly Easer SineOut = (t) => {
		return Mathf.Sin(Mathf.PI / 2 * t);
	};
	public static readonly Easer SineInOut = (t) => {
		return -Mathf.Cos(Mathf.PI * t) / 2f + .5f;
	};
	public static readonly Easer ElasticIn = (t) => {
		return 1 - ElasticOut(1 - t);
	};
	public static readonly Easer ElasticOut = (t) => {
		return Mathf.Pow(2, -10 * t) * Mathf.Sin((t - 0.075f) * (2 * Mathf.PI) / 0.3f) + 1;
	};
	public static readonly Easer ElasticInOut = (t) => {
		return (t <= 0.5f) ? ElasticIn(t * 2) / 2 : ElasticOut(t * 2 - 1) / 2 + 0.5f;
	};
	
	public static readonly Easer SmoothStepInOut = (t) => {
		return  t * t * t * (t * (6f * t - 15f) + 10f);
	};

	public static Easer FromType(EaseType type)
	{
		switch (type)
		{
		case EaseType.Linear:
			return Linear;
		case EaseType.QuadIn:
			return QuadIn;
		case EaseType.QuadOut:
			return QuadOut;
		case EaseType.QuadInOut:
			return QuadInOut;
		case EaseType.CubeIn:
			return CubeIn;
		case EaseType.CubeOut:
			return CubeOut;
		case EaseType.CubeInOut:
			return CubeInOut;
		case EaseType.BackIn:
			return BackIn;
		case EaseType.BackOut:
			return BackOut;
		case EaseType.BackInOut:
			return BackInOut;
		case EaseType.ExpoIn:
			return ExpoIn;
		case EaseType.ExpoOut:
			return ExpoOut;
		case EaseType.ExpoInOut:
			return ExpoInOut;
		case EaseType.SineIn:
			return SineIn;
		case EaseType.SineOut:
			return SineOut;
		case EaseType.SineInOut:
			return SineInOut;
		case EaseType.ElasticIn:
			return ElasticIn;
		case EaseType.ElasticOut:
			return ElasticOut;
		case EaseType.ElasticInOut:
			return ElasticInOut;
		case EaseType.SmoothStepInOut:
			return SmoothStepInOut;
		}
		return Linear;
	}
}
#endregion
