using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmReflector : MonoBehaviour {

    public CampReflectFloat input;
    public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
    public AudioHelm.HelmController controller;
    public AudioHelm.Param parameter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        controller.SetParameterValue(parameter, curve.Evaluate(input.GetFloat()));
	}
}
