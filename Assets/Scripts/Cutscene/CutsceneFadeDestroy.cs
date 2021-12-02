using System.Collections;
using UnityEngine;

public class CutsceneFadeDestroy : CutsceneAbstract {
    private GameObject targetGameObject;
    private SpriteRenderer targetSpriteRenderer;
    private float time;
    private static readonly float TOTAL_TIME = 3.0f;

    public CutsceneFadeDestroy(string target) {
        this.targetGameObject = GameObject.Find(target);
        this.targetSpriteRenderer = targetGameObject.GetComponent<SpriteRenderer>();

    }

    public override void Enter() {
        time = 0;
    }

    public override bool Loop() {
        time += Time.deltaTime;

        Color color = targetSpriteRenderer.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, time / TOTAL_TIME);
        targetSpriteRenderer.color = color;

        return time <= TOTAL_TIME;
    }

    public override void Exit() {
        targetGameObject.transform.position = new Vector3(-99, -99, 0);
    }
}
