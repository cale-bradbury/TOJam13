using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPatchOnAwake : MonoBehaviour {

    public AudioHelm.HelmController controller;
    public AudioHelm.HelmPatch patch;

	// Use this for initialization
	void OnEnable () {
        controller.LoadPatch(patch);
	}
}
