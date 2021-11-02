using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePannel : UIPannel {
    private bool delayClose = false;

    protected override Vector3 GetOffset() {
        return new Vector3(0, 100.0f, 0.0f);
    }

    void Update() {
        if(!delayClose) {
            delayClose = true;
            StartCoroutine(DelayedClose(5.0f));
        }
        text.text = message;
    }

    private IEnumerator DelayedClose(float seconds) {
        yield return new WaitForSeconds(seconds);
        Close();
    }
}
