using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBonusScore : MonoBehaviour {

    public LayerMask layer;
    public float score = 111;
    public float fuel = 10;

    // Update is called once per frame
    void OnTriggerEnter(Collider c)
    {
        if (((1 << c.gameObject.layer) & layer) != 1)
        {
            ScoreManager.instance.AddBonus((int)score);
            PlayerMovement.instance.AddFuel(fuel);
        }
    }
}
