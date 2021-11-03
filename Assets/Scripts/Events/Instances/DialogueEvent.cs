using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueEvent : IEvent {
    private string message;
    private string name;

    public DialogueEvent(string name, string message) {
        this.message = message;
        this.name = name;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public string GetName() {
        return name;
    }

    public string GetMessage() {
        return message;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class DialogueStartEvent : DialogueEvent {
    private DialogueEndEvent endEvent;

    public DialogueStartEvent(string name, string message) : base(name, message) {
        this.endEvent = new DialogueEndEvent(this);
    }

    public IEndEvent GetEndEvent() {
        return endEvent;
    }
}

public class DialogueEndEvent : DialogueEvent, IEndEvent {
    private DialogueStartEvent startEvent;

    public DialogueEndEvent(DialogueStartEvent startEvent) : base(startEvent.GetName(), startEvent.GetMessage()) {
        this.startEvent = startEvent;
    }

    public IEvent GetStartEvent() {
        return startEvent;
    }
}
