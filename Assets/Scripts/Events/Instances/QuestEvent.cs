using UnityEngine;

public abstract class QuestEvent : IEvent {
    private QuestAbstract quest;

    public QuestEvent(QuestAbstract quest) {
        this.quest = quest;
    }

    public QuestAbstract GetQuest() {
        return quest;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class QuestEndEvent : QuestEvent {
    public QuestEndEvent(QuestAbstract quest) : base(quest) {}
} 