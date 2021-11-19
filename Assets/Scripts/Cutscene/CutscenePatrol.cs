using System.Collections;
using UnityEngine;

public class CutscenePatrol : CutsceneAbstract {
    private StateHuman[] stateHumans;

    public CutscenePatrol(string[] actors) {
        this.stateHumans = new StateHuman[actors.Length];
        int i = 0;
        foreach(string actor in actors) {
            this.stateHumans[i++] = GameObject.Find(actor).GetComponent<StateHuman>();
        }
    }

    public override void Enter() {
        foreach(StateHuman actor in stateHumans) {
            actor.Patrol();
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
