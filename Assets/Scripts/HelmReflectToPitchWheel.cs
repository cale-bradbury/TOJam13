using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmReflectToPitchWheel : MonoBehaviour {

    public CampReflectFloat input;
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
    public AudioHelm.HelmController controller;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float v = curve.Evaluate(input.GetFloat());
        float transpose = 0;
        float tune = 0;
        if (v > 0)
        {
            transpose =  Mathf.Floor(v);
            tune = (v) % 1;
        }
        else if(v<0)
        {
            transpose = Mathf.Ceil(v) - 1;
            tune = (v) % 1;
        }
        controller.SetParameterValue(AudioHelm.CommonParam.kOsc2Transpose, transpose);
        controller.SetParameterPercent(AudioHelm.CommonParam.kOsc2Tune, tune);
        controller.SetParameterValue(AudioHelm.CommonParam.kOsc1Transpose, transpose);
        controller.SetParameterPercent(AudioHelm.CommonParam.kOsc1Tune, tune);
    }
}
