using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collider2D) {
        if(collider2D.transform.tag == "Player") {
            EventManager.Get().BroadcastAll(new AlertEvent(transform.position, collider2D.transform));
        }
    }
}
