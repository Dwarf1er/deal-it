using UnityEngine;

public class DealTask : AbstractTask {
    private IDealable idealable;
    private string dealName;

    public DealTask(string target) {
        dealName = target;

        if(GameObject.Find(target).TryGetComponent<IDealable>(out IDealable deal)) {
            idealable = deal;
        } else {
            throw new System.Exception("Deal should be IDealable.");
        }
    }

    public override void Enter() {
        base.Enter();
        EventManager.Get()
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        if(dealEvent.GetTo() != idealable) return;
        Done();
    }

    public override string GetTitle() {
        return "Deal to " + dealName;
    }
}