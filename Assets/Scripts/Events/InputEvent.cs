using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent : IEvent {
    public string player { get; }
    public Vector2 direction { get; }

    public InputEvent(string player, Vector2 direction) {
        this.player = player;
        this.direction = direction;
    }
}
