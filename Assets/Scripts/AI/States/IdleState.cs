using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
    public IdleState(IStateHandler stateHandler) : base(stateHandler) {}

    public override State NextState() {
        if(this.IsNextState(0.01f)) return new PatrolState(stateHandler);

        return this;
    }

    public override void Enter() {}

    public override void Loop() {}

    public override void Exit() {}

    public override bool IsComplete() {
        return true;
    }
}
