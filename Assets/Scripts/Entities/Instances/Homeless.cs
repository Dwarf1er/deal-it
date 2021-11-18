using UnityEngine;

public class Homeless : StateHuman, IInteractable {
    protected override string GetTextureName() {
        return "homeless1";
    }

    public bool IsInteractable() {
        return true;
    }

    public override State GetBaseState() {
        return new LookAtState(this, Vector2.down);
    }

    public override float GetSpeed() {
        return 0.5f;
    }
}
