using System.Collections;
using UnityEngine;

public class CutsceneIfQuest : CutsceneConditional, ISubscriber {
    private Quest quest;
    private string questState = "None";

    public CutsceneIfQuest(string target) {
        this.quest = QuestManager.Get().GetQuest(target);

        EventManager.Get()
            .Subscribe((QuestStartEvent questEvent) => OnQuestStart(questEvent))
            .Subscribe((QuestCompleteEvent questEvent) => OnQuestComplete(questEvent))
            .Subscribe((QuestEndEvent questEvent) => OnQuestEnd(questEvent));
    }

    protected override string GetConditionalString() {
        return questState;
    }

    private void OnQuestStart(QuestStartEvent questEvent) {
        if(questEvent.GetQuest() == quest) questState = "Active";
    }

    private void OnQuestComplete(QuestCompleteEvent questEvent) {
        if(questEvent.GetQuest() == quest) questState = "Complete";
    }

    private void OnQuestEnd(QuestEndEvent questEvent) {
        if(questEvent.GetQuest() == quest) questState = "End";
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }
}
