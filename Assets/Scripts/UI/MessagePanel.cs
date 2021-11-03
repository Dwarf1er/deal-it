using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : UIPanel {
    private Text text;
    public string message;
    private bool delayClose = false;

    protected override Vector3 GetOffset() {
        return new Vector3(0, 150.0f, 0.0f);
    }

    private void Start() {
        text = GetComponentInChildren<Text>();
    }

    private void Update() {
        if(!delayClose) {
            delayClose = true;
            StartCoroutine(DelayedClose(3.0f));
        }
        text.text = message;
    }

    private IEnumerator DelayedClose(float seconds) {
        yield return new WaitForSeconds(seconds);
        Close();
    }
}
