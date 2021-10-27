using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DealEvent : IInteractionEvent<IDealer, IDealable> {
    private IDealer from;
    private IDealable to;

    public DealEvent(IDealer from, IDealable to) {
        this.from = from;
        this.to = to;
    }

    public Vector2 GetPosition() {
        return (from.GetPosition() + to.GetPosition()) / 2.0f;
    }

    public IDealer GetFrom() {
        return this.from;
    }

    public IDealable GetTo() {
        return this.to;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class DealStartEvent : DealEvent, IStartEvent {
    private DealEndEvent dealEndEvent;

    public DealStartEvent(IDealer from, IDealable to) : base(from, to) {
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
