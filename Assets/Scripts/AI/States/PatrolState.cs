using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : GotoState {
    public PatrolState(AI ai) : base(ai, ai.GetGraph().RandomNode().GetPosition()) {}

    public override AIState NextState() {
        if(this.IsNextState(0.001f)) return new IdleState(this.ai);

        return this;
    }

    public override void Update() {
        if(path != null && path.Count == 0) {
            path = null;
            target = ai.GetGraph().RandomNode().GetPosition();
            Enter();
        }

        base.Update();
    }

    public override bool IsComplete() {
        return true;
    }
}
