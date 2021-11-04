using UnityEngine;

public class QuestManager : MonoBehaviour {
    public QuestAbstract[] quests;
    private static QuestManager questManager;

    private void Start() {
        if(questManager) Destroy(this);
        questManager = this;
        this.quests = transform.GetComponentsInChildren<QuestAbstract>();
    }

    public static QuestManager Get() {
        return questManager;
    }
}