using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEvent : IEvent {
    private Vector2 position;
    public Transform target { get; }

    public AlertEvent(Vector2 position, Transform target) {
        this.position = position;
        this.target = target;
    }

    public Vector2 GetPosition() {
        return this.position;
    }

    public float GetRange() {
        return 10.0f;
    }
}
