using UnityEngine;

public class CutsceneSound : CutsceneAbstract {
    private string name;

    public CutsceneSound(string name) {
        this.name = name;
    }

    public override void Enter() {
        EventManager.Get().Broadcast(new SoundEvent(name));
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
