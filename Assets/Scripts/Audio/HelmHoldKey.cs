using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmHoldKey : MonoBehaviour {

    public AudioHelm.HelmController controller;
    public int note = 64;
    public float velocity = 1;

    void Start()
    {
        Invoke("Key", .1f);
    }

    void OnEnable()
    {
        Key();
    }
    
    void OnDisable ()
    {
        controller.NoteOff(note);
    }

    void Key()
    {
        controller.NoteOn(note, velocity);
    }
}
