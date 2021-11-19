using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : StateHuman {
    protected override void Start() {
        base.Start();

        EventManager.Get().Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
    }

    protected override string GetTextureName() {
        return "guard1";
    }

    public override float GetSpeed() {
        return 0.75f;
    }

    public override State GetBaseState() {
        return new IdleState(this);
    }

    private void OnAlert(AlertEvent alertEvent) {
        Vector3 position = alertEvent.GetPosition();

        SetNextState(new SequenceState(this, new State[]{
            new GotoState(this, position),
            new FollowState(this, alertEvent.target)
        }));
    }
}
