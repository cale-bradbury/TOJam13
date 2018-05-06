using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmSurfaceDistance : MonoBehaviour {

    public AudioHelm.HelmController controller;
    public AudioHelm.Param param;
    public LayerMask layerMask;
    public AnimationCurve distanceToValue = AnimationCurve.Linear(0,1,1,0);
    public AudioSource source;
    public float panAmount = .2f;
    public float smoothing = .5f;
    float value = 0;

    // Use this for initialization
    void Start () {
        value = controller.GetParameterValue(param);
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        float distLeft, distRight;
        distLeft = distRight = 100000;

        if(Physics.Raycast(transform.position, -transform.right, out hit,100,layerMask))
            distLeft = hit.distance;
        if(Physics.Raycast(transform.position, transform.right, out hit, 100, layerMask))
            distRight = hit.distance;

        float dist = Mathf.Min(distRight, distLeft);
        float pan = 0;
        if (distRight != distLeft)
        {
            if (distRight > distLeft)
            {
                pan = panAmount;
            }
            else
            {
                pan = -panAmount;
            }
        }

        source.panStereo = Mathf.Lerp(source.panStereo, pan, smoothing);
        value = Mathf.Lerp(value, distanceToValue.Evaluate(dist), smoothing);
        controller.SetParameterPercent(param, value);
    }
}
