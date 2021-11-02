using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePannel : UIPannel {
    private bool animateText = false;

    protected override void Start() {
        base.Start();

        EventManager.Get()
            .Subscribe((DealInputEvent inputEvent) => HandleInput());
    }

    protected override Vector3 GetOffset() {
        return new Vector3(0.0f, -200.0f, 0.0f);
    }

    private void HandleInput() {
        if(IsOpen()) Close();
    }

    private void Update() {
        if(!animateText) {
            animateText = true;
            StartCoroutine(ScrollMessage());
        }
    }

    private IEnumerator ScrollMessage() {
        for(int i = 0; i < message.Length; i++) {
            text.text = message.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}