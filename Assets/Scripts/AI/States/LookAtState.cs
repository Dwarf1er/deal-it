using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtState : AIState {
    Vector2 direction;
    Transform target;
    public LookAtState(AI ai, Vector2 direction) : base(ai) {
        this.direction = direction;
    }

    public LookAtState(AI ai, Transform target) : base (ai) {
        this.target = target;
    }

    public override AIState NextState() {
        return this;
    }

    public override void Enter() {
        this.ai.direction = new Vector2();
    }

    public override void Update() {
        if(target != null) this.ai.LookAt(target);
        else this.ai.LookAt(direction);
    }

    public override void Exit() {}

    public override bool IsComplete() {
        return false;
    }
}
