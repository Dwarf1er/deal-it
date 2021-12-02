using UnityEngine;

public class SecurityCamera : MonoBehaviour, IInteractable, ISubscriber {
    private bool open = false;

    private void Start() {
        EventManager.Get()
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent));
    }

    public void OnToggle(ToggleEvent toggleEvent) {
        if(!toggleEvent.GetTarget().Equals(transform)) return;
        open = !open;
    }

    public bool IsInteractable() {
        return open;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
