using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtState : State {
    Vector2 direction;
    Transform target;
    public LookAtState(IStateHandler stateHandler, Vector2 direction) : base(stateHandler) {
        this.direction = direction;
    }

    public LookAtState(IStateHandler stateHandler, Transform target) : base (stateHandler) {
        this.target = target;
    }

    public override State NextState() {
        return this;
    }

    public override void Enter() {
        stateHandler.ResetDirection();
    }

    public override void Loop() {
        if(target != null) {
            stateHandler.LookAt(target.position);
        } else {
            stateHandler.LookTowards(direction);
        }
    }

    public override void Exit() {}

    public override bool IsComplete() {
        return false;
    }
}
