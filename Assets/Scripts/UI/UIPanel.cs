using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPanel : MonoBehaviour, ISubscriber {
    private RectTransform rectTransform;
    private Vector2 basePosition;
    private bool open = false;
    private bool transitioning = false;
    private readonly float STEPS = 100;

    private void Awake() {
        this.rectTransform = GetComponent<RectTransform>();
        this.basePosition = rectTransform.pivot;
        this.rectTransform.pivot = this.basePosition + GetOffset();
    }

    protected virtual void Start() {}

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);    
    }

    protected abstract Vector2 GetOffset();
    protected abstract bool DestroyOnClose();

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }

    public bool IsOpen() {
        return open;
    }

    public void Open() {
        if(transitioning) return;

        StartCoroutine(AnimateOpen());
    }

    public void Close() {
        if(transitioning) return;

        StartCoroutine(AnimateClose());
    }

    private IEnumerator AnimateOpen() {
        open = false;
        transitioning = true;

        Vector2 targetPosition = basePosition;

        for(float i = 0; i <= STEPS; i++) {
            rectTransform.pivot = Vector2.Lerp(rectTransform.pivot, targetPosition, i / STEPS);
            yield return new WaitForSeconds(0.001f);
        }

        open = true;
        transitioning = false;
    }

    private IEnumerator AnimateClose() {
        open = true;
        transitioning = true;

        Vector2 targetPosition = basePosition + GetOffset();

        for(float i = 0; i <= STEPS; i++) {
            rectTransform.pivot = Vector2.Lerp(rectTransform.pivot, targetPosition, i / STEPS);
            yield return new WaitForSeconds(0.001f);
        }

        if(DestroyOnClose()) {
            Destroy(this.gameObject);
        }

        open = false;
        transitioning = false;
    }
}