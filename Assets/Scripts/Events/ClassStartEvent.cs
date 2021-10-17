using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStartEvent : IEvent {
    public Vector2 position { get; }

    public ClassStartEvent(Vector2 position) {
        this.position = position;
    }
}
