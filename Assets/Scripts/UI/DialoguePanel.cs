using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : UIPanel {
    public Text nameText;
    public Text messageText;
    public DialogueStartEvent dialogueEvent;
    private bool animateText = false;

    private void Start() {
        EventManager.Get()
            .Subscribe((InteractInputEvent inputEvent) => HandleInput());
    }

    protected override Vector3 GetOffset() {
        return new Vector3(0.0f, -200.0f, 0.0f);
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