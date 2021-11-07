using System.Collections;
using UnityEngine;

public class CutsceneDialogue : CutsceneAbstract, ISubscriber {
    public string entityName;
    public string message;
    private IEndEvent dialogueEndEvent;
    private bool done;

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public override void Enter() {
        done = false;

        DialogueStartEvent dialogueEvent = new DialogueStartEvent(entityName, message);
        this.dialogueEndEvent = dialogueEvent.GetEndEvent();

        EventManager.Get()
            .Subscribe((DialogueEndEvent dialogueEvent) => OnDialogueEnd(dialogueEvent))
            .Broadcast(dialogueEvent);
    }

    public override bool Loop() {
        return !done;
    }

    public override void Exit() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void OnDialogueEnd(DialogueEndEvent dialogueEvent) {
        done = dialogueEvent == this.dialogueEndEvent;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
