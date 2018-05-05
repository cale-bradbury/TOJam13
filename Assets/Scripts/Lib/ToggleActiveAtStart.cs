using UnityEngine;
using System.Collections;

public class ToggleActiveAtStart : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		if (!gameObject.activeInHierarchy) {
			gameObject.SetActive (true);
			gameObject.SetActive (false);
		}
	}

}
