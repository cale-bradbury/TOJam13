using UnityEngine;
using System.Collections;

public class CampLookAt : MonoBehaviour {

    public Transform lookAt;
    public float smoothing = .9f;
    public Vector3 additionalRotation;
	
	// Update is called once per frame
	void Update () {

        Quaternion q = transform.rotation;
        transform.LookAt(lookAt);
        transform.Rotate(additionalRotation);
        transform.rotation = Quaternion.Lerp(q, transform.rotation, smoothing);
	}
}
