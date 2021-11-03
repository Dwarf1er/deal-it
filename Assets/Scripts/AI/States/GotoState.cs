using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoState : AIState {
    protected Queue<TilemapNode> path;
    protected Vector3 target;

    public GotoState(AI ai, Vector3 target) : base(ai) {
        this.target = target;
        this.path = null;
    }

    public override AIState NextState() {
        if(path.Count == 0) return new IdleState(ai);

        return this;
    }

    public override void Enter() {
        if(path != null && path.Count > 0) return;

        TilemapNode[] shortestPath = this.ai.GetGraph().GetPathTo(this.ai.transform.position, target);
        this.path = new Queue<TilemapNode>(shortestPath);
    }

    public override void Update() {
        if(path == null || path.Count == 0) return;

        TilemapNode nextNode = path.Peek();
        Vector3 target = nextNode.GetPosition();

        this.ai.MoveTowards(target);

        if(this.ai.ReachedPosition(target)) this.path.Dequeue();
    }

    public override void Exit() {
        this.ai.ResetDirection();
    }

    public override bool IsComplete() {
        return path.Count == 0;
    }
}
