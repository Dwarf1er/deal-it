using System.Collections;
using UnityEngine;

public class CutsceneLookAt : CutsceneAbstract {
    public CutsceneHuman human;
    public Transform target;

    public override void Enter() {
        human.SetLookAt(target);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
