using System.Collections;
using UnityEngine;

public abstract class InventoryUI : MonoBehaviour, ISubscriber {
    private Vector2 offset = new Vector2(0, -5);
    protected RectTransform rectTransform;
    protected Vector2 basePosition;
    private ItemSlotUI[] itemSlots;
    private int selectedItem;
    private bool isVisible;
    private bool buttonDown;

    protected virtual void Start() {
        this.rectTransform = GetComponent<RectTransform>();
        this.basePosition = rectTransform.pivot;
        rectTransform.pivot = offset;
        this.itemSlots = GetComponentsInChildren<ItemSlotUI>();
        itemSlots[selectedItem].selected = true;
        EventManager.Get()
            .Subscribe((MoveInputEvent inputEvent) => OnMove(inputEvent));
    }

    private void UpdateItems() {
        ItemStack[] itemStacks = GetInventory().GetItemStacks().ToArray();
            
        for(int i = 0; i < itemStacks.Length; i++) {
            itemSlots[i].SetItemStack(itemStacks[i]);
        }
    }

    private void OnMove(MoveInputEvent inputEvent) {
        if(inputEvent.direction.magnitude == 0) {
            buttonDown = false;
            return;
        }

        if(buttonDown) return;
        buttonDown = true;

        if(inputEvent.direction.x != 0) {
            itemSlots[GetInventory().GetSelected()].selected = false;
            GetInventory().ToggleItem(inputEvent.direction.x > 0);
            itemSlots[GetInventory().GetSelected()].selected = true;
        }
    }

    private void OnDestroy() {
        EventManager.Get().UnSubcribeAll(this);
    }

    protected abstract Inventory GetInventory();

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
        if(isVisible) {
            UpdateItems();
        }

        StartCoroutine(Animate(isVisible));
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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SetVisible(false);
            EventManager.Get().Broadcast(new CloseInventoryEvent(GetInventory()));
        }
    }
}