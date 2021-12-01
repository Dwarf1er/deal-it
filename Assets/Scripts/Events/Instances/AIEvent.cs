using UnityEngine;

public class ReachedEvent : IEvent {
    private Transform target;

    public ReachedEvent(Transform target) {
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
