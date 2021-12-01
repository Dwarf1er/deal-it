using UnityEngine;

public class Door : MonoBehaviour, IInteractable, ISubscriber {
    public Sprite[] sprites;
    private bool open = false;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        EventManager.Get()
            .Subscribe((ToggleEvent toggleEvent) => OnToggle(toggleEvent));
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public void OnToggle(ToggleEvent toggleEvent) {
        if(!toggleEvent.GetTarget().Equals(transform)) return;
        open = !open;
        spriteRenderer.sprite = sprites[open ? 1 : 0];
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
