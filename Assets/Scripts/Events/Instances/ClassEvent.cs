using UnityEngine;

public abstract class ClassEvent : IEvent {
    private Vector2 position;

    public ClassEvent(Vector2 position) {
        this.position = position;
    }

    public Vector2 GetPosition() {
        return this.position;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class ClassStartEvent : ClassEvent, IStartEvent {
    private ClassEndEvent classEndEvent;

    public ClassStartEvent(Vector2 position) : base(position) {
        this.classEndEvent = new ClassEndEvent(this);
    }

    public IEndEvent GetEndEvent() {
        return classEndEvent;
    }
}

public class ClassEndEvent : ClassEvent, IEndEvent, IDelayedEvent {
    private ClassStartEvent classStartEvent;

    public ClassEndEvent(ClassStartEvent classStartEvent) : base(classStartEvent.GetPosition()) {
        this.classStartEvent = classStartEvent;
    }

    public IEvent GetStartEvent() {
        return this.classStartEvent;
    }

    public float GetDelay() {
        return 30.0f;
    }
}
