using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor : StateHuman {
    protected override void Start() {
        base.Start();

        EventManager.Get().Subscribe((ClassStartEvent classEvent) => OnClassStart(classEvent));
        EventManager.Get().Subscribe((ClassEndEvent classEvent) => OnClassEnd(classEvent));
    }

    protected override string GetTextureName() {
        return "professor2";
    }

    public override float GetSpeed() {
        return 0.5f;
    }

    public override State GetBaseState() {
        return new IdleState(this);
    }

    private void OnClassStart(ClassStartEvent classEvent) {
        Vector3 position = classEvent.GetPosition();

        SetNextState(new SequenceState(this, new State[]{
            new GotoState(this, position),
            new GotoObjectState(this, "Board"),
            new LookAtState(this, GameObject.Find("P1").transform)
        }));
    }

    private void OnClassEnd(ClassEndEvent classEvent) {
        this.SetNextState(new IdleState(this));
    }
}
