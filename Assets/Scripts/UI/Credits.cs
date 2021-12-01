using UnityEngine;

public class Credits : MonoBehaviour, IInteractable, ISubscriber {
    private RectTransform rectTransform;

    private void Start() {
        this.rectTransform = GetComponent<RectTransform>();
        EventManager.Get()
            .Subscribe((MoveInputEvent inputEvent) => OnInput(inputEvent))
            .Subscribe((InteractInputEvent inputEvent) => OnInput(inputEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    private void Update() {
        rectTransform.pivot = new Vector2(rectTransform.pivot.x, rectTransform.pivot.y - Time.deltaTime);
    }

    private void OnInput(object inputEvent) {
        EventManager.Get().Broadcast(new InteractStartEvent(this, this));
    }

    public bool IsInteractable() {
        return true;
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }

    public Vector2 GetPosition() {
        return Vector2.zero;
    }
}
