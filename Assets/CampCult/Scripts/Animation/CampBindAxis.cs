using UnityEngine;
using System.Collections;

public class CampBindAxis : MonoBehaviour {

	public Transform bind;
	public bool bindX=true;
	public bool bindY = true;
	public bool bindZ = true;
	public bool smoothing = false;
	public float smooth = .5f;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 v = transform.position;
        float t = 1;// Time.deltaTime;// / Application.targetFrameRate;

        if (smoothing){
			if(bindX)v.x = Mathf.Lerp(v.x, bind.position.x+offset.x, smooth*t);
			if(bindY)v.y = Mathf.Lerp(v.y, bind.position.y + offset.y, smooth*t);
			if(bindZ)v.z = Mathf.Lerp(v.z, bind.position.z + offset.z, smooth*t);
		}else{
			if(bindX)v.x = bind.position.x + offset.x;
			if(bindY)v.y = bind.position.y + offset.y;
			if(bindZ)v.z = bind.position.z + offset.z;
		}
		transform.position = v;
	}
}
