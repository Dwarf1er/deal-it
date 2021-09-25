using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoState : AIState {
    private Queue<Node> path;
    private Vector3 target;

    public GotoState(AI ai, Vector3 target) : base(ai) {
        this.target = target;
        this.path = null;
    }

    public override AIState NextState() {
        if(path.Count == 0) return new IdleState(this.ai);

        return this;
    }

    public override void Enter() {
        if(path == null || path.Count == 0) {
            Node[] shortestPath = this.ai.graph.GetPathTo(this.ai.transform.position, target);
            this.path = new Queue<Node>(shortestPath);
        }
    }

    public override void Update() {
        if(this.path.Count == 0) return;

        Node nextNode = path.Peek();
        Vector3 target = nextNode.transform.position;

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
