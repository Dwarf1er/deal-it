using UnityEngine;

public abstract class AbstractTask : ISubscriber {
    private bool done = false;
    private bool started = false;

    public bool IsDone() {
        return done;
    }

    protected void Done() {
        if(!started) return;
        if(done) return;
        done = true;
        EventManager.Get().Broadcast(new TaskEndEvent(this));
        // TODO: Prevent concurrent delete.
        // EventManager.Get().UnSubcribeAll(this);
    }

    public virtual void Enter() {
        started = true;
    }

    public abstract string GetTitle();
    public bool HasDistance() {
        return false;
    }
    public Transform GetTransform() {
        return null;
    }
}