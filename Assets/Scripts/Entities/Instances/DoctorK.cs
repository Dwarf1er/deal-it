using UnityEngine;

public class DoctorK : StateHuman, IInteractable {
    public override State GetBaseState() {
        return new IdleState(this);
    }

    public bool IsInteractable() {
        return true;
    }

    protected override string GetTextureName() {
        return "drk";
    }

    public override float GetSpeed() {
        return 0.8f;
    }
}
