using UnityEngine;

public class CutsceneIfInteract : CutsceneBoolConditional, ISubscriber {
    private IInteractable interactable;
    private bool interacted = false;

    public CutsceneIfInteract(string target) {
        this.interactable = GameObject.Find(target).GetComponent<IInteractable>();
        EventManager.Get()
            .Subscribe((InteractEndEvent interactEvent) => OnInteractEnd(interactEvent));
    }

    protected override bool GetConditionalBool() {
        if(interacted) {
            interacted = false;
            return true;
        }

        return false;
    }

    private void OnInteractEnd(InteractEndEvent interactEvent) {
        if(!interactEvent.GetTo().Equals(interactable)) return;
        interacted = true;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
