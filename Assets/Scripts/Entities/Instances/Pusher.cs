using UnityEngine;

public class Pusher : StateHuman, IInteractable {
    public override State GetBaseState() {
        return new IdleState(this);
    }

    public bool IsInteractable() {
        return true;
    }

    protected override string GetTextureName() {
        return "dealer1";
    }

    public override float GetSpeed() {
        return 0.8f;
    }
}