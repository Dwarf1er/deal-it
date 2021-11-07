using System.Collections;
using UnityEngine;

public class CutsceneWait : CutsceneAbstract {
    public float seconds = 1.0f;
    private bool done;

    public override void Enter() {
        done = false;
        StartCoroutine(Sleep(seconds));
    }

    public override bool Loop() {
        return !done;
    }

    public override void Exit() {}

    private IEnumerator Sleep(float seconds) {
        yield return new WaitForSeconds(seconds);
        done = true;
    }
}
