using UnityEngine;

public class ToggleEvent : IEvent {
    private Transform target;

    public ToggleEvent(Transform target) {
        this.target = target;
    }

    public Transform GetTarget() {
        return target;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}
