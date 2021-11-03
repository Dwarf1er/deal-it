using UnityEngine;

public class CutsceneHuman : AI {
    public void Goto(Vector2 target) {
        SetNextState(new GotoState(this, target));
    }

    public void SetLookAt(Transform transform) {
        SetNextState(new LookAtState(this, transform));
    }

    public void Patrol() {
        SetNextState(new PatrolState(this));
    }

    public void Stop() {
        SetNextState(new LookAtState(this, Vector2.zero));
    }
}
