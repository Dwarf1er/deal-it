using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DealEvent : IInteractionEvent {
    private AbstractHuman from;
    private AbstractHuman to;

    public DealEvent(AbstractHuman from, AbstractHuman to) {
        this.from = from;
        this.to = to;
    }

    public Vector2 GetPosition() {
        return (from.transform.position + to.transform.position) / 2.0f;
    }

    public AbstractHuman GetFrom() {
        return this.from;
    }

    public AbstractHuman GetTo() {
        return this.to;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class DealStartEvent : DealEvent, IStartEvent {
    private DealEndEvent dealEndEvent;

    public DealStartEvent(AbstractHuman from, AbstractHuman to) : base(from, to) {
        this.dealEndEvent = new DealEndEvent(this);
    }

    public IEndEvent GetEndEvent() {
        return this.dealEndEvent;
    }
}

public class DealEndEvent : DealEvent, IEndEvent, IDelayedEvent, ICancellableEvent {
    private DealStartEvent dealStartEvent;
    private bool cancelled = false;

    public DealEndEvent(DealStartEvent dealStartEvent) : base(dealStartEvent.GetFrom(), dealStartEvent.GetTo()) {
        this.dealStartEvent = dealStartEvent;
    }

    public IEvent GetStartEvent() {
        return this.dealStartEvent;
    }

    public float GetDelay() {
        return 5.0f;
    }

    public void Cancel() {
        this.cancelled = true;
    }

    public bool IsCancelled() {
        return cancelled;
    }
}
