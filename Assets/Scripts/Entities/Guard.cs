using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : AI {
    public override void Start() {
        base.Start();

        EventManager.Get().Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
    }

    private void OnAlert(AlertEvent alertEvent) {
        this.SetNextState(new SequenceState(this, new AIState[]{
            new GotoState(this, new Vector3(alertEvent.position.x, alertEvent.position.y)),
            new FollowState(this, alertEvent.target)
        }));
    }
}
