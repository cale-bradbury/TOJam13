using UnityEngine;
using System.Collections;

public class CampFireEventOnTrigger : MonoBehaviour {

	public string eventName;
    public LayerMask layer;

    // Update is called once per frame
    void OnTriggerEnter (Collider c) {
        if (((1 << c.gameObject.layer) & layer) != 1)
        {
            Messenger.Broadcast(eventName);
        }
	}
}
