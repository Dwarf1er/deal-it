using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bropst : MonoBehaviour, IDealable, ISubscriber, IQuestProvider {
    private bool canTalk = false;
    private Quest quest;

    private void Start() {
        quest = QuestManager.Get().GetQuest("Coffee Time");

        EventManager.Get()
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void OnToggle(ToggleEvent toggleEvent) {
        if(!toggleEvent.GetTarget().Equals(transform)) return;
        canTalk = !canTalk;
    }
    public Quest GetQuest() {
        return quest;
    }

    public bool IsDealable() {
        return quest.IsComplete() && !quest.IsDone();
    }

    public bool IsInteractable() {
        return canTalk && !quest.IsComplete() && !quest.IsDone();
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
