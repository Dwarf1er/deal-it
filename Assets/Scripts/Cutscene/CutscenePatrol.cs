using System.Collections;
using UnityEngine;

public class CutscenePatrol : CutsceneAbstract {
    public StateHuman[] actors;

    public override void Enter() {
        foreach(StateHuman actor in actors) {
            actor.Patrol();
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
