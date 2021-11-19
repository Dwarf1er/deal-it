using UnityEngine;

public class CutsceneDestroy : CutsceneAbstract {
    private GameObject targetGameObject;

    public CutsceneDestroy(string target) {
        this.targetGameObject = GameObject.Find(target);
    }

    public override void Enter() {
        GameObject.Destroy(targetGameObject);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
