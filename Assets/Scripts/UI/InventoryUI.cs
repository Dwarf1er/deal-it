using System.Collections;
using UnityEngine;

public abstract class InventoryUI : MonoBehaviour, ISubscriber {
    private Vector2 offset = new Vector2(0, -5);
    protected RectTransform rectTransform;
    protected Vector2 basePosition;
    private ItemSlotUI[] itemSlots;
    private int selectedItem;
    private bool isVisible;

    protected virtual void Start() {
        this.rectTransform = GetComponent<RectTransform>();
        this.basePosition = rectTransform.pivot;
        rectTransform.pivot = offset;
        this.itemSlots = GetComponentsInChildren<ItemSlotUI>();
        itemSlots[selectedItem].selected = true;
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    public bool HasDistance() {
        return false;
    }

    public Transform GetTransform() {
        return transform;
    }

    protected bool Visible() {
        return isVisible;
    }

    protected void SetVisible(bool isVisible) {
        StartCoroutine(Animate(isVisible));
    }

    protected void ToggleItem() {
        int i = selectedItem;

        while(true) {
            i = (i + 1) % itemSlots.Length;

            if(!itemSlots[i].locked) {
                itemSlots[selectedItem].selected = false;
                itemSlots[i].selected = true;
                selectedItem = i;
                break;
            }                
        }
    }

    private IEnumerator Animate(bool toOpen) {
        isVisible = !toOpen;
        Vector2 targetPosition = basePosition + (toOpen ? Vector2.zero : offset);

        for(float i = 0; i <= 50.0f; i++) {
            rectTransform.pivot = Vector2.Lerp(rectTransform.pivot, targetPosition, i / 50.0f);
            yield return new WaitForSeconds(0.001f);
        }
        isVisible = toOpen;
    }

    public ItemSlotUI GetSelected() {
        return itemSlots[selectedItem];
    }
}