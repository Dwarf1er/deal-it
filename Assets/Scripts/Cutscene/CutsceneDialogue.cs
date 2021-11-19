using System.Collections;
using UnityEngine;

public class CutsceneDialogue : CutsceneAbstract, ISubscriber {
    private string title;
    private string message;
    private IEndEvent dialogueEndEvent;
    private bool done;

    public CutsceneDialogue(string name, string message) {
        this.title = name;
        this.message = message;

        EventManager.Get()
            .Subscribe((DialogueEndEvent dialogueEvent) => OnDialogueEnd(dialogueEvent));
    }

    public override void Enter() {
        done = false;

        DialogueStartEvent dialogueEvent = new DialogueStartEvent(title, message);
        this.dialogueEndEvent = dialogueEvent.GetEndEvent();
        EventManager.Get().Broadcast(dialogueEvent);
    }

    public override bool Loop() {
        return !done;
    }

    public override void Exit() {}

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
