using System.Collections;
using UnityEngine;

public class CutsceneWait : CutsceneAbstract {
    private float seconds;
    private float time;

    public CutsceneWait(float seconds) {
        this.seconds = seconds;
    }

    public override void Enter() {
        time = 0;
    }

    public override bool Loop() {
        time += Time.deltaTime;
        return time <= seconds;
    }

    public override void Exit() {}
}
