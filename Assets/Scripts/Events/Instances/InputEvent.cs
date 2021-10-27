using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInputEvent : IEvent {
    public string player { get; }
    public Vector2 direction { get; }

    public MoveInputEvent(string player, Vector2 direction) {
        this.player = player;
        this.direction = direction;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class DealInputEvent : IEvent {
    public string player { get; }

    public DealInputEvent(string player) {
        this.player = player;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}
