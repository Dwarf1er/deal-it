using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : AI, IDealable {
    private IEndEvent endEvent;

    public override void Start() {
        this.textureName = "student1";

        base.Start();

        EventManager.Get().Subscribe((ClassStartEvent classEvent) => OnClassStart(classEvent));
        EventManager.Get().Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent));

        EventManager.Get().Subscribe((ClassEndEvent classEvent) => OnClassEnd(classEvent));
        EventManager.Get().Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
    }

    /// TODO: Don't copy from other class.
    private bool CanStartEvent() {
        if(endEvent == null) return true;
        if(endEvent is ICancellableEvent) {
            ICancellableEvent cancellableEvent = (ICancellableEvent)endEvent;
            cancellableEvent.Cancel();
            endEvent = null;
            return true;
        }

        return false;
    }

    private void OnClassStart(ClassStartEvent classEvent) {
        if(!CanStartEvent()) return;

        endEvent = classEvent.GetEndEvent();

        Vector3 position = classEvent.GetPosition();

        this.SetNextState(new SequenceState(this, new AIState[]{
            new GotoState(this, position),
            new GotoObjectState(this, "Chair"),
            new LookAtState(this, Vector2.down)
        }));
    }

    public bool IsDealable() {
        return CanStartEvent();
    }

    private void OnDealStart(DealStartEvent dealEvent) {
        if(!dealEvent.GetTo().Equals(this)) return;

        endEvent = dealEvent.GetEndEvent();

        this.SetNextState(new LookAtState(this, dealEvent.GetFrom().GetTransform()));
    }

    private void OnEndEvent(IEndEvent endEvent) {
        if(this.endEvent != endEvent) return;

        this.endEvent = null;

        this.SetNextState(new IdleState(this));
    }

    private void OnClassEnd(ClassEndEvent classEvent) {
        OnEndEvent(classEvent);
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        OnEndEvent(dealEvent);
    }
}
