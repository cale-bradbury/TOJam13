using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmReflector : MonoBehaviour {

    public CampReflectFloat input;
    public AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
    public AudioHelm.HelmController controller;
    public AudioHelm.Param parameter;
    public bool percent = false;
    public bool debug;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(percent)
            controller.SetParameterPercent(parameter, curve.Evaluate(input.GetFloat()));
        else
            controller.SetParameterValue(parameter, curve.Evaluate(input.GetFloat()));
        if (debug)
        {
            float v = input.GetFloat();
            Debug.Log("value - "+v+"    curved - "+curve.Evaluate(v));
        }
	}
}
