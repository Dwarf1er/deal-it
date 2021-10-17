using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoObjectState : AIState {
    private string targetName;
    private Vector3 target;

    public GotoObjectState(AI ai, string target) : base(ai) {
        this.targetName = target;
        this.target = new Vector3();
    }

    public override AIState NextState() {
        if(this.IsComplete()) return new IdleState(this.ai);

        return this;
    }

    public override void Enter() {
        float nearestDistance = float.MaxValue;
        GameObject nearestObject = null;

        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag(this.targetName)) {
            float distance = this.ai.DistanceTo(gameObject);
            
            if(distance < nearestDistance) {
                nearestDistance = distance;
                nearestObject = gameObject;
            }
        }

        this.target = nearestObject.transform.position;
    }

    public override void Update() {
        this.ai.MoveTowards(this.target);
    }

    public override void Exit() {
        this.ai.ResetDirection();
    }

    public override bool IsComplete() {
        return this.ai.ReachedPosition(this.target);
    }
}
