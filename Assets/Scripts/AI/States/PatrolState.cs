using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState {
    private Node target;

    public PatrolState(AI ai) : base(ai) {
        this.target = null;
    }

    public override AIState NextState() {
        if(this.IsNextState(0.001f)) return new IdleState(this.ai);

        return this;
    }

    public override void Enter() {
        if(target == null) {
            this.target = ai.graph.GetNearestNode(ai.transform.position);
        }
    }

    public override void Update() {
        if(this.ai.ReachedPosition(this.target.transform.position)) this.target = target.RandomNeighbor();

        this.ai.MoveTowards(this.target.transform.position);
    }

    public override void Exit() {
        this.ai.ResetDirection();
    }

    public override bool IsComplete() {
        return true;
    }
}
