using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : UIPanel {
    public Text nameText;
    public Text messageText;
    public DialogueStartEvent dialogueEvent;
    private bool animateText = false;

    protected override void Start() {
        base.Start();
        EventManager.Get()
            .Subscribe((DialogueInputEvent inputEvent) => HandleInput());
    }

    protected override bool DestroyOnClose() {
        return true;
    }

    protected override Vector2 GetOffset() {
        return new Vector2(0.0f, 2.0f);
    }

    private void HandleInput() {
        if(IsOpen()) {
            EventManager.Get().Broadcast(dialogueEvent.GetEndEvent());
            Close();
        }
    }

    private void Update() {
        nameText.text = dialogueEvent.GetName();
        if(!animateText) {
            animateText = true;
            StartCoroutine(ScrollMessage());
        }
    }

    private IEnumerator ScrollMessage() {
        string message = dialogueEvent.GetMessage();
        for(int i = 0; i <= message.Length; i++) {
            messageText.text = message.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}