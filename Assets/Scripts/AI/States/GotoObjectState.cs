using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoObjectState : GotoState {
    private string targetTag;

    public GotoObjectState(IStateHandler stateHandler, string targetTag) : base(stateHandler, Vector3.zero) {
        this.targetTag = targetTag;
    }

    public override void Enter() {
        float nearestDistance = float.MaxValue;
        GameObject nearestObject = null;

        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag(targetTag)) {
            float distance = stateHandler.DistanceTo(gameObject.transform.position);
            
            if(distance < nearestDistance) {
                nearestDistance = distance;
                nearestObject = gameObject;
            }
        }

        this.target = nearestObject.transform.position;

        base.Enter();
    }
}
