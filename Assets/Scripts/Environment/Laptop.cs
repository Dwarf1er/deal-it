using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable {
    public bool IsInteractable() {
        return true;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }
}
