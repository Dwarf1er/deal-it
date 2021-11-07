using UnityEngine;

public class QuestManager : MonoBehaviour {
    private Quest[] quests;
    private static QuestManager questManager;

    private void Start() {
        if(questManager) Destroy(this);
        questManager = this;
        this.quests = transform.GetComponentsInChildren<Quest>();
    }

    public static QuestManager Get() {
        return questManager;
    }

    public Quest[] GetQuests() {
        return this.quests;
    }

    public bool HasQuest(string title) {
        foreach(Quest quest in quests) {
            if(quest.GetTitle() == title) return quest.IsStarted();
        }

        return false;
    }
}