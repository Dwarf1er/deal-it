using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jerry : MonoBehaviour, IInteractable, ISubscriber {
    private bool canTalk = false;

    private void Start() {
        EventManager.Get()
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent));
    }

    public bool IsInteractable() {
        return canTalk;
    }

    public void OnToggle(ToggleEvent toggleEvent) {
        if(!toggleEvent.GetTarget().Equals(transform)) return;
        canTalk = !canTalk;
    }

    public bool HasDistance() {
        return true;
    }
    
    public Vector2 GetPosition() {
        return transform.position;
    }

    public Transform GetTransform() {
        return transform;
    }
}
