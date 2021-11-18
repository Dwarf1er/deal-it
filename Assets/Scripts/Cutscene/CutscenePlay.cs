using UnityEngine;

public class CutscenePlay : CutsceneAbstract {
    public override void Enter() {
        EventManager.Get().Broadcast(new StartEvent());
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
