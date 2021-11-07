using System.Collections;
using UnityEngine;

public class CutsceneWaitQuest : CutsceneAbstract, ISubscriber {
    public Quest quest;
    private bool done;

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public override void Enter() {
        done = false;
        EventManager.Get()
            .Subscribe((QuestEndEvent questEvent) => OnQuestEnd(questEvent));
    }

    private void OnQuestEnd(QuestEndEvent questEvent) {
        if(questEvent.GetQuest() == quest) {
            done = true;
        }
    }

    public override bool Loop() {
        return !done;
    }

    public override void Exit() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
