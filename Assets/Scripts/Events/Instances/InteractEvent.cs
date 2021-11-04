using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractEvent : IInteractionEvent<object, IInteractable> {
    private object from;
    private IInteractable to;

    public InteractEvent(object from, IInteractable to) {
        this.from = from;
        this.to = to;
    }

    public Vector2 GetPosition() {
        return to.GetPosition();
    }

    public object GetFrom() {
        return from;
    }

    public IInteractable GetTo() {
        return to;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class InteractStartEvent : InteractEvent, IStartEvent {
    private InteractEndEvent endEvent;

    public InteractStartEvent(object from, IInteractable to) : base(from, to) {
        this.endEvent = new InteractEndEvent(this);
    }

    public IEndEvent GetEndEvent() {
        return endEvent;
    }
}

public class InteractEndEvent : InteractEvent, IEndEvent, ICancellableEvent {
    private InteractStartEvent startEvent;
    private bool cancelled = false;

    public InteractEndEvent(InteractStartEvent startEvent) : base(startEvent.GetFrom(), startEvent.GetTo()) {
        this.startEvent = startEvent;
    }

    public IEvent GetStartEvent() {
        return startEvent;
    }

    public void Cancel() {
        this.cancelled = true;
    }

    public bool IsCancelled() {
        return cancelled;
    }
}
