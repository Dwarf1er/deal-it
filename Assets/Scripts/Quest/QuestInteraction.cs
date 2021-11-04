using UnityEngine;

public class QuestInteraction : QuestAbstract {
    public Transform interactable;
    private IInteractable iinteractable;
    private string interactName;

    private void Start() {
        interactName = interactable.name;

        if(interactable.TryGetComponent<IInteractable>(out IInteractable interact)) {
            iinteractable = interact;
        } else {
            throw new System.Exception("Interact should be IInteractable.");
        }

        EventManager.Get()
            .Subscribe((InteractEndEvent interactEvent) => OnInteractEnd(interactEvent));
    }

    private void OnInteractEnd(InteractEndEvent interactEvent) {
        if(interactEvent.GetTo() != iinteractable) return;
        Done();
    }
    public override string GetQuestName() {
        return "Interact with " + interactName;
    }
}