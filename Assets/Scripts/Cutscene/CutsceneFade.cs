using System.Collections;
using UnityEngine;

public class CutsceneFade : CutsceneAbstract {
    public bool fadeOut;
    private Fade fade;

    public override void Enter() {
        fade = FindObjectOfType<Fade>();
        if(fadeOut) fade.FadeOut();
        else fade.FadeIn();
    }

    public override bool Loop() {
        return fade.IsTransitioning();
    }

    public override void Exit() {

    }
}
