using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputEvent : IEvent {
    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class MoveInputEvent : InputEvent {
    public string player { get; }
    public Vector2 direction { get; }

    public MoveInputEvent(string player, Vector2 direction) {
        this.player = player;
        this.direction = direction;
    }
}

public class DealInputEvent : InputEvent {
    public string player { get; }

    public DealInputEvent(string player) {
        this.player = player;
    }
}

public class InteractInputEvent : InputEvent {
    public string player { get; }

    public InteractInputEvent(string player) {
        this.player = player;
    }
}
