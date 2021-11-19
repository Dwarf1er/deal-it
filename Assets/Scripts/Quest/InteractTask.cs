using UnityEngine;

public class InteractTask : AbstractTask {
    private IInteractable iinteractable;
    private string interactName;

    public InteractTask(string target) {
        interactName = target;

        if(GameObject.Find(target).TryGetComponent<IInteractable>(out IInteractable interact)) {
            iinteractable = interact;
        } else {
            throw new System.Exception("Interact should be IInteractable.");
        }
    }

    public override void Enter() {
        base.Enter();

        EventManager.Get()
            .Subscribe((InteractEndEvent interactEvent) => OnInteractEnd(interactEvent));
    }

    private void OnInteractEnd(InteractEndEvent interactEvent) {
        if(interactEvent.GetTo() != iinteractable) return;
        Done();
    }

    public override string GetTitle() {
        return "Interact with " + interactName;
    }
}