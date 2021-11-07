using System.Collections;
using UnityEngine;

public class CutsceneFadeDestroy : CutsceneAbstract {
    public GameObject target;
    private SpriteRenderer spriteRenderer;
    private bool done;

    private IEnumerator FadeDestroy() {
        Color color = spriteRenderer.color;

        for(int i = 0; i <= 100; i++) {
            color.a = Mathf.Lerp(1.0f, 0.0f, i / 100.0f);
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.005f);
        }

        Destroy(this.gameObject);
        done = true;
    }

    public override void Enter() {
        done = false;
        spriteRenderer = target.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeDestroy());
    }

    public override bool Loop() {
        return !done;
    }

    public override void Exit() {}
}
