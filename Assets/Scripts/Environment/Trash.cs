using UnityEngine;

public class Trash : MonoBehaviour, IInteractable, ISubscriber {

    private IEndEvent endEvent;

    private void Start() {
        EventManager.Get()
            .Subscribe((InteractStartEvent interactEvent) => OnInteractStart(interactEvent))
            .Subscribe((InteractEndEvent interactEvent) => OnInteractEnd(interactEvent))
            .Subscribe((DialogueEndEvent dialogueEvent) => OnDialogueEnd(dialogueEvent));
    }

    public bool IsInteractable() {
        return true;
    }

    public bool HasDistance() {
        return true;
    }

    public Transform GetTransform() {
        return transform;
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void Update() {
        
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    private void OnInteractStart(InteractStartEvent interactEvent) {
        if(!interactEvent.GetTo().Equals(this)) return;
        if(endEvent != null) return; 

        endEvent = interactEvent.GetEndEvent();
    }

    private void OnInteractEnd(InteractEndEvent interactEvent) {
        if(endEvent != interactEvent) return;

        DialogueStartEvent dialogueEvent;
        if(QuestManager.Get().HasQuest("Bropst Quest")) {
            dialogueEvent = new DialogueStartEvent("Trash", "You found... An old coffee. Could still be drinkable.");
        } else {
            dialogueEvent = new DialogueStartEvent("Trash", "You found... Trash. What were you expecting?");
        }

        endEvent = dialogueEvent.GetEndEvent();

        EventManager.Get().Broadcast(dialogueEvent);
    }

    private void OnDialogueEnd(DialogueEndEvent dialogueEvent) {
        if(endEvent != dialogueEvent) return;

        endEvent = null;
    }
}
