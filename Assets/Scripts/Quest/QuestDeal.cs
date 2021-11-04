using UnityEngine;

public class QuestDeal : QuestAbstract {
    public Transform dealable;
    private IDealable idealable;
    private string dealName;

    private void Start() {
        dealName = dealable.name;

        if(dealable.TryGetComponent<IDealable>(out IDealable deal)) {
            idealable = deal;
        } else {
            throw new System.Exception("Deal should be IDealable.");
        }

        EventManager.Get()
            .Subscribe((DealEndEvent dealEvent) => OnDealEnd(dealEvent));
    }

    private void OnDealEnd(DealEndEvent dealEvent) {
        if(dealEvent.GetTo() != idealable) return;
        Done();
    }
    public override string GetQuestName() {
        return "Deal to " + dealName;
    }
}