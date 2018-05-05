using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDepthTexture : MonoBehaviour {



	// Use this for initialization
	void OnEnable () {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
