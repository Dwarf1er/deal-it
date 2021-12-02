using UnityEngine;

public class CutsceneDestroy : CutsceneAbstract {
    private GameObject targetGameObject;

    public CutsceneDestroy(string target) {
        this.targetGameObject = GameObject.Find(target);
    }

    public override void Enter() {
        targetGameObject.transform.position = new Vector3(-99, -99, 0);
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
