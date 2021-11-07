using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoState : State {
    protected Queue<TilemapNode> path;
    protected Vector3 target;

    public GotoState(IStateHandler stateHandler, Vector3 target) : base(stateHandler) {
        this.target = target;
        this.path = null;
    }

    public override State NextState() {
        if(path.Count == 0) return stateHandler.GetBaseState();

        return this;
    }

    public override void Enter() {
        if(path != null && path.Count > 0) return;

        TilemapNode[] shortestPath = stateHandler.GetGraph().GetPathTo(stateHandler.GetTransform().position, target);
        this.path = new Queue<TilemapNode>(shortestPath);
    }

    public override void Loop() {
        if(path == null || path.Count == 0) return;

        TilemapNode nextNode = path.Peek();
        Vector3 target = nextNode.GetPosition();

        stateHandler.MoveTowards(target);

        if(stateHandler.DistanceTo(target) < 0.025f) this.path.Dequeue();
    }

    public override void Exit() {
        stateHandler.ResetDirection();
    }

    public override bool IsComplete() {
        return path.Count == 0;
    }
}
