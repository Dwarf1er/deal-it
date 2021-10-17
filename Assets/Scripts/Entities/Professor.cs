using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor : AI {
    public override void Start() {
        base.Start();
        this.eventManager.onClassStart += OnClassStart;
        this.eventManager.onClassEnd += OnClassEnd;
    }

    public void OnDestroy() {
        this.eventManager.onClassStart -= OnClassStart;
        this.eventManager.onClassEnd -= OnClassEnd;
    }

    private void OnClassStart() {
        this.SetNextState(new SequenceState(this, new AIState[]{
            new GotoState(this, new Vector3(0, -0.75f)),
            new GotoObjectState(this, "Board"),
            new WaitState(this, Vector2.down)
        }));
    }

    private void OnClassEnd() {
        this.SetNextState(new IdleState(this));
    }
}
