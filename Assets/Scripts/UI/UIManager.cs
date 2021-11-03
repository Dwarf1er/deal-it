using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ISubscriber {
    private static UIManager uiManager;

    private void Start() {
        if(uiManager) Destroy(this);
        uiManager = this;

        EventManager.Get()
            .Subscribe((ClassStartEvent classStartEvent) => OnClassStart(classStartEvent))
            .Subscribe((ClassEndEvent classEndEvent) => OnClassEnd(classEndEvent))
            .Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent))
            .Subscribe((DialogueStartEvent dialogueEvent) => OnDialogue(dialogueEvent));
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }

    public static UIManager Get() {
        return uiManager;
    }

    private MessagePanel ShowPopupMessage(string message) {
        GameObject prefab = (GameObject)Resources.Load("UI/Popup");
        GameObject canvas = GameObject.Find("Canvas");

        GameObject instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        instance.transform.SetParent(canvas.transform, false);

        MessagePanel panel = instance.GetComponent<MessagePanel>();
        panel.message = message;
        panel.Open();

        return panel;
    }

    private DialoguePanel ShowDialogueMessage(DialogueStartEvent dialogueEvent) {
        GameObject prefab = (GameObject)Resources.Load("UI/Dialogue");
        GameObject canvas = GameObject.Find("Canvas");

        GameObject instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        instance.transform.SetParent(canvas.transform, false);

        DialoguePanel panel = instance.GetComponent<DialoguePanel>();
        panel.dialogueEvent = dialogueEvent;
        panel.Open();

        return panel;
    }

    private void OnClassStart(ClassStartEvent classStartEvent) {
        ShowPopupMessage("Class Started");
    }

    private void OnClassEnd(ClassEndEvent classStartEvent) {
        ShowPopupMessage("Class Ended");
    }

    private void OnAlert(AlertEvent alertEvent) {
        ShowPopupMessage("Alert");
    }

    private void OnDialogue(DialogueStartEvent dialogueEvent) {
        ShowDialogueMessage(dialogueEvent);
    }
}
