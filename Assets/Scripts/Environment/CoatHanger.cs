using UnityEngine;

public class CoatHanger : MonoBehaviour, ISubscriber {
    public Sprite[] sprites;
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
        spriteRenderer.sprite = sprites[1];
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
