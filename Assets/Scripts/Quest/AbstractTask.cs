using UnityEngine;

public abstract class AbstractTask : MonoBehaviour, ISubscriber {
    private bool done = false;
    private bool started = false;

    protected virtual void Start() {}

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool IsDone() {
        return done;
    }

    protected void Done() {
        if(!started) return;
        if(done) return;
        done = true;
        EventManager.Get().Broadcast(new TaskEndEvent(this));
    }

    public void Enter() {
        started = true;
    }

    public abstract string GetTitle();
    public bool HasDistance() {
        return false;
    }
    public Transform GetTransform() {
        return transform;
    }
}