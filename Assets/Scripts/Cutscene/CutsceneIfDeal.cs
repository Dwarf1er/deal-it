using UnityEngine;

public class CutsceneIfDeal : CutsceneBoolConditional, ISubscriber {
    private IDealable interactable;
    private bool interacted = false;

    public CutsceneIfDeal(string target) {
        this.interactable = GameObject.Find(target).GetComponent<IDealable>();
        EventManager.Get()
            .Subscribe((DealEndEvent interactEvent) => OnDealEnd(interactEvent));
    }

    protected override bool GetConditionalBool() {
        if(interacted) {
            interacted = false;
            return true;
        }

        return false;
    }

    private void OnDealEnd(DealEndEvent interactEvent) {
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
