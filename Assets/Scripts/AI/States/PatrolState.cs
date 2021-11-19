using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : GotoState {
    public PatrolState(IStateHandler stateHandler) : base(stateHandler, stateHandler.RandomPosition()) {}

    public override State NextState() {
        if(this.IsNextState(0.001f)) return stateHandler.GetBaseState();

        return this;
    }

    public override void Loop() {
        if(base.IsComplete()) {
            path = null;
            target = stateHandler.RandomPosition();
            Enter();
        }

        base.Loop();
    }

    public override bool IsComplete() {
        return true;
    }
}
