using UnityEngine;

public class CutsceneIfDeal : CutsceneBoolConditional, ISubscriber {
    private string target;
    private IDealable interactable;
    private bool interacted = false;

    public CutsceneIfDeal(string target) {
        this.target = target;
        AttachObject();
        EventManager.Get()
            .Subscribe((DealEndEvent interactEvent) => OnDealEnd(interactEvent));
    }

    private void AttachObject() {
        if(interactable != null) return;

        GameObject gameObj = GameObject.Find(target);

        if(gameObj != null) this.interactable = gameObj.GetComponent<IDealable>();
    }

    protected override bool GetConditionalBool() {
        AttachObject();

        if(interacted) {
            interacted = false;
            return true;
        }

        return false;
    }

    private void OnDealEnd(DealEndEvent interactEvent) {
        if(!interactEvent.GetTo().Equals(interactable)) return;
        if(interactEvent.IsCancelled()) return;
        interacted = true;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
