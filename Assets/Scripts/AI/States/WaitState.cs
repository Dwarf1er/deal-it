using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : AIState {
    Vector2 direction;
    bool setDirection;
    public WaitState(AI ai, Vector2 direction) : base(ai) {
        this.direction = direction;
    }

    public override AIState NextState() {
        return this;
    }

    public override void Enter() {
        this.ai.direction = direction;
        this.setDirection = false;
    }

    public override void Update() {
        if(!this.setDirection) {
            this.setDirection = true;
            this.ai.direction = new Vector2();
        }
    }

    public override void Exit() {}

    public override bool IsComplete() {
        return false;
    }
}
