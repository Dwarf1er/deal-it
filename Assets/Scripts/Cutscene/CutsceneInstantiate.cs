using UnityEngine;

public class CutsceneInstantiate : CutsceneAbstract {
    private GameObject prefab;
    private Vector2 position;

    public CutsceneInstantiate(string resource, Vector2 position) {
        this.prefab = GameObject.Find(resource);
        this.position = position;
    }

    public override void Enter() {
        this.prefab.transform.position = position;
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
