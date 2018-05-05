using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

    public RainController controller;
    new internal Rigidbody rigidbody;

	// Use this for initialization
	void Awake () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < 0)
            controller.RemoveRain(this);
	}

    private void OnCollisionEnter(Collision collision)
    {
        controller.HitRain(this, collision);
    }
}
