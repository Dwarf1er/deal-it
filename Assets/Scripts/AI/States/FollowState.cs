using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State {
    private Transform target;
    private static readonly float FOLLOW_RANGE = 0.8f;
    private static readonly float STOPPING_RANGE = 0.05f;

    public FollowState(IStateHandler stateHandler, Transform target) : base(stateHandler) {
        this.target = target;
    }

    public override State NextState() {
        if(this.IsComplete()) return stateHandler.GetBaseState();

        return this;
    }

    public override void Enter() {}

    public override void Loop() {
        stateHandler.MoveTowards(this.target.position);
    }

    public override void Exit() {
        stateHandler.ResetDirection();
    }

    public override bool IsComplete() {
        float distance = stateHandler.DistanceTo(target.position);

        if(distance < STOPPING_RANGE) {
            EventManager.Get().Broadcast(new ReachedEvent(this.target));
            return true;
        }

        if(distance > FOLLOW_RANGE) {
            return true;
        }

        return false;
    }
}
