using System.Collections.Generic;
using UnityEngine;

public class Quest : ISubscriber {
    public string title;
    public int reward;
    public string description;
    private HashSet<AbstractTask> children;
    private int taskCompleteCount;
    private bool started = false;
    private bool done = false;
    private bool complete = false;

    public Quest(string title) {
        this.title = title;
        this.children = new HashSet<AbstractTask>();
    }

    public void Enter() {
        EventManager.Get().Subscribe((TaskEndEvent taskEvent) => OnTaskEnd(taskEvent));
        foreach(AbstractTask task in children) {
            task.Enter();
        }
        taskCompleteCount = 0;
        started = true;
        EventManager.Get().Broadcast(new QuestStartEvent(this));
    }

    public void Exit() {
        done = true;
        EventManager.Get().UnSubcribeAll(this);
        EventManager.Get().Broadcast(new QuestEndEvent(this));
    }

    public HashSet<AbstractTask> GetChildren() {
        return this.children;
    }

    public void AddChild(AbstractTask task) {
        children.Add(task);
    }

    public bool IsStarted() {
        return started;
    }

    public bool IsComplete() {
        return complete;
    }

    private void OnTaskEnd(TaskEndEvent taskEvent) {
        if(!children.Contains(taskEvent.GetTask())) return;
        if(++taskCompleteCount < children.Count) return;

        complete = true;
        EventManager.Get().Broadcast(new QuestCompleteEvent(this));
    }

    public bool IsDone() {
        return done;
    }

    public string GetTitle() {
        return title;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
