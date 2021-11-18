using System.Collections.Generic;
using UnityEngine;

public class Quest {
    public string title;
    public int reward;
    public string description;
    private List<AbstractTask> children;
    private bool started = false;
    private bool done = false;
    private bool complete = false;

    public Quest(string title) {
        this.title = title;
        this.children = new List<AbstractTask>();
    }

    public void Enter() {
        foreach(AbstractTask task in children) {
            task.Enter();
        }
        started = true;
        EventManager.Get().Broadcast(new QuestStartEvent(this));
    }

    public void Exit() {
        done = true;
        EventManager.Get().Broadcast(new QuestEndEvent(this));
    }

    public void AddChild(AbstractTask task) {
        children.Add(task);
    }

    public AbstractTask[] GetChildren() {
        return children.ToArray();
    }

    public bool IsStarted() {
        return started;
    }

    public bool IsComplete() {
        if(complete) return true;

        foreach(AbstractTask task in children) {
            if(!task.IsDone()) return false;
        }

        complete = true;
        EventManager.Get().Broadcast(new QuestCompleteEvent(this));

        return complete;
    }

    public bool IsDone() {
        return done;
    }

    public string GetTitle() {
        return title;
    }
}
