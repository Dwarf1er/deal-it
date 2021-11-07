using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : GotoState {
    public PatrolState(IStateHandler stateHandler) : base(stateHandler, stateHandler.GetGraph().RandomNode().GetPosition()) {}

    public override State NextState() {
        if(this.IsNextState(0.001f)) return stateHandler.GetBaseState();

        return this;
    }

    public override void Loop() {
        if(path != null && path.Count == 0) {
            path = null;
            target = stateHandler.GetGraph().RandomNode().GetPosition();
            Enter();
        }

        base.Loop();
    }

    public override bool IsComplete() {
        return true;
    }
}
