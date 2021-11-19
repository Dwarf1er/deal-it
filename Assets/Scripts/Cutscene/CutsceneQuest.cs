using UnityEngine;

public class CutsceneQuest : CutsceneAbstract {
    private string target;
    private Quest quest;
    private bool start;

    public CutsceneQuest(string target, bool start) {
        this.target = target;
        this.start = start;
    }

    public override void Enter() {
        if(this.quest == null) {
            this.quest = QuestManager.Get().GetQuest(target);
        }
            
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
