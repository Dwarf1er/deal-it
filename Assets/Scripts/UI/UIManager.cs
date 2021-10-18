using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ISubscriber {
    private static UIManager uiManager;

    void Start() {
        if(uiManager) Destroy(this);
        uiManager = this;

        EventManager.Get()
            .Subscribe((ClassStartEvent classStartEvent) => OnClassStart(classStartEvent))
            .Subscribe((ClassEndEvent classEndEvent) => OnClassEnd(classEndEvent))
            .Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return null;
    }

    private void ShowMessage(string message) {
        GameObject prefab = (GameObject)Resources.Load("UI/MessagePannel");
        GameObject canvas = GameObject.Find("Canvas");

        GameObject instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        instance.transform.SetParent(canvas.transform, false);

        MessagePannel messagePannel = instance.GetComponent<MessagePannel>();
        messagePannel.message = message;
        messagePannel.Begin();
    }

    private void OnClassStart(ClassStartEvent classStartEvent) {
        ShowMessage("Class Started");
    }

    private void OnClassEnd(ClassEndEvent classStartEvent) {
        ShowMessage("Class Ended");
    }

    private void OnAlert(AlertEvent alertEvent) {
        ShowMessage("Alert");
    }
}
