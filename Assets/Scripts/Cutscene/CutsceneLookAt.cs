using System.Collections;
using UnityEngine;

public class CutsceneLookAt : CutsceneAbstract {
    public StateHuman actor;
    public Transform target;

    public override void Enter() {
        actor.SetLookAt(target);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
