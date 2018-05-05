using UnityEngine;
using System.Collections;

public class SwitchPageOnSwipe : MonoBehaviour {

	public GameObject prevPage, nextPage;
	public GameObject[] additionalDisables;

	bool isEnabled = true;

	void OnEnable()
	{
		Init ();
	}

	void OnDisable()
	{
		Disable ();	
	}


	public void Disable()
	{
		if (isEnabled)
		{
			GameManager.OnSwipeLeft -= HandleOnSwipeLeft;
			GameManager.OnSwipeRight -= HandleOnSwipeRight;
			isEnabled = false;
		}
	}

	public void Init()
	{
		if (!isEnabled)
		{
			GameManager.OnSwipeLeft += HandleOnSwipeLeft;
			GameManager.OnSwipeRight += HandleOnSwipeRight;
			isEnabled = true;
		}
	}


	void HandleOnSwipeRight ()
	{
		if (prevPage != null)
		{
			gameObject.SetActive(false);
			prevPage.SetActive(true);
		}
		if (additionalDisables.Length > 0)
		{
			foreach (GameObject g in additionalDisables)
			{
				g.SetActive(false);
			}
		}
	}

	void HandleOnSwipeLeft ()
	{
		if (nextPage != null)
		{
			gameObject.SetActive(false);
			nextPage.SetActive(true);
		}
		if (additionalDisables.Length > 0)
		{
			foreach (GameObject g in additionalDisables)
			{
				g.SetActive(false);
			}
		}
	}

}
