using UnityEngine;

public class CutsceneQuest : CutsceneAbstract {
    private Quest quest;
    private bool start;

    public CutsceneQuest(string target, bool start) {
        this.start = start;
        this.quest = QuestManager.Get().GetQuest(target);
    }

    public override void Enter() {
        if(start) {
            quest.Enter();
        } else {
            quest.Exit();
        }
        
    }

    public override bool Loop() {
        return false;
    }

    public override void Exit() {}
}
