using UnityEngine;

public abstract class QuestAbstract : MonoBehaviour, ISubscriber {
    private bool done = false;

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }
    public bool IsDone() {
        return done;
    }
    protected void Done() {
        if(done) return;
        done = true;
        EventManager.Get().Broadcast(new QuestEndEvent(this));
    }
    public abstract string GetQuestName();
    public bool HasDistance() {
        return false;
    }
    public Transform GetTransform() {
        return transform;
    }
}