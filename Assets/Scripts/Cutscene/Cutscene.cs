using System.Collections.Generic;
using UnityEngine;

public class Cutscene : CutsceneCollection {
    private string name;
    private bool loop;

    public Cutscene(string name, bool loop) {
        this.name = name;
        this.loop = loop;
    }

    public override string GetName() {
        return name;
    }

    public override bool Loop() {
        bool baseState = base.Loop();

        if(!baseState) {
            if(loop) SetStep(0);
            else return false;
        }

        return true;
    }
}
