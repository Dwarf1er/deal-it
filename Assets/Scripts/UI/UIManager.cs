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
            .Subscribe((AlertEvent alertEvent) => OnAlert(alertEvent));
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

    public UIPannel ShowPopupMessage(string message) {
        GameObject prefab = (GameObject)Resources.Load("UI/Popup");
        GameObject canvas = GameObject.Find("Canvas");

        GameObject instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        instance.transform.SetParent(canvas.transform, false);

        UIPannel messagePannel = instance.GetComponent<UIPannel>();
        messagePannel.message = message;
        messagePannel.Open();

        return messagePannel;
    }

    public UIPannel ShowDialogueMessage(string message) {
        GameObject prefab = (GameObject)Resources.Load("UI/Dialogue");
        GameObject canvas = GameObject.Find("Canvas");

        GameObject instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        instance.transform.SetParent(canvas.transform, false);

        UIPannel messagePannel = instance.GetComponent<UIPannel>();
        messagePannel.message = message;
        messagePannel.Open();

        return messagePannel;
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
}
