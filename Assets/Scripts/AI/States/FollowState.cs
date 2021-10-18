using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : AIState {
    private Transform target;
    private float followRange = 1.0f;
    private float stoppingRange = 0.05f;

    public FollowState(AI ai, Transform target) : base(ai) {
        this.target = target;
    }

    public override AIState NextState() {
        if(this.IsComplete()) return new IdleState(this.ai);

        return this;
    }

    public override void Enter() {}

    public override void Update() {
        this.ai.MoveTowards(this.target.position);
    }

    public override void Exit() {
        this.ai.ResetDirection();
    }

    public override bool IsComplete() {
        float distance = this.ai.DistanceTo(target);

        return distance < this.stoppingRange || distance > this.followRange;
    }
}
