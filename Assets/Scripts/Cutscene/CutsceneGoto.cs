using System.Collections;
using UnityEngine;

public class CutsceneGoto : CutsceneAbstract {
    public StateHuman[] actors;
    public Transform target;
    public Vector2 offset;
    public Vector2 lookTowards;
    private bool done;

    public override void Enter() {
        foreach(StateHuman actor in actors) {
            actor.Goto((Vector2)target.position + offset);
        }
    }

    public override bool Loop() {
        foreach(StateHuman actor in actors) {
            if(!actor.IsGoto()) return false;
        }
        
        return true;
    }

    public override void Exit() {
        foreach(StateHuman actor in actors) {
            actor.Stop();
            actor.LookTowards(lookTowards);
        }
    }
}
