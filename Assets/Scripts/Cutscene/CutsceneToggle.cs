using UnityEngine;

public class CutsceneToggle : CutsceneAbstract {
    private string[] targets;

    public CutsceneToggle(string[] targets) {
        this.targets = targets;
    }

    public override void Enter() {
        EventManager em = EventManager.Get();

        foreach(string target in targets) {
            em.Broadcast(new ToggleEvent(GameObject.Find(target).transform));
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
