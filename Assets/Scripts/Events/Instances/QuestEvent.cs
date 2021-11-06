using UnityEngine;

public abstract class QuestEvent : IEvent {
    private Quest quest;

    public QuestEvent(Quest quest) {
        this.quest = quest;
    }

    public Quest GetQuest() {
        return quest;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class QuestStartEvent : QuestEvent {
    public QuestStartEvent(Quest quest) : base(quest) {}
}

public class QuestEndEvent : QuestEvent {
    public QuestEndEvent(Quest quest) : base(quest) {}
}

public abstract class TaskEvent : IEvent {
    private AbstractTask task;

    public TaskEvent(AbstractTask task) {
        this.task = task;
    }

    public AbstractTask GetTask() {
        return this.task;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }

    public float GetRange() {
        return float.MaxValue;
    }
}

public class TaskStartEvent : TaskEvent {
    public TaskStartEvent(AbstractTask task) : base(task) {}
}

public class TaskEndEvent : TaskEvent {
    public TaskEndEvent(AbstractTask task) : base(task) {}
}
