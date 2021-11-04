using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : UIPanel {
    private Text text;
    public string message;
    private bool delayClose = false;

    protected override Vector2 GetOffset() {
        return new Vector2(0, -2.0f);
    }

    protected override bool DestroyOnClose() {
        return true;
    }

    protected override void Start() {
        base.Start();
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
