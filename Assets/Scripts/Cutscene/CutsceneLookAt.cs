using System.Collections;
using UnityEngine;

public class CutsceneLookAt : CutsceneAbstract {
    private StateHuman[] stateHumans;
    private Transform targetTransform;

    public CutsceneLookAt(string[] actors, string target) {
        this.stateHumans = new StateHuman[actors.Length];
        int i = 0;
        foreach(string actor in actors) {
            this.stateHumans[i++] = GameObject.Find(actor).GetComponent<StateHuman>();
        }
        this.targetTransform = GameObject.Find(target).transform;
    }

    public override void Enter() {
        foreach(StateHuman actor in stateHumans) {
            actor.SetLookAt(targetTransform);
        }
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
