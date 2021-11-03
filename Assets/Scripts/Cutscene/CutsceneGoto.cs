using System.Collections;
using UnityEngine;

public class CutsceneGoto : CutsceneAbstract {
    public CutsceneHuman[] humans;
    public Transform target;
    public Vector2 offset;
    public Vector2 lookAt;
    private bool done;

    public override void Enter() {
        foreach(CutsceneHuman human in humans) {
            human.Goto((Vector2)target.position + offset);
        }
    }

    public override bool Loop() {
        foreach(CutsceneHuman human in humans) {
            if(!(human.GetState() is GotoState)) return false;
        }
        
        return true;
    }

    public override void Exit() {
        foreach(CutsceneHuman human in humans) {
            human.Stop();
            human.LookAt(lookAt);
        }
    }
}
