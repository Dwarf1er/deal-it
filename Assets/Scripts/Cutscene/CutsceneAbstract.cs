using UnityEngine;
using System.Collections.Generic;

public abstract class CutsceneAbstract {
    public virtual string GetName() {
        return "";
    }

    public virtual void AddChild(CutsceneAbstract child) {}
    public virtual CutsceneAbstract[] GetChildren() {
        return new CutsceneAbstract[]{};
    }

    public abstract void Enter();

    public abstract bool Loop();

    public abstract void Exit();
}
