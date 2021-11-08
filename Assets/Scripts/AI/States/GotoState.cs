using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoState : State {
    protected Queue<Vector2> path;
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

        Vector2[] shortestPath = stateHandler.GetPath(stateHandler.GetTransform().position, target);
        this.path = new Queue<Vector2>(shortestPath);
    }

    public override void Loop() {
        if(path == null || path.Count == 0) return;

        Vector3 target = path.Peek();

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
