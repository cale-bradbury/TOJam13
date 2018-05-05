using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PopUp : MonoBehaviour
{

	public RectTransform target;
	public float duration;
	public bool shouldMove = true;

	List<float> origAlphas;
	List<Graphic> imgs;
	Text[] labels;
	Vector2 origScale;
	RectTransform rect;
	Vector2 origPos;

	bool isInit = false;


	void Awake()
	{
		if (!isInit)
			Init();
		

	}


	public void AddImage(Graphic img)
	{
		imgs.Add(img);
		origAlphas.Add(img.color.a);
	}

	public void RemoveImage(Graphic img)
	{
		int index = imgs.IndexOf(img);
		if (index >= 0)
		{
			imgs.RemoveAt(index);
			origAlphas.RemoveAt(index);
		}
	}

	public List<Graphic> GetImageList()
	{
		return imgs;
	}

	public List<float> GetAlphaList()
	{
		return origAlphas;
	}

	void OnEnable()
	{
		

	}

	void Init()
	{
//		gameObject.SetActive (true);
		rect = GetComponent<RectTransform>();
		imgs = new List<Graphic>(GetComponentsInChildren<Graphic>());
		labels = GetComponentsInChildren<Text>();

		//store original values
		origScale = new Vector2(rect.rect.width, rect.rect.height);
		origPos = rect.anchoredPosition;


		origAlphas = new List<float>();
		for (int i = 0; i < imgs.Count; i++)
		{
			origAlphas.Add(imgs[i].color.a);
		}

		for (int i = 0; i < imgs.Count; i++)
		{
			Color col = imgs[i].color;
			col.a = 0;
			imgs[i].color = col;
		}

		if (shouldMove)
		{
			
			for (int i = 0; i < labels.Length; i++)
			{
				labels[i].rectTransform.localScale = Vector3.zero;
			}
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height);

			//		rect.sizeDelta = target.sizeDelta;
			rect.anchoredPosition = target.anchoredPosition;

		}

		isInit = true;
		if (gameObject.activeInHierarchy)
		{
//			StartCoroutine(Auto.Wait(0.1f, () => {
				
			gameObject.SetActive(false);
//					}));
		}

	}

	public void ChangeTarget(RectTransform newTarget)
	{
		if (!shouldMove)
			return;
		if (target != newTarget)
		{
			target = newTarget;
			if (rect != null)
			{
				
				rect.anchoredPosition = target.anchoredPosition;
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width);
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height);
			}
		}
	}

	public void Hide()
	{
		if (gameObject.activeInHierarchy)
		{
			if (shouldMove)
			{
				
				StartCoroutine(rect.ScaleRectTo(new Vector2(target.rect.width, target.rect.height), duration / 2, EaseType.SmoothStepInOut, null));
				StartCoroutine(rect.MoveRectTo(target.anchoredPosition, duration / 2, EaseType.SmoothStepInOut, null));
				for (int i = 0; i < labels.Length; i++)
				{
					StartCoroutine(labels[i].rectTransform.ScaleTo(Vector3.zero, duration / 2, EaseType.SmoothStepInOut, null));
				}
			
			}

			for (int i = 0; i < imgs.Count; i++)
			{
				StartCoroutine(imgs[i].LerpImageAlpha(0, duration / 2, EaseType.SmoothStepInOut, null));
			}

			StartCoroutine(Auto.Wait(duration + 0.1f, () => {
						gameObject.SetActive(false);
					}));
		}
	}



	public void Hide(System.Action onComplete)
	{
		if (gameObject.activeInHierarchy)
		{
			if (shouldMove)
			{
				StartCoroutine(rect.ScaleRectTo(new Vector2(target.rect.width, target.rect.height), duration / 2, EaseType.SmoothStepInOut, null));
				StartCoroutine(rect.MoveRectTo(target.anchoredPosition, duration / 2, EaseType.SmoothStepInOut, null));

				for (int i = 0; i < labels.Length; i++)
				{
					StartCoroutine(labels[i].rectTransform.ScaleTo(Vector3.zero, duration / 2, EaseType.SmoothStepInOut, null));
				}
			}

			for (int i = 0; i < imgs.Count; i++)
			{
				StartCoroutine(imgs[i].LerpImageAlpha(0, duration / 2, EaseType.SmoothStepInOut, null));
			}

			StartCoroutine(Auto.Wait(duration + 0.1f, () => {
						if (onComplete != null)
						{
							onComplete();
						}
						gameObject.SetActive(false);
					}));
		}
	}

	public void Show(System.Action onComplete)
	{
		gameObject.SetActive(true);
		if (!isInit)
		{
			Init();
		}

		if (shouldMove)
		{

			rect.anchoredPosition = target.anchoredPosition;
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height);
		}



		if (shouldMove)
		{
			StartCoroutine(rect.ScaleRectTo(origScale, duration, EaseType.SmoothStepInOut, null));
			StartCoroutine(rect.MoveRectTo(origPos, duration, EaseType.SmoothStepInOut, null));

			for (int i = 0; i < labels.Length; i++)
			{
				labels[i].rectTransform.localScale = Vector3.zero;
				StartCoroutine(labels[i].rectTransform.ScaleTo(Vector3.one, duration, EaseType.SmoothStepInOut, null));
			}
		}

		for (int i = 0; i < imgs.Count; i++)
		{
			Color col = imgs[i].color;
			col.a = 0;
			imgs[i].color = col;
			StartCoroutine(imgs[i].LerpImageAlpha(origAlphas[i], duration, EaseType.SmoothStepInOut, null));
		}


		if (GetComponentInChildren<Scrollbar>())
		{
			StartCoroutine(Auto.Wait(duration - 0.05f, () => {
						GetComponentInChildren<Scrollbar>().value = 1;

					}));
		}

		StartCoroutine(Auto.Wait(duration + 0.1f, () => {
					if (onComplete != null)
					{
						onComplete();
					}
					
				}));
	}

	public void Show()
	{

		gameObject.SetActive(true);
		if (!isInit)
		{
			Init();
		}

		if (shouldMove)
		{
			
			rect.anchoredPosition = target.anchoredPosition;
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height);
		}


		if (shouldMove)
		{
			StartCoroutine(rect.ScaleRectTo(origScale, duration, EaseType.SmoothStepInOut, null));
			StartCoroutine(rect.MoveRectTo(origPos, duration, EaseType.SmoothStepInOut, null));

			for (int i = 0; i < labels.Length; i++)
			{
				labels[i].rectTransform.localScale = Vector3.zero;
				StartCoroutine(labels[i].rectTransform.ScaleTo(Vector3.one, duration, EaseType.SmoothStepInOut, null));
			}
		}

		for (int i = 0; i < imgs.Count; i++)
		{
			Color col = imgs[i].color;
			col.a = 0;
			imgs[i].color = col;
			StartCoroutine(imgs[i].LerpImageAlpha(origAlphas[i], duration, EaseType.SmoothStepInOut, null));
		}


		if (GetComponentInChildren<Scrollbar>())
		{
			StartCoroutine(Auto.Wait(duration - 0.05f, () => {
						GetComponentInChildren<Scrollbar>().value = 1;
				
					}));
		}
	}

}
