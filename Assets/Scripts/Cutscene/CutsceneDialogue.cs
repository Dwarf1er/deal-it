using System.Collections;
using UnityEngine;

public class CutsceneDialogue : CutsceneAbstract {
    public string message;
    private UIPannel uiPannel;

    public override void Enter() {
        uiPannel = UIManager.Get().ShowDialogueMessage(message);
    }

    public override bool Loop() {
        return !uiPannel.IsDone();
    }

    public override void Exit() {

    }
}
