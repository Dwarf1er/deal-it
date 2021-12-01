using System.Collections;
using UnityEngine;

public class CutscenePatrol : CutsceneAbstract {
    private string[] actors;

    public CutscenePatrol(string[] actors) {
        this.actors = actors;
    }

    public override void Enter() {
        StateHuman[] stateHumans = new StateHuman[actors.Length];
        foreach(string actor in actors) {
            GameObject.Find(actor).GetComponent<StateHuman>().Patrol();
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
