using UnityEngine;

public class InteractTask : AbstractTask {
    public Transform interactable;
    private IInteractable iinteractable;
    private string interactName;

    protected override void Start() {
        base.Start();

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

    public override string GetTitle() {
        return "Interact with " + interactName;
    }
}