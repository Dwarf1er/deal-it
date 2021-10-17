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
        this.SetNextState(new SequenceState(this, new AIState[]{
            new GotoState(this, new Vector3(classEvent.position.x, classEvent.position.y)),
            new GotoObjectState(this, "Board"),
            new WaitState(this, Vector2.down)
        }));
    }

    private void OnClassEnd(ClassEndEvent classEvent) {
        this.SetNextState(new IdleState(this));
    }
}
