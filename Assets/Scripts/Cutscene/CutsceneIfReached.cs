using UnityEngine;

public class CutsceneIfReached : CutsceneBoolConditional, ISubscriber {
    private Transform interactable;
    private bool interacted = false;

    public CutsceneIfReached(string target) {
        this.interactable = GameObject.Find(target).transform;
        EventManager.Get()
            .Subscribe((ReachedEvent interactEvent) => OnReached(interactEvent));
    }

    protected override bool GetConditionalBool() {
        if(interacted) {
            interacted = false;
            return true;
        }

        return false;
    }

    private void OnReached(ReachedEvent interactEvent) {
        if(!interactEvent.GetTarget().Equals(interactable)) return;
        interacted = true;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
