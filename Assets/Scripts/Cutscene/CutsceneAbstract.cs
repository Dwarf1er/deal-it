using UnityEngine;

public abstract class CutsceneAbstract : MonoBehaviour {
    public abstract void Enter();

    public abstract bool Loop();

    public abstract void Exit();
}
