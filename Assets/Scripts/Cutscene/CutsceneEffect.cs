using UnityEngine;

public class CutsceneEffect : CutsceneAbstract {
    private string name;

    public CutsceneEffect(string name) {
        this.name = name;
    }

    public override void Enter() {
        if(name == "glitch") {
            Glitch.glitch = true;
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
