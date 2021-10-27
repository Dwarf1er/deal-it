using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor : AI {
    public override void Start() {
        base.Start();

        EventManager.Get().Subscribe((ClassStartEvent classEvent) => OnClassStart(classEvent));
        EventManager.Get().Subscribe((ClassEndEvent classEvent) => OnClassEnd(classEvent));
    }

    private void OnClassStart(ClassStartEvent classEvent) {
        Vector3 position = classEvent.GetPosition();

        this.SetNextState(new SequenceState(this, new AIState[]{
            new GotoState(this, position),
            new GotoObjectState(this, "Board"),
            new LookAtState(this, GameObject.Find("P1").transform)
        }));
    }

    private void OnClassEnd(ClassEndEvent classEvent) {
        this.SetNextState(new IdleState(this));
    }
}
