using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : StateHuman, IDealable {
    private IEndEvent endEvent;

    protected override void Start() {
        base.Start();

        EventManager.Get().Subscribe((ClassStartEvent classEvent) => OnClassStart(classEvent));
        EventManager.Get().Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent));

        EventManager.Get().Subscribe((ClassEndEvent classEvent) => OnClassEnd(classEvent));
        EventManager.Get().Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
    }

    protected override string GetTextureName() {
        return "student1";
    }

    public override float GetSpeed() {
        return 0.75f;
    }

    public override State GetBaseState() {
        return new IdleState(this);
    }

    private void OnClassStart(ClassStartEvent classEvent) {
        endEvent = classEvent.GetEndEvent();

        Vector3 position = classEvent.GetPosition();

        this.SetNextState(new SequenceState(this, new State[]{
            new GotoState(this, position),
            new GotoObjectState(this, "Chair"),
            new LookAtState(this, Vector2.down)
        }));
    }

    public bool IsDealable() {
        return endEvent != null;
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
