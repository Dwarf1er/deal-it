using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    private List<Quest> quests;
    private static QuestManager questManager;

    private void Awake() {
        if(questManager) Destroy(this);
        questManager = this;
        this.quests = new List<Quest>();
        foreach(QuestFile questFile in FindObjectsOfType<QuestFile>()) {
            foreach(Quest quest in questFile.GetQuests()) {
                quests.Add(quest);
            }
        }
    }

    public static QuestManager Get() {
        return questManager;
    }

    public Quest[] GetQuests() {
        return this.quests.ToArray();
    }

    public Quest GetQuest(string title) {
        foreach(Quest quest in quests) {
            if(quest.GetTitle() == title) return quest;
        }

        throw new System.Exception("Could not find quest \"" + title + "\".");
    }

    public bool HasQuest(string title) {
        foreach(Quest quest in quests) {
            if(quest.GetTitle() == title) return quest.IsStarted();
        }

        return false;
    }
}