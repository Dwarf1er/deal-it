using System.Collections.Generic;

public abstract class CutsceneConditional : CutsceneAbstract {
    private Dictionary<string, CutsceneAbstract> stepMapping;
    private CutsceneAbstract targetStep;

    protected abstract string GetConditionalString();

    public CutsceneConditional() {
        this.stepMapping = new Dictionary<string, CutsceneAbstract>();
    }

    public override void AddChild(CutsceneAbstract child) {
        stepMapping.Add(child.GetName(), child);
    }

    public override void Enter() {
        string targetValue = GetConditionalString().ToLower();

        if(stepMapping.ContainsKey(targetValue)) {
            targetStep = stepMapping[targetValue];
        } else if(stepMapping.ContainsKey("else")) {
            targetStep = stepMapping["else"];
        } else {
            targetStep = null;
        }

        if(targetStep != null) targetStep.Enter();
    }

    public override bool Loop() {
        if(targetStep == null) return false;
        return targetStep.Loop();
    }

    public override void Exit() {
        if(targetStep != null) targetStep.Exit();
    }
}

public abstract class CutsceneBoolConditional : CutsceneConditional {
    protected override string GetConditionalString() {
        return (GetConditionalBool() ? "true" : "false");
    }
    protected abstract bool GetConditionalBool();
}
