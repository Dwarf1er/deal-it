using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jerry : MonoBehaviour, IInteractable, ISubscriber {
    private void Start() {}

    public bool IsInteractable() {
        return true;
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
