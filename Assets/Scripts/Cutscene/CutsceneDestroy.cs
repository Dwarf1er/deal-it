using UnityEngine;

public class CutsceneDestroy : CutsceneAbstract {
    public GameObject target;

    public override void Enter() {
        Destroy(target);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
