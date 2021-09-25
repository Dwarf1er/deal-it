using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState {
    public IdleState(AI ai) : base(ai) {}

    public override AIState NextState() {
        if(this.IsNextState(0.01f)) return new PatrolState(this.ai);

        return this;
    }

    public override void Enter() {}

    public override void Update() {}

    public override void Exit() {}

    public override bool IsComplete() {
        return true;
    }
}
