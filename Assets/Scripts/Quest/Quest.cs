using UnityEngine;

public class Quest : MonoBehaviour {
    public string title;
    public string description;
    public int reward;
    private AbstractTask[] tasks;
    private bool started = false;
    private bool complete = false;

    private void Start() {
        this.tasks = transform.GetComponentsInChildren<AbstractTask>();
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public void Enter() {
        foreach(AbstractTask task in tasks) {
            task.Enter();
        }
        started = true;
        EventManager.Get().Broadcast(new QuestStartEvent(this));
    }

    public void Exit() {
        complete = true;
        EventManager.Get().Broadcast(new QuestEndEvent(this));
    }

    public bool IsStarted() {
        return started;
    }

    public bool TasksDone() {
        if(complete) return true;

        foreach(AbstractTask task in tasks) {
            if(!task.IsDone()) return false;
        }

        return true;
    }

    public bool IsComplete() {
        return complete;
    }

    public AbstractTask[] GetTasks() {
        return tasks;
    }

    public string GetTitle() {
        return title;
    }

    public string GetDescription() {
        return description;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
