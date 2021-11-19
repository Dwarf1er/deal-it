using System.Collections.Generic;

public class CutsceneCollection : CutsceneAbstract {
    private int currentStep;
    private bool inTransition;
    private List<CutsceneAbstract> children;

    public CutsceneCollection() {
        children = new List<CutsceneAbstract>();
    }

    public override void AddChild(CutsceneAbstract child) {
        children.Add(child);        
    }

    public override CutsceneAbstract[] GetChildren() {
        return children.ToArray();
    }

    protected void SetStep(int step) {
        currentStep = step;
    }

    public override void Enter() {
        currentStep = 0;
        inTransition = true;
    }

    public override bool Loop() {
        if(currentStep >= children.Count) return false;

        if(inTransition) {
            inTransition = false;
            children[currentStep].Enter();
        }

        if(!children[currentStep].Loop()) {
            children[currentStep++].Exit();
            inTransition = true;
        }

        return true;
    }

    public override void Exit() {}
}
