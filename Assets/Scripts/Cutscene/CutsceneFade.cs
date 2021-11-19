using System.Collections;
using UnityEngine;

public class CutsceneFade : CutsceneAbstract {
    private bool fout;
    private Fade fade;

    public CutsceneFade(bool fout) {
        this.fout = fout;
        this.fade = Object.FindObjectOfType<Fade>();
    }

    public override void Enter() {
        if(fout) fade.FadeOut();
        else fade.FadeIn();
    }

    public override bool Loop() {
        return fade.IsTransitioning();
    }

    public override void Exit() {}
}
