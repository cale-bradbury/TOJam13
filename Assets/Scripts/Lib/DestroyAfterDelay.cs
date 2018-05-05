using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DestroyAfterDelay : MonoBehaviour {


	public float delay = 5.0f;

	IEnumerator Start ()
	{
		yield return new WaitForEndOfFrame ();
		yield return new WaitForEndOfFrame ();
		GameObject.Destroy (gameObject, delay);
	}







}
