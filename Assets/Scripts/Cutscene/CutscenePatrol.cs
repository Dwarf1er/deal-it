using System.Collections;
using UnityEngine;

public class CutscenePatrol : CutsceneAbstract {
    public CutsceneHuman[] humans;

    public override void Enter() {
        foreach(CutsceneHuman human in humans) {
            human.Patrol();
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
