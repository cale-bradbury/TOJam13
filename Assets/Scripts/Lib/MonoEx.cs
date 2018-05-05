using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonoEx : MonoBehaviour
{

	Vector3 origPos;
	Quaternion origRot;
	Vector3 origScale;
	bool origIsKinematic;
	bool origUseGravity;
	protected float origDrag;
	[HideInInspector]
	public Rigidbody rb;

	protected virtual void Awake ()
	{
		Init ();
	}

	protected virtual void Init ()
	{
		if (GetComponent<Rigidbody> ()) {
			rb = GetComponent<Rigidbody> ();
			origIsKinematic = rb.isKinematic;
			origDrag = rb.drag;
		}
		origPos = transform.position;
		origRot = transform.rotation;
		origScale = transform.localScale;
	}

	public virtual void Reset ()
	{
		if (rb != null) {
			rb.isKinematic = true;
			rb.velocity = Vector3.zero;
			rb.isKinematic = origIsKinematic;
		}
		transform.position = origPos;
		transform.rotation = origRot;
		transform.localScale = origScale;
	}

	public virtual void Reset (System.Action doFirst)
	{
		if (doFirst != null) {
			doFirst ();
		}
		Reset ();
	}

	public virtual void Enable ()
	{
		gameObject.SetActive (true);
		enabled = true;
		if (rb != null) {
			rb.isKinematic = false;
		}

	}

	public virtual void Disable ()
	{
		gameObject.SetActive (false);
		Reset ();
	}

	public Vector3 GetOrigScale ()
	{
		return origScale;
	}

	public Vector3 GetOrigPos ()
	{
		return origPos;
	}

	public Quaternion GetOrigRot ()
	{
		return origRot;
	}


}
