using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    public Rain prefab;
    List<Rain> active;
    List<Rain> unactive;
    public float spawnRadius;
    public Vector2 spawnTimeMinMax;
    public AudioHelm.HelmController controller;
    public AnimationCurve noteDotCurve;
   // public Vector3 rainForce;


    private void Awake()
    {
        active = new List<Rain>();
        unactive = new List<Rain>();
    }
    // Use this for initialization
    void OnEnable()
    {
        Spawn();
    }
    void OnDisable()
    {
        CancelInvoke("Spawn");
    }

    // Update is called once per frame
    void Spawn () {
        Rain r;
        if (unactive.Count > 0)
        {
            r = unactive[0];
            unactive.RemoveAt(0);
        }
        else
        {
            r = Instantiate<Rain>(prefab);
            r.controller = this;
        }
        active.Add(r);
        r.rigidbody.velocity = Vector3.zero;
        r.gameObject.SetActive(true);
        r.transform.position = transform.position;
        Vector2 p = Random.insideUnitCircle * spawnRadius;
        r.transform.position += new Vector3(p.x, 0, p.y);
        Invoke("Spawn", Random.Range(spawnTimeMinMax.x, spawnTimeMinMax.y));
    }

    public void RemoveRain(Rain r)
    {/*
        Vector3 force = rainForce * Time.deltaTime;
        for (int i = active.Count-1; i>=0; i--)
        {
            active[i].transform.position += force;
            if (active[i].transform.position.y < 0)
            {
                unactive.Add(active[i]);
                active[i].gameObject.SetActive(false);
                active.RemoveAt(i);
            }
        }    */
        active.Remove(r);
        unactive.Add(r);
        r.gameObject.SetActive(false);
    }

    public void HitRain(Rain r, Collision c)
    {
        RemoveRain(r);
        float dot = Vector3.Dot(Vector3.up, c.impulse.normalized);
        int note = Mathf.FloorToInt(noteDotCurve.Evaluate(dot));
        Debug.Log(note+"   "+dot+"   "+c.impulse.magnitude);
        controller.NoteOn(note, .3f,.05f);
        //controller.NoteOn(note, 1);
    }
}
