using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : UIPanel {
    public Text messageText;

    protected override void Start() {
        base.Start();

        EventManager.Get()
            .Subscribe((PanelInputEvent inputEvent) => OnPanelInput(inputEvent));
    }

    private void Update() {
        messageText.text = "";
        foreach(QuestAbstract quest in QuestManager.Get().quests) {
            messageText.text += (quest.IsDone() ? "*" : "~") + " " + quest.GetQuestName() + "\n\n";
        }
    }

    protected override Vector2 GetOffset() {
        return new Vector2(-2.0f, 0.0f);
    }

    protected override bool DestroyOnClose() {
        return false;
    }

    private void OnPanelInput(PanelInputEvent inputEvent) {
        if(this.IsOpen()) {
            Close();
        } else {
            Open();
        }
    }
}
