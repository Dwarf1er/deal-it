using UnityEngine;

public class Pusher : StateHuman {
    public override State GetBaseState() {
        return new IdleState(this);
    }

    protected override string GetTextureName() {
        return "dealer1";
    }

    protected override float GetSpeed() {
        return 0.8f;
    }
}