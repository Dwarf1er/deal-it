using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : IEvent {
    private string sound;

    public SoundEvent(string sound) {
        this.sound = sound;
    }

    public string GetSound() {
        return sound;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}
