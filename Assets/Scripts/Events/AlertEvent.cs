using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEvent : IEvent {
    public Vector3 position { get; }
    public Transform target { get; }

    public AlertEvent(Vector3 position, Transform target) {
        this.position = position;
        this.target = target;
    }
}
