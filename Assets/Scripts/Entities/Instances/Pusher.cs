using UnityEngine;

public class Pusher : StateHuman {
    public override State GetBaseState() {
        return new IdleState(this);
    }

    protected override string GetTextureName() {
        return "dealer1";
    }

    public override float GetSpeed() {
        return 0.8f;
    }
}