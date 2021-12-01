using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bropst : MonoBehaviour, IDealable, ISubscriber, IQuestProvider {
    private Quest quest;

    private void Start() {
        quest = QuestManager.Get().GetQuest("Coffee Time");
    }

    public Quest GetQuest() {
        return quest;
    }

    public bool IsDealable() {
        return quest.IsComplete() && !quest.IsDone();
    }

    public bool IsInteractable() {
        return !quest.IsStarted() || quest.IsDone();
    }

    public bool HasDistance() {
        return true;
    }
    
    public Vector2 GetPosition() {
        return transform.position;
    }

    public Transform GetTransform() {
        return transform;
    }
}
