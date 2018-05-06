using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmColliderKey : MonoBehaviour {

    public AudioHelm.HelmController controller;
    public int key;
    public LayerMask layer;

    // Update is called once per frame
    void OnTriggerEnter(Collider c)
    {
        if (((1 << c.gameObject.layer) & layer) != 1)
        {
            controller.NoteOn(key);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (((1 << c.gameObject.layer) & layer) != 1)
        {
            controller.NoteOff(key);
        }
    }
}
