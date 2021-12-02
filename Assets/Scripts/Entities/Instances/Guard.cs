using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : StateHuman {
    private static readonly float SPOT_DISTANCE = 0.75f;

    protected override void Start() {
        base.Start();

        EventManager.Get()
            .Subscribe((DealStartEvent dealEvent) => OnDealStart(dealEvent))
            .Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
    }

    protected override string GetTextureName() {
        return "guard1";
    }

    public override float GetSpeed() {
        return 0.6f;
    }

    public override State GetBaseState() {
        return new IdleState(this);
    }

    private void OnDealStart(DealStartEvent dealEvent) {
        if((dealEvent.GetFrom().GetPosition() - (Vector2)transform.position).magnitude < SPOT_DISTANCE) {
            EventManager.Get().Broadcast(new AlertEvent(this.transform.position, dealEvent.GetFrom().GetTransform()));
        }   
    }

    private void OnAlert(AlertEvent alertEvent) {
        Vector3 position = alertEvent.GetPosition();

        SetNextState(new SequenceState(this, new State[]{
            new GotoState(this, position),
            new FollowState(this, alertEvent.target)
        }));
    }
}
