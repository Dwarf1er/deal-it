using UnityEngine;

public class PlayerInventory : Inventory, ISubscriber {
    public int money;

    private void Start() {
        EventManager.Get()
            .Subscribe((InteractEndEvent interactEvent) => OnInteractEnd(interactEvent))
            .Subscribe((QuestEndEvent questEvent) => OnQuestEnd(questEvent));
    }

    private void OnQuestEnd(QuestEndEvent questEvent) {
        money += questEvent.GetQuest().reward;
    }

    private void OnInteractEnd(InteractEndEvent interactEvent) {
        if(interactEvent.GetTo() is Trash) {
            money += Random.Range(0, 10);
        }
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}