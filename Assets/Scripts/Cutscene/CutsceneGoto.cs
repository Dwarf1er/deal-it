using System.Collections;
using UnityEngine;

public class CutsceneGoto : CutsceneAbstract {
    private StateHuman[] stateHumans;
    private Transform targetTransform;
    private Vector2 offset;
    private Vector2 lookTowards;
    private bool done;

    public CutsceneGoto(string[] actors, string target, Vector2 offset, Vector2 lookTowards) {
        this.offset = offset;
        this.lookTowards = lookTowards;
        this.stateHumans = new StateHuman[actors.Length];
        int i = 0;
        foreach(string actor in actors) {
            this.stateHumans[i++] = GameObject.Find(actor).GetComponent<StateHuman>();
        }
        this.targetTransform = GameObject.Find(target).transform;
    }

    public override void Enter() {
        foreach(StateHuman actor in stateHumans) {
            actor.Goto((Vector2)targetTransform.position + offset);
        }
    }

    public override bool Loop() {
        foreach(StateHuman actor in stateHumans) {
            if(!actor.IsGoto()) return false;
        }
        
        return true;
    }

    public override void Exit() {
        foreach(StateHuman actor in stateHumans) {
            actor.Stop();
            actor.LookTowards(lookTowards);
        }
    }
}
