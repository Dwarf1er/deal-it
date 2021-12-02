using UnityEngine;

public class Homeless : StateHuman, IInteractable {
    private bool canTalk = false;

    protected override void Start() {
        base.Start();

        EventManager.Get()
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent));
    }

    private void OnToggle(ToggleEvent toggleEvent) {
        if(!toggleEvent.GetTarget().Equals(transform)) return;
        canTalk = !canTalk;
    }

    protected override string GetTextureName() {
        return "homeless1";
    }

    public bool IsInteractable() {
        return canTalk;
    }

    public override State GetBaseState() {
        return new LookAtState(this, Vector2.down);
    }

    public override float GetSpeed() {
        return 0.5f;
    }
}
