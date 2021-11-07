using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : IEvent {
    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}
