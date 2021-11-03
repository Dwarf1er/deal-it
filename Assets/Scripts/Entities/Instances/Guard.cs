using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : AI {
    public override void Start() {
        this.textureName = "guard1";

        base.Start();

        EventManager.Get().Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
    }

    private void OnAlert(AlertEvent alertEvent) {
        Vector3 position = alertEvent.GetPosition();

        this.SetNextState(new SequenceState(this, new AIState[]{
            new GotoState(this, position),
            new FollowState(this, alertEvent.target)
        }));
    }
}
